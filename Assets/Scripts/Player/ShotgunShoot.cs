using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunShoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float spreadAngle = 30f;
    [SerializeField] public float fireRate = 0.5f;
    [SerializeField] public int bulletCount = 1;
    private float nextFireTime = 0f;

    void Update()
    {
        if (InputManager.Shoot)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            if (bulletPrefab != null && firePoint != null && bulletCount > 0)
            {
                float startAngle = -spreadAngle / 2f;
                float angleStep = bulletCount > 1 ? spreadAngle / (bulletCount - 1) : 0;
                for (int i = 0; i < bulletCount; i++)
                {
                    float angle = startAngle + i * angleStep;
                    Quaternion rotation = firePoint.rotation * Quaternion.Euler(0, 0, angle);
                    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        if (bulletCount == 1)
                        {
                            rb.velocity = firePoint.right * bulletSpeed;
                        }else
                            rb.velocity = rotation * Vector2.right * bulletSpeed;
                    }
                }
            }
        }
        
    }
}