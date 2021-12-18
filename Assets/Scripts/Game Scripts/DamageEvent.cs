using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Damage Event", menuName = "Events/Damage Event")]
public class DamageEvent : Event
{
    public enum DamageType
    {
        health,
        stamina,
        mana,
        gold
    }

    [SerializeField] DamageType damageType;
    [SerializeField] bool isPercentage;
    [SerializeField] float value;

    public DamageType GetDamageType() { return damageType; }
    public bool IsPercentage() { return isPercentage; }
    public float GetValue() { return value; }
}
