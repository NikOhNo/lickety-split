using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class NPCConvo : MonoBehaviour
{
    public NPCConversation myConvo;
    bool canTalk = false;
    // private void OnMouseOver() {
    //     if (Input.GetMouseButtonDown(0)) {
    //         ConversationManager.Instance.StartConversation(myConvo);
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            canTalk = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            canTalk = false;
        }
    }

    private void Update() {
        if (canTalk && (Input.GetKeyDown(KeyCode.F))) {
            ConversationManager.Instance.StartConversation(myConvo);
            canTalk = false;
        }
    }
}
