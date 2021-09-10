using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    float ENEMY_SPEED = 5f;
    int ENEMY_HEALTH = 100;

    private GameObject player;

    private void Start()
    {
        player = GameManager.instance.player;
        ENEMY_SPEED = Random.Range(5, 10);
    }

    private void Update()
    {
        if (player != null)
        {
            transform.Translate((player.transform.position - transform.position).normalized * Time.deltaTime * ENEMY_SPEED);
        }
    }

    public void TakeDamage(int _damage)
    {
        ENEMY_HEALTH -= _damage;

        if (ENEMY_HEALTH <= 0)
        {
            Died();
        }
    }

    public void Died()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            if (other.gameObject.GetComponent<IDamageable>() != null)
            {
                other.gameObject.GetComponent<IDamageable>().TakeDamage(25);
                Destroy(gameObject);
            }
        }
    }
}
