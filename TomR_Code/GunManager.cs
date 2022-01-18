using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public GameObject playerInstance;
    public GameObject bulletPrefab;

    private float refireTime = 0;

    private void Update()
    {
        refireTime -= Time.deltaTime;
        if (Input.GetMouseButton(0) && refireTime <= 0)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab);
            Vector3 mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
            mousePosition.y = 0;
            bullet.transform.position = playerInstance.transform.position + (mousePosition - playerInstance.transform.position).normalized;
            bullet.transform.LookAt(mousePosition);
        }
    }
}
