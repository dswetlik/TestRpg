using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event Base", menuName = "Events/Event Base")]
public class EventBase : ScriptableObject
{
    
    [SerializeField] new string name;
    [TextArea(3, 5)] [SerializeField] string description;

    [SerializeField] Event choiceAEvent;
    [SerializeField] Event choiceBEvent;

    public string GetName() { return name; }
    public string GetDescription() { return description; }

    public Event GetChoiceA() { return choiceAEvent; }
    public Event GetChoiceB() { return choiceBEvent; }

}
