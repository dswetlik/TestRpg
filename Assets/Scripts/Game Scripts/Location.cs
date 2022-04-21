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

    public enum LocationType
    {
        Town,
        Wilderness,
        Dungeon
    }

    protected Location(LocationType locationType) { this.locationType = locationType; }

    [SerializeField] LocationType locationType;
    [SerializeField] uint id;
    [SerializeField] new string name;
    [SerializeField] string sceneName;

    [SerializeField] Vector3 spawnLocation;
    [SerializeField] Vector3 spawnRotation;

    public LocationType GetLocationType() { return locationType; }
    public uint GetID() { return id; }
    public string GetName() { return name; }

    public string GetSceneName() { return sceneName; }
    public Vector3 GetSpawnLocation() { return spawnLocation; }
    public Vector3 GetSpawnRotation() { return spawnRotation; }

}
