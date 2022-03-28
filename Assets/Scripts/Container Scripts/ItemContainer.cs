﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemContainer : MonoBehaviour
{
    Item item;
    int itemCount = 0;

    public bool IsWeaponContainer = false;
    public bool IsArmorContainer = false;

    public Item GetItem()
    {
        return item;
    }

    public void SetItem(Item item)
    {
        this.item = item;
        if(!IsArmorContainer && !IsWeaponContainer)
            itemCount++;
        UpdateTextComponents();
    }

    public void SetItem(Item item, int count)
    {
        this.item = item;
        itemCount = count;
        UpdateTextComponents();
    }

    public void AddItem()
    {
        itemCount++;
        UpdateTextComponents();
    }

    public void AddItem(int count)
    {
        itemCount += count;
        UpdateTextComponents();
    }

    public int GetCount() { return itemCount; }

    public void RemoveItem()
    {
        if (IsWeaponContainer)
            item = Engine.NULL_WEAPON;
        else if (IsArmorContainer)
            item = Engine.NULL_ARMOR;
        else
            itemCount--;
        UpdateTextComponents();
    }

    public void RemoveItem(int count)
    {
        itemCount -= count;
        UpdateTextComponents();
    }

    void UpdateTextComponents()
    {
        Image itemImg = gameObject.transform.GetChild(0).GetComponent<Image>();

        if (item.GetSprite() != null)
        {
            itemImg.color = new Color(itemImg.color.r, itemImg.color.g, itemImg.color.b, 1);
            itemImg.sprite = item.GetSprite();
        }
        else
        {
            itemImg.color = new Color(itemImg.color.r, itemImg.color.g, itemImg.color.b, 0);
        }

        if (!IsArmorContainer && !IsWeaponContainer)
            gameObject.transform.GetChild(1).GetComponent<Text>().text = itemCount.ToString();
    }

    public bool IsEmpty()
    {
        if (itemCount <= 0 || item == Engine.NULL_ITEM || item == Engine.NULL_WEAPON || item == Engine.NULL_ARMOR)
            return true;
        return false;
    }

}
