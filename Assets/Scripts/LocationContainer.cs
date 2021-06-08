using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationContainer : MonoBehaviour
{
    [SerializeField] Location location;

    public Location GetLocation() { return location; }

    public void SetLocation(Location location) { this.location = location; }
}
