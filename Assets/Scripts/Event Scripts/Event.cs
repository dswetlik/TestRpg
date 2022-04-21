using UnityEngine;

//[CreateAssetMenu(fileName = "New Descriptive Event", menuName = "Events/Descriptive Event", order = 0)]
public abstract class Event : ScriptableObject
{

    public enum EventType
    {
        start,
        enemy,
        loot,
        damage,
        description
    }

    public Event(EventType eventType) { this.eventType = eventType; }

    [SerializeField] EventType eventType;
    [SerializeField] uint id;

    public EventType GetEventType() { return eventType; }
    public uint GetID() { return id; }

    public static bool operator ==(Event eventA, Event eventB) { return (eventA.GetID() == eventB.GetID()); }
    public static bool operator !=(Event eventA, Event eventB) { return (eventA.GetID() != eventB.GetID()); }
    public static bool operator <=(Event eventA, Event eventB) { return (eventA.GetID() <= eventB.GetID()); }
    public static bool operator >=(Event eventA, Event eventB) { return (eventA.GetID() >= eventB.GetID()); }
    public static bool operator >(Event eventA, Event eventB) { return (eventA.GetID() > eventB.GetID()); }
    public static bool operator <(Event eventA, Event eventB) { return (eventA.GetID() < eventB.GetID()); }
}
