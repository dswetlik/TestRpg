using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Inventory system, works off of a dual SortedDictionaries, where the key is ItemID.
/// </summary>
[System.Serializable]
public class Inventory //: ISerializationCallbackReceiver
{

    [SerializeField] List<uint> _itemKeys = new List<uint>();
    [SerializeField] List<Item> _itemValues = new List<Item>();

    [SerializeField] List<uint> _countKeys = new List<uint>();
    [SerializeField] List<uint> _countValues = new List<uint>();

    SortedDictionary<uint, Item> inventory = new SortedDictionary<uint, Item>();
    SortedDictionary<uint, uint> count = new SortedDictionary<uint, uint>();

    /// <summary>
    /// Adds a single count of Item to inventory.
    /// </summary>
    /// <param name="item">Item to add to inventory.</param>
    public void AddToInventory(Item item)
    {
        try
        {
            inventory.Add(item.GetID(), item);
            count.Add(item.GetID(), 1);
        }
        catch (ArgumentException)
        {
            count[item.GetID()] += 1;
        }
    }

    /// <summary>
    /// Adds multiple of one Item type to inventory.
    /// </summary>
    /// <param name="item">Item to Add.</param>
    /// <param name="numberOfItems">Number of Items To Add.</param>
    public void AddToInventory(Item item, uint numberOfItems)
    {
        if (numberOfItems <= 1)
            AddToInventory(item);
        else
        {
            try
            {
                inventory.Add(item.GetID(), item);
                count.Add(item.GetID(), numberOfItems);
            }
            catch (ArgumentException)
            {
                count[item.GetID()] += numberOfItems;
            }
        }
    }

    /// <summary>
    /// Gets Item of item.id from Inventory
    /// </summary>
    /// <param name="id">ID of Item</param>
    /// <returns>Item from Inventory</returns>
    public Item GetItem(uint id)
    {
        // try-catch statement ensures no KeyNotFoundExceptions occur when working with dictionaries
        try
        {
            if (count[id] > 1)
            {
                count[id] -= 1;
                return inventory[id];
            }
            else
            {
                count.Remove(id);
                Item item = inventory[id];
                inventory.Remove(id);
                return item;
            }
        }
        catch (KeyNotFoundException)
        {
            return null;
        }
    }

    public int GetItemCount(uint id)
    {
        try
        {
            return (int)count[id];
        }
        catch(KeyNotFoundException)
        {
            return -1;
        }
    }


    /// <summary>
    /// Removes one count of item.id from inventory.
    /// </summary>
    /// <param name="id">ID of Item</param>
    public void RemoveItem(uint id)
    {
        // try-catch statement ensures no KeyNotFoundExceptions occur when working with dictionaries
        try
        {
            if (count[id] > 1)
            {
                count[id] -= 1;
            }
            else
            {
                count.Remove(id);
                Item item = inventory[id];
                inventory.Remove(id);
            }
        }
        catch (KeyNotFoundException)
        {

        }
    }

    /// <summary>
    /// Returns total weight of inventory.
    /// </summary>
    /// <returns>A uint value for weight of inventory.</returns>
    public uint GetTotalWeight()
    {
        uint totalWeight = 0;

        foreach (KeyValuePair<uint, Item> kvp in inventory)
        {
            if (count[kvp.Key] > 1)
                totalWeight += (kvp.Value.GetWeight() * count[kvp.Key]);
            else
                totalWeight += kvp.Value.GetWeight();
        }

        return totalWeight;
    }

    /// <summary>
    /// Checks if item.id is in inventory
    /// </summary>
    /// <param name="id">ID of Item</param>
    /// <returns>True if item.id is inside inventory</returns>
    public bool CheckForItem(uint id)
    {
        return inventory.ContainsKey(id);
    }

    public void OnBeforeSerialize()
    {
        GameObject.Find("GameManager").GetComponent<Engine>().ClearInventorySlots();

        _itemKeys.Clear(); _itemValues.Clear();
        _countKeys.Clear(); _countValues.Clear();

        foreach (KeyValuePair<uint, Item> kvp in inventory)
        {
            _itemKeys.Add(kvp.Key);
            _itemValues.Add(kvp.Value);
        }

        foreach (KeyValuePair<uint, uint> kvp in count)
        {
            _countKeys.Add(kvp.Key);
            _countValues.Add(kvp.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        inventory = new SortedDictionary<uint, Item>();
        count = new SortedDictionary<uint, uint>();

        for (int i = 0; i < Math.Min(_itemKeys.Count(), _itemValues.Count()) && i < Math.Min(_countKeys.Count(), _countValues.Count()); i++)
        {
            inventory.Add(_itemKeys[i], _itemValues[i]);
            count.Add(_countKeys[i], _countValues[i]);
            GameObject.Find("GameManager").GetComponent<Engine>().LoadInventorySlot(_itemValues[i], _countValues[i]);
        }

    }

    public List<uint> GetItemKeys() { return _itemKeys; }
    public List<Item> GetItemValues() { return _itemValues; }

    public List<uint> GetItemValuesAsID()
    {
        List<uint> _itemValuesAsID = new List<uint>();
        for (int i = 0; i < _itemValues.Count; i++)
            _itemValuesAsID.Add(_itemValues[i].GetID());

        return _itemValuesAsID;
    }

    public List<uint> GetCountKeys() { return _countKeys; }
    public List<uint> GetCountValues() { return _countValues; }

    public void SetItemKeys(List<uint> list) { _itemKeys = list; }
    public void SetItemValues(List<Item> list) { _itemValues = list; }

    public void SetItemValuesByID(List<uint> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            try
            {
                _itemValues.Add(Engine.ItemDictionary[list[i]]);
            }
            catch (KeyNotFoundException e)
            {
                Debug.LogError(String.Format("ID at element {0} not found in ItemDictionary!", i));
            }
        }
    }

    public void SetCountKeys(List<uint> list) { _countKeys = list; }
    public void SetCountValues(List<uint> list) { _countValues = list; }

}
