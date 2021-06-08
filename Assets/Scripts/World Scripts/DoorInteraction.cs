using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    [SerializeField] bool isOpen;
    [SerializeField] bool isLocked;

    private void Start()
    {
        isOpen = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameObject.Find("GameManager").GetComponent<Engine>().SetActiveDoor(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            GameObject.Find("GameManager").GetComponent<Engine>().SetActiveDoor(this, false);
        }
    }

    public void OpenDoor()
    {
        isOpen = true;
        transform.parent.gameObject.SetActive(false);
    }

    public bool IsOpen() { return isOpen; }

    public bool IsLocked() { return isLocked; }

    public void SetLocked(bool x) { isLocked = x; }
}
