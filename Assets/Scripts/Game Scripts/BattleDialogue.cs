using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Battle Dialogue", menuName = "Dialogue/Battle Dialogue")]
public class BattleDialogue : Dialogue
{

    [SerializeField] Enemy enemy;

    public Enemy GetEnemy() { return enemy; }

}
