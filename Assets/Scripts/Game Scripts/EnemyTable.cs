using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Table", menuName = "Tables/Enemy Tables", order = 5)]
public class EnemyTable : ScriptableObject
{

    [SerializeField] List<Enemy> enemies = new List<Enemy>();
    [Range(0, 100.0f)] [SerializeField] List<float> enemyProbability = new List<float>();

    /*
    public List<Enemy> EnemySpawn(Location location)
    {
        List<Enemy> enemySpawn = new List<Enemy>();

        int i = 0;
        while (i < location.GetMaxEnemySpawn())
        {
            float key = Random.Range(0, 100.0f);
            for (int j = 0; j < enemies.Count; j++)
            {
                if (key <= enemyProbability[j])
                {
                    enemySpawn.Add(enemies[j]);
                    i++;
                }
                if (i >= location.GetMaxEnemySpawn())
                    break;
            }
        }

        return enemySpawn;
    }
    */
    public List<Enemy> GetEnemies() { return enemies; }
    public Enemy GetEnemy(int enemyElement) { return enemies[enemyElement]; }
    public float GetEnemyProbability(int floatElement) { return enemyProbability[floatElement]; }

}
