using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventContainer : MonoBehaviour
{

    [SerializeField] Event _event;

    public Event GetEvent() { return _event; }
    public void SetEvent(Event _event) { this._event = _event; }

}
