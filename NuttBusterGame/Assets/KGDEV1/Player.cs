using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Player : MonoBehaviour, IDamageable
{
    const float PLAYER_SPEED = 10f;
    int PLAYER_HEALTH = 100;

    public GameObject bulletPrefab;
    private List<GameObject> bullets = new List<GameObject>();

    public System.Action OnDeath;

    [SerializeField] private float timeBetweenShooting = 0.25f;
    private float refireTime = 0;

    public TextMeshProUGUI healthText;

    public GameObject gun;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        healthText.text = (PLAYER_HEALTH + " HP");
    }

    private void Update()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y, transform.position.z);

        transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * PLAYER_SPEED, 0, Input.GetAxis("Vertical") * Time.deltaTime * PLAYER_SPEED);
        // Change to rigidbody, because you can travel through walls

        // Rotate Weapon In Right Direction
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f);
        Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
        lookPos = lookPos - transform.position;
        float angle = Mathf.Atan2(lookPos.z, lookPos.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.AngleAxis(-angle, Vector3.up);

        refireTime -= Time.deltaTime;
        if (Input.GetMouseButton(0) && refireTime <= 0)
        {
            // Fire gun
            refireTime = timeBetweenShooting;
            GameObject bullet = Instantiate(bulletPrefab);
            Vector3 mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
            mousePosition.y = 0;
            bullet.GetComponent<Bullet>().SetPosition(GameManager.instance.player.transform, mousePosition);
            bullets.Add(bullet);
        }
    }

    public void TakeDamage(int _damage)
    {
        PLAYER_HEALTH -= _damage;
        healthText.text = PLAYER_HEALTH + " HP";

        if (PLAYER_HEALTH <= 0)
        {
            Died();
            healthText.text = "";
        }
    }

    public void Died()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}
