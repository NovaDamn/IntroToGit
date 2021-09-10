using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    const float BULLET_SPEED = 25f;
    const float BULLET_LIFE = 5f;

    public LayerMask layersToInteract;

    private void Start()
    {
        Destroy(gameObject, BULLET_LIFE);
    }

    private void Update()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, BULLET_SPEED * Time.deltaTime, layersToInteract))
        {
            if (hitInfo.collider.gameObject.GetComponent<IDamageable>() != null)
            {
                hitInfo.collider.gameObject.GetComponent<IDamageable>().TakeDamage(10);
                Destroy(gameObject);
            }
        }
        else
        {
            transform.Translate(transform.forward * BULLET_SPEED * Time.deltaTime, Space.World);
        }
    }

    public void SetPosition(Transform _playerPos, Vector3 _mousePos)
    {
        transform.position = _playerPos.position + (_mousePos - _playerPos.position).normalized;
        transform.LookAt(_mousePos);
    }
}
