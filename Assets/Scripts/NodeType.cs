using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeType : MonoBehaviour
{

    [SerializeField] List<NodeType> nearbyNodes = new List<NodeType>();
    [SerializeField] bool isOccupied;

    public List<NodeType> GetNearbyNodes() { return nearbyNodes; }
    public bool IsOccupied() { return isOccupied; }

    public void SetOccupied(bool x) { isOccupied = x; }
}
