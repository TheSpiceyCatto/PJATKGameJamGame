using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunShoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject szczekPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] public float bulletSpeed = 10f;
    [SerializeField] private float spreadAngle = 30f;
    [SerializeField] public float fireRate = 0.5f;
    [SerializeField] public int bulletCount = 1;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite alternateSprite;
    [SerializeField] private GameObject shootEffectPrefab; // Prefab for the shoot effect
    [SerializeField] private float effectOffsetDistance = 0.5f; // Distance to move the effect towards the mouse
    [SerializeField] private PlayerMovement pm;

    private float nextFireTime = 0f;
    private SpriteRenderer _sr;
    private bool isAlternateSprite = false;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

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
            pm.Szczek();
            nextFireTime = Time.time + fireRate;

            // Play shooting effect
            PlayShootEffect();

            // Shoot bullets
            if (bulletPrefab != null && firePoint != null && bulletCount > 0)
            {
                float startAngle = -spreadAngle / 2f;
                float angleStep = bulletCount > 1 ? spreadAngle / (bulletCount - 1) : 0;
                for (int i = 0; i < bulletCount; i++)
                {
                    float angle = startAngle + i * angleStep;
                    Quaternion rotation = firePoint.rotation * Quaternion.Euler(0, 0, angle);
                    GameObject bullet;
                    if (!isAlternateSprite)
                    {
                        bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
                    }
                    else
                    {
                        bullet = Instantiate(szczekPrefab, firePoint.position, rotation);
                    }
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        if (bulletCount == 1)
                        {
                            rb.velocity = firePoint.right * bulletSpeed;
                        }
                        else
                        {
                            rb.velocity = rotation * Vector2.right * bulletSpeed;
                        }
                    }
                }
            }
        }
    }

    private void PlayShootEffect()
    {
        if (shootEffectPrefab != null && firePoint != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            Vector3 directionToMouse = (mousePosition - firePoint.position).normalized;
            Vector3 effectPosition = firePoint.position + directionToMouse * effectOffsetDistance;
            GameObject shootEffect = Instantiate(shootEffectPrefab, effectPosition, firePoint.rotation);
            shootEffect.GetComponent<Animator>().SetTrigger("shoot");
            if (!isAlternateSprite)
            {
                shootEffect.GetComponent<Animator>().SetBool("isAstronaut", true);
            }
            else
            {
                shootEffect.GetComponent<Animator>().SetBool("isAstronaut", false);
            }
            Destroy(shootEffect, 0.2f);
            Destroy(shootEffect, 0.2f);
        }
    }

    public void spriteSwap()
    {
        isAlternateSprite = !isAlternateSprite;
        if (isAlternateSprite)
        {
            _sr.sprite = alternateSprite;
        }
        else
        {
            _sr.sprite = defaultSprite;
        }
    }
}
