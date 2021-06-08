using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Event", menuName = "Events/Enemy Event", order = 0)]
public class EnemyEvent : Event
{
    
    [SerializeField] Enemy enemy;

    public Enemy GetEnemy() { return enemy; }
    public void SetEnemy(Enemy enemy) { this.enemy = enemy; }

}
