using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Descriptive Event", menuName = "Events/Descriptive Event", order = 0)]
public class Event : ScriptableObject
{

    public enum EventType
    {
        enemy,
        loot,
        damage,
        descriptive
    }

    [SerializeField] new string name;
    [SerializeField] uint id;
    [SerializeField] EventType eventType;
    [TextArea(3,5)][SerializeField] string description;

    public string GetName() { return name; }
    public uint GetID() { return id; }
    public EventType GetEventType() { return eventType; }
    public string GetDescription() { return description; }

}
