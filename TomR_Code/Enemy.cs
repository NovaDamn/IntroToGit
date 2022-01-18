using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    const float ENEMY_SPEED = 5f;
    const int ENEMY_HEALTH = 100;

    private GameObject playerInstance;
    private int health;

    private void Start()
    {
        health = ENEMY_HEALTH;
    }

    private void Update()
    {
        transform.Translate((playerInstance.transform.position - transform.position).normalized * Time.deltaTime * ENEMY_SPEED);
    }

    public void AssignPlayer(GameObject _player)
    {
        playerInstance = _player;
    }

    public void TakeDamage(int _damage)
    {
        health -= _damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
