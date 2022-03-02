using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boss Enemy", menuName = "Enemies/Boss Enemy", order = 1)]
public class BossEnemy : Enemy
{

    [Header("Unlocks")]
    [SerializeField] List<Item> unlockedItems;
    [SerializeField] List<int> unlockedItemsCount;
    [SerializeField] string unlockedTitle;

    bool hasBeenDefeated;

    public List<Item> GetUnlockedItems() { return unlockedItems; }
    public List<int> GetUnlockedItemsCount() { return unlockedItemsCount; }
    public string GetUnlockedTitle() { return unlockedTitle; }
    public bool HasBeenDefeated() { return hasBeenDefeated; }

    public void SetHasBeenDefeated(bool x) { hasBeenDefeated = x; }
}
