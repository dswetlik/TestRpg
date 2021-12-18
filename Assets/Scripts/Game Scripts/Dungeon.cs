using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dungeon", menuName = "Dungeon")]
public class Dungeon : ScriptableObject
{
    [SerializeField] new string name;
    [SerializeField] uint id;
    [SerializeField] string description;
    [SerializeField] bool isCleared;
    [SerializeField] int floorCount;
    [SerializeField] int clearedFloorCount;
    [SerializeField] List<EventBase> floorEvents;
    [SerializeField] Vector3 outputLocation;
    [SerializeField] Vector3 outputRotation;

    public string GetName() { return name; }
    public uint GetID() { return id; }
    public string GetDescription() { return description; }
    public bool IsCleared() { return isCleared; }
    public int GetFloorCount() { return floorCount; }
    public int GetClearedFloorCount() { return clearedFloorCount; }
    public List<EventBase> GetFloorEvents() { return floorEvents; }
    public Vector3 GetOutputLocation() { return outputLocation; }
    public Vector3 GetOutputRotation() { return outputRotation; }

    public void SetCleared(bool x) { isCleared = x; }

    public void IncrementClearedFloors() { clearedFloorCount++; }
    public void DecrementClearedFloors() { clearedFloorCount--; }

    public void SetClearedFloors(int floors) { clearedFloorCount = floors; }
}
