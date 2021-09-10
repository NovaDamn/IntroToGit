using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;

    public GameObject enemyPrefab;

    private List<Enemy> enemies = new List<Enemy>();

    public GameObject GameOverPanel;

    private bool gameOver = false;

    private void Start()
    {
        player.GetComponent<Player>().OnDeath += GameOver;

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // Spawn some enemies
        for (int i = 0; i < 5; ++i)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.transform.position = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
            enemies.Add(enemy.GetComponent<Enemy>());
        }

        StartCoroutine(EnemySpawner());
    }

    IEnumerator EnemySpawner()
    {
        while (!gameOver)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.transform.position = new Vector3(Random.Range(-100, 100), -0.5f, Random.Range(-100, 100));
            enemies.Add(enemy.GetComponent<Enemy>());
            yield return new WaitForSeconds(10);
        }
    }

    public void GameOver()
    {
        gameOver = true;
        GameOverPanel.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (gameOver)
            {
                SceneManager.LoadScene("ShooterGamePlay");
            }
        }
    }
}
