using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationContainer : MonoBehaviour
{
    [SerializeField] Location locationA;
    [SerializeField] Location locationB;

    [SerializeField] GameObject locationAObject;
    [SerializeField] GameObject locationBObject;

    public Location GetLocationA() { return locationA; }
    public Location GetLocationB() { return locationB; }

    public GameObject GetLocationAObject() { return locationAObject; }
    public GameObject GetLocationBObject() { return locationBObject; }
}
