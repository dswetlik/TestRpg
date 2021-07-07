using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{

    [SerializeField] new string name;
    [SerializeField] List<Item> itemList = new List<Item>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Player entered storefront");
            GameObject.Find("GameManager").GetComponent<Engine>().SetIsInShop(true, gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Exited Store");
            GameObject.Find("GameManager").GetComponent<Engine>().SetIsInShop(false, gameObject);
        }
    }

    public List<Item> GetItemList() { return itemList; }

    public void AddItem(Item item) { itemList.Add(item); }

    public void RemoveItem(Item item) { itemList.Remove(item); }

    public string GetName() { return name; }
}
