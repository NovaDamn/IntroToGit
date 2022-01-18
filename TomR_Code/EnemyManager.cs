using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    const float ENEMY_SPEED = 5f;
    const int ENEMY_HEALTH = 100;

    public GameObject playerInstance;
    public GameObject enemyPrefab;

    private List<GameObject> enemies = new List<GameObject>();
    private List<int> enemyHealth = new List<int>();

    private void Start()
    {
        for (int i = 0; i < 10; ++i)
        {
            GameObject enemy = GameObject.Instantiate(enemyPrefab);
            enemy.GetComponent<Enemy>().AssignPlayer(playerInstance);
            enemy.transform.position = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
            enemies.Add(enemy);
            enemyHealth.Add(ENEMY_HEALTH);
        }
    }
}
