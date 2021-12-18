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
        bool eventFound = false;

        float key = Random.Range(0, 100.0f);
        int x = 0;

        while (!eventFound)
        {   
            while (x < events.Count)
            {
                if (key > itemProbability[x]) { x++; }
                else if (key <= itemProbability[x]) eventFound = true;
            }
            x = 0;
            key = Random.Range(0, 100.0f);
        }
        return events[x];
    }

    public Event GetEvent(int eventElement) { return events[eventElement]; }
    public float GetEventProbability(int floatElement) { return itemProbability[floatElement]; }

}
