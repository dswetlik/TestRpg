using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapon", order = 1)]
public class Weapon : Item
{

    public enum WeaponType
    {
        oneHanded,
        twoHanded
    }

    [SerializeField] WeaponType weaponType;
    [SerializeField] List<ActiveSkill> weaponSkills;
    [SerializeField] int minDamage;
    [SerializeField] int maxDamage;

    public Weapon(uint id = 0, string name = "New Weapon", uint weight = 0, uint value = 0, uint maxDamage = 0, uint minDamage = 0) : base(id)
    {

    }

    public WeaponType GetWeaponType() { return weaponType; }
    public List<ActiveSkill> GetWeaponSkills() { return weaponSkills; }
    public ActiveSkill GetRandomSkill() { return (weaponSkills.Count > 1) ? weaponSkills[Random.Range(0, weaponSkills.Count)] : weaponSkills[0]; }
    public int GetMaxDamage() { return maxDamage; }
    public int GetMinDamage() { return minDamage; }
    public void SetMaxDamage(int damage) { this.maxDamage = damage; }
    public void SetMinDamage(int damage) { this.minDamage = damage; }

    public int Attack()
    {
        return Random.Range(minDamage, maxDamage + 1);
    }

}
