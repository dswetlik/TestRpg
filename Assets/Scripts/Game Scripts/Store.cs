using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Store : MonoBehaviour
{

    [SerializeField] new string name;
    [SerializeField] List<Item> defItemList = new List<Item>();
    List<Item> itemList = new List<Item>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            itemList = defItemList.ToList<Item>();
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
            itemList.Clear();
        }
    }

    public List<Item> GetItemList() { return itemList; }

    public void AddItem(Item item) { itemList.Add(item); }

    public void RemoveItem(Item item) { itemList.Remove(item); }

    public void AddDefItem(Item item) { defItemList.Add(item); }

    public void RemoveDefItem(Item item) { defItemList.Remove(item); }

    public string GetName() { return name; }
}
