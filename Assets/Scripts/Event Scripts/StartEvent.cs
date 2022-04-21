using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event Base", menuName = "Events/Start Event Chain", order = 0)]
public class StartEvent : Event, IEventLinkable, IEventDescription
{

    protected StartEvent() : base(EventType.start) { }

    
    [SerializeField] new string name;
    
    [TextArea(3, 5)] [SerializeField] string openingDescription;
    [SerializeField] List<Event> eventLinks;

    [SerializeField] bool isRepeatable;

    [Header("Dungeon Event Bools")]
    [SerializeField] bool isOptional;
    [SerializeField] bool isComplete;

    public string GetName() { return name; }
   

    public string GetEventDescription() { return openingDescription; }    
    public List<Event> GetEventLinks() { return eventLinks; }
    public bool IsRepeatable() { return isRepeatable; }
    public bool IsOptional() { return isOptional; }
    public bool IsComplete() { return isComplete; }
    public void SetComplete(bool x) { isComplete = x; }

}
