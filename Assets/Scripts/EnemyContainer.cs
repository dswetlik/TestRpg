using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContainer : MonoBehaviour
{
    [SerializeField] Enemy enemy;

    public Enemy GetEnemy() { return enemy; }

    public void SetEnemy(Enemy enemy) { this.enemy = enemy; }
}

