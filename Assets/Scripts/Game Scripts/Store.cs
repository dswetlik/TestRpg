﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New Store", menuName = "Store")]
public class Store : ScriptableObject
{

    [SerializeField] uint id;
    [SerializeField] new string name;
    [SerializeField] List<Item> itemList = new List<Item>();
    [SerializeField] NPC npc;
    
    public uint GetID() { return id; }

    public string GetName() { return name; }

    public List<Item> GetItemList() { return itemList.ToList<Item>(); }

    public NPC GetNPC() { return npc; }

    public void AddItem(Item item) { itemList.Add(item); }

    public void RemoveItem(Item item) { itemList.Remove(item); }

    public void ClearInventory() { itemList.Clear(); }
    
}
