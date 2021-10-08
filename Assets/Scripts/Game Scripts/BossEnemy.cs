using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boss Enemy", menuName = "Enemy/Boss Enemy", order = 1)]
public class BossEnemy : Enemy
{
    [SerializeField] List<Item> unlockedItems;
    [SerializeField] List<int> unlockedItemsCount;

    public List<Item> GetUnlockedItems() { return unlockedItems; }
    public List<int> GetUnlockedItemsCount() { return unlockedItemsCount; }
}
