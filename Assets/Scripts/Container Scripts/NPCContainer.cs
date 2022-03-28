using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCContainer : MonoBehaviour
{

    [SerializeField] NPC npc;
    
    public NPC GetNPC() { return npc; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject.Find("GameManager").GetComponent<DialogueManager>().SetIsInNPC(true, npc);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject.Find("GameManager").GetComponent<DialogueManager>().SetIsInNPC(false, npc);
        }
    }
}
