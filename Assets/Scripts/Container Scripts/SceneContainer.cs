using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneContainer : MonoBehaviour
{
    [SerializeField] Location location;

    public Location GetLocation() { return location; }
}
