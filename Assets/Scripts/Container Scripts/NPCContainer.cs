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
            GameObject.Find("GameManager").GetComponent<Engine>().SetIsInNPC(true, gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject.Find("GameManager").GetComponent<Engine>().SetIsInNPC(false, gameObject);
        }
    }
}
