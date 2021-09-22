using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreContainer : MonoBehaviour
{

    [SerializeField] Store store;

    public Store GetStore() { return store; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player entered storefront");
            GameObject.Find("GameManager").GetComponent<Engine>().SetIsInShop(true, gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Exited Store");
            GameObject.Find("GameManager").GetComponent<Engine>().SetIsInShop(false, gameObject);
        }
    }
}
