using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Damage Event", menuName = "Events/Damage Event")]
public class DamageEvent : Event, IEventLinkable, IEventDescription, IEventPlayer
{
    public enum DamageType
    {
        health,
        stamina,
        mana,
        gold
    }

    private DamageEvent(DamageType damageType) : base(EventType.damage) { this.damageType = damageType; }

    [SerializeField] [TextArea(3, 5)] string playerDescription;
    [SerializeField] string eventLinkButtonText;
    [SerializeField] [TextArea(3, 5)] string eventDescription;

    [SerializeField] List<Event> eventLinks;

    [SerializeField] DamageType damageType;
    [SerializeField] bool isPercentage;
    [SerializeField] float value;

    public string GetPlayerDescription() { return playerDescription; }
    public string GetEventLinkButtonText() { return eventLinkButtonText; }

    public string GetEventDescription() { return eventDescription; }

    public List<Event> GetEventLinks() { return eventLinks; }

    public DamageType GetDamageType() { return damageType; }
    public bool IsPercentage() { return isPercentage; }
    public float GetValue() { return value; }
}
