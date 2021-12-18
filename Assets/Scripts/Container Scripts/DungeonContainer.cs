using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonContainer : MonoBehaviour
{
    [SerializeField] Dungeon dungeon;

    public Dungeon GetDungeon() { return dungeon; }
}
