using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dungeon", menuName = "Dungeon")]
public class Dungeon : Location
{

    protected Dungeon() : base(LocationType.Dungeon) { }

    [SerializeField] string description;

    [SerializeField] bool isCleared;
    [SerializeField] List<StartEvent> events;

    public string GetDescription() { return description; }
    public bool IsCleared() { return isCleared; }
    public void SetCleared(bool x) { isCleared = x; }
    
    public List<StartEvent> GetEvents(bool includeCleared = false, bool includeOptional = false)
    {
        List<StartEvent> _startEvents = new List<StartEvent>();
        foreach (StartEvent _events in events)
        {
            _startEvents.Add(EventManager.EventDictionary[_events.GetID()]);
            Debug.Log(string.Format("Event: {0}", _events.GetName()));
        }

        if (!includeCleared)
            _startEvents.RemoveAll(x => x.IsComplete());
        if (!includeOptional)
            _startEvents.RemoveAll(x => x.IsOptional());

        return _startEvents;
    }

}
