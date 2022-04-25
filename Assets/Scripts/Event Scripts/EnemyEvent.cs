using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Event", menuName = "Events/Enemy Event", order = 0)]
public class EnemyEvent : Event, IEventLinkable, IEventDescription, IEventPlayer
{

    protected EnemyEvent() : base(EventType.enemy) { }

    [SerializeField] [TextArea(3, 5)] string playerDescription;
    [SerializeField] string eventLinkButtonText;
    [SerializeField] [TextArea(3, 5)] string eventDescription;

    [SerializeField] List<Event> eventLinks;

    public string GetPlayerDescription() { return playerDescription; }
    public string GetEventLinkButtonText() { return eventLinkButtonText; }

    public string GetEventDescription() { return eventDescription; }

    public List<Event> GetEventLinks() { return eventLinks; }

    [SerializeField] Enemy enemy;

    public Enemy GetEnemy() { return enemy; }
    public void SetEnemy(Enemy enemy) { this.enemy = enemy; }

}
