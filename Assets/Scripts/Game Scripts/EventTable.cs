using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event Table", menuName = "Tables/Event Table", order = 4)]
public class EventTable : ScriptableObject
{
    [SerializeField] List<Event> events = new List<Event>();
    [Range(0, 100.0f)] [SerializeField] List<float> itemProbability = new List<float>();

    public Event RandomEvent()
    {
        float key = Random.Range(0, 100.0f);
        bool eventFound = false;
        int x = 0;

        while (!eventFound)
        {
            if (key > itemProbability[x]) { x++; }
            else if (key <= itemProbability[x]) eventFound = true;
        }

        return events[x];
    }

    public Event GetEvent(int eventElement) { return events[eventElement]; }
    public float GetEventProbability(int floatElement) { return itemProbability[floatElement]; }

}
