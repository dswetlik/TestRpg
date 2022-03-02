using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Items/Consumables/Potion")]
public class Potion : Consumable
{

    public enum PotionType
    {
        none,
        health,
        stamina,
        mana
    };

    [SerializeField] List<PotionType> effects;
    [SerializeField] List<int> effectChanges;

    public List<int> GetEffectChanges() { return effectChanges; }

    public override void UseItem(Player player)
    {
        for(int i = 0; i < effects.Count; i++)
        {
            PotionType effect = effects[i];
            switch (effect)
            {
                case PotionType.health:
                    player.ChangeHealth(effectChanges[i]);
                    break;
                case PotionType.stamina:
                    player.ChangeStamina(effectChanges[i]);
                    break;
                case PotionType.mana:
                    player.ChangeMana(effectChanges[i]);
                    break;
            }
        }
    }
}
