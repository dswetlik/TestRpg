using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueContainer : MonoBehaviour
{

    [SerializeField] Dialogue dialogue;
    
    public Dialogue GetDialogue() { return dialogue; }
    public void SetDialogue(Dialogue dialogue) { this.dialogue = dialogue; }

}
