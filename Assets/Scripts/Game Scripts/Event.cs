using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Base Event", menuName = "Events/Base Event", order = 0)]
public class Event : ScriptableObject
{

    public enum EventType
    {
        enemy,
        loot
    }

    [SerializeField] new string name;
    [SerializeField] uint id;
    [SerializeField] EventType eventType;

    public string GetName() { return name; }
    public uint GetID() { return id; }
    public EventType GetEventType() { return eventType; }

    public void SetName(string name) { this.name = name; }
    public void SetID(uint id) { this.id = id; }
    public void SetEventType(EventType eventType) { this.eventType = eventType; }
}
