using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    
    // object for spawning player if it doesn't exist in the scene already
    // should be in every scene :)
    
    // Start is called before the first frame update
    void Start()
    {
        if (!GameObject.FindWithTag("Player"))
        {
            var player = Instantiate(playerPrefab);
            player.transform.position = this.transform.position;
            
            GameObject.FindWithTag("MainCamera").GetComponent<CinemachineBrain>().ActiveVirtualCamera.Follow = player.transform;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
