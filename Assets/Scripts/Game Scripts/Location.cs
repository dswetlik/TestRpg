using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[CreateAssetMenu(fileName = "New Location", menuName = "Location", order = 3)]
public class Location : ScriptableObject
{

    [SerializeField] uint id;
    [SerializeField] new string name;
    [SerializeField] bool isSearchable;
    [SerializeField] ItemLootTable itemLootTable;
    [SerializeField] EventTable eventTable;
    [SerializeField] EnemyTable enemyTable;
    [SerializeField] int maxEnemySpawn;
    [SerializeField] bool isEnterable;
    [SerializeField] Location enteredLocation;
    [SerializeField] string sceneName;
    [SerializeField] Vector3 spawnLocation;
    [SerializeField] Quaternion spawnRotation;
    [SerializeField] List<GameObject> chests;

    /*
    public Location(uint id, string name, bool isSearchable, Location north, Location east, Location south, Location west)
    {
        this.id = id;
        this.name = name;
        this.isSearchable = isSearchable;
        this.north = north;
        this.east = east;
        this.south = south;
        this.west = west;
    }
    */

    public uint GetID() { return id; }
    public string GetName() { return name; }
    public bool GetSearchable() { return isSearchable; }
    public ItemLootTable GetLootTable() { return itemLootTable; }
    public EventTable GetEventTable() { return eventTable; }
    public EnemyTable GetEnemyTable() { return enemyTable; }
    public int GetMaxEnemySpawn() { return maxEnemySpawn; }
    public bool GetEnterable() { return isEnterable; }
    public Location GetEnteredLocation() { return enteredLocation; }
    public string GetSceneName() { return sceneName; }
    public Vector3 GetSpawnLocation() { return spawnLocation; }
    public Quaternion GetSpawnRotation() { return spawnRotation; }

    public void SetID(uint id) { this.id = id; }
    public void SetName(string name) { this.name = name; }
    public void SetSearchable(bool isSearchable) { this.isSearchable = isSearchable; }
    public void SetLootTable(ItemLootTable itemLootTable) { this.itemLootTable = itemLootTable; }
    public void SetEventTable(EventTable eventTable) { this.eventTable = eventTable; }

}
