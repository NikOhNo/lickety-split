using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;


// detects collisions 
public class RoomExitController : MonoBehaviour
{
    public SceneAsset sceneToLoad;
    private string sceneAssetPath;
    BoxCollider2D bc2d; // controls area in which the player gets the prompt to switch rooms
    public GameObject playerSpawnPosition;
    public Collider2D target; // collider2d attached to player

    public UIController uiController;
    
    public KeyCode interactButton = KeyCode.F;

    public GameObject roomExitSpawner;
    
    private bool activatable = false;
    
    /*
     * a plan
     *  create a gameobject that is dontdestroyonload-ed, store in this game object the reference to the target
     *  find the roomexit in the new scene, place the player at that roomexit's playerSpawnPosition. then destroy the
     *  previously created object.. ... ... . .yeyyyghlkjajh
     */

    void Start()
    {
        bc2d = GetComponent<BoxCollider2D>();
        bc2d.isTrigger = true;
        sceneAssetPath = AssetDatabase.GetAssetPath(sceneToLoad);
        uiController = GameObject.Find("UIController").GetComponent<UIController>();
        // get position of the PlayerSpawnPosition child object
    }

    public void Update()
    {
        // FIXME maybe trigger something on the player that lets them know that they're activatable?
        if (activatable && Input.GetKeyDown(interactButton))
        {
            SwitchSceneAndRepositionPlayer(target);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            activatable = true;
            target = col;
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.red;
        }
        
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player") && activatable)
        {
            activatable = false;
            target = null;
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.black;
        }
    }

    public void SwitchSceneAndRepositionPlayer(Collider2D target)
    {
        DontDestroyOnLoad(target);
        // create room exit spawner, which will persist into the next room and place the player at the right location 
        // for the exit in that room. 
        // maybe in the future, when there are more than one exit per room, each exit is connected to another exit in a
        // different scene. perhaps, when the roomExitSpawner is looking for the exit to place the player at, check 
        // what room they connect to? and compare that to the room the player is coming from?
        var spawnerInstance = Instantiate(roomExitSpawner);
        DontDestroyOnLoad(spawnerInstance);
        var roomExitSpawnerScript = spawnerInstance.GetComponent<RoomExitSpawner>();
        roomExitSpawnerScript.player = target.gameObject;
        roomExitSpawnerScript.previousScene = SceneManager.GetActiveScene().name;
        uiController.StartCoroutine(uiController.FadeToBlack()); // uncomment for fade to black effect
        // wait until the fade to black has finished to load the scene...
        uiController.onFinishFadeToBlack += OnFinishFadeToBlack;
        // SceneManager.LoadScene(sceneToLoad.name, LoadSceneMode.Single);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnFinishFadeToBlack()
    {
        uiController.onFinishFadeToBlack -= OnFinishFadeToBlack;
        SceneManager.LoadScene(sceneToLoad.name, LoadSceneMode.Single);
    }
}
