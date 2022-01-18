using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject playerInstance;
    public GameObject enemyPrefab;

    private List<GameObject> enemies = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < 10; ++i)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.transform.position = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));

            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.AssignPlayer(playerInstance);
            enemyScript.onDeath += RemoveEnemy;

            enemies.Add(enemy);
        }
    }

    private void RemoveEnemy(GameObject _enemy)
    {
        enemies.Remove(_enemy);
    }
}
