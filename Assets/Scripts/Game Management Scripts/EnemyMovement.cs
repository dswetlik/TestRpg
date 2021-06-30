using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] NodeType currentNode;
    [SerializeField] NodeType previousNode;

    [SerializeField] Enemy enemy;

    private void Start()
    {
        //StartCoroutine(Move());
    }

    public void Move()
    {
        StartCoroutine(GetNextNode());
    }

    IEnumerator GetNextNode()
    {
        Debug.Log("Determining Move");
        List<NodeType> nearbyNodes = currentNode.GetComponent<NodeType>().GetNearbyNodes();

        GameObject player = GameObject.Find("Player");

        if (nearbyNodes.Count <= 1)
        {
            MoveToNode(nearbyNodes[0]);
        }
        else
        {
            Debug.Log("Node Count: " + nearbyNodes.Count);
            NodeType nearestNode = nearbyNodes[0];
            float distanceTo = Vector3.Distance(currentNode.transform.position, nearestNode.transform.position) + Vector3.Distance(nearestNode.transform.position, player.transform.position);
            for(int i = 0; i < nearbyNodes.Count; i++)
            {
                float newDistance = Vector3.Distance(currentNode.transform.position, nearestNode.transform.position) + Vector3.Distance(nearbyNodes[i].transform.position, player.transform.position);
                if (newDistance < distanceTo && nearbyNodes[i] != previousNode && !nearbyNodes[i].IsOccupied())
                {
                    nearestNode = nearbyNodes[i];
                    distanceTo = newDistance;
                }
            }
            MoveToNode(nearestNode);
        }

        yield return null;
    }
    
    public Enemy GetEnemy() { return enemy; }
    public void SetEnemy(Enemy enemy) { this.enemy = enemy; }

    public void MoveToNode(NodeType node)
    {
        Debug.Log("Moving");
        transform.position = node.transform.position;
        previousNode = currentNode;
        currentNode = node;

        transform.LookAt(GameObject.Find("Player").transform);

        previousNode.SetOccupied(false);
        currentNode.SetOccupied(true);
    }

    public void SetCurrentNode(NodeType node) { currentNode = node; } 

    public void SetPreviousNode(NodeType node)
    {
        previousNode = node;
    }
}
