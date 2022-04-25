using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Description Event", menuName = "Events/Description Event", order = 0)]
public class DescriptionEvent : Event, IEventLinkable, IEventDescription, IEventPlayer
{

    protected DescriptionEvent() : base(EventType.description) { }

    [SerializeField] [TextArea(3, 5)] string playerDescription;
    [SerializeField] string eventLinkButtonText;
    [SerializeField] [TextArea(3, 5)] string eventDescription;

    [SerializeField] List<Event> eventLinks;

    public string GetPlayerDescription() { return playerDescription; }
    public string GetEventLinkButtonText() { return eventLinkButtonText; }

    public string GetEventDescription() { return eventDescription; }

    public List<Event> GetEventLinks() { return eventLinks; }

}
