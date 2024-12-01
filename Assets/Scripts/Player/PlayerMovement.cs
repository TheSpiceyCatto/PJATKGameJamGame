using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Managers;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private SpriteRenderer srWeapon;
    [SerializeField] private Transform shotgun;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite alternateSprite;
    [SerializeField] private ShotgunShoot shoot;
    [SerializeField] private Transform follower;
    [SerializeField] private float minDistance = 2f;
    [SerializeField] private FollowerMovement fm;
    [SerializeField] public float swapCooldown = 0.5f;
    private float lastSwap = 0f;
    private Animator _animator;
    private Vector2 _movement;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private bool isAlternateSprite = false;
    public float velocity;
    private Vector3 _weaponRight;
    private Vector3 _weaponLeft;
    private bool isFacingRight = true;
    private bool canMove = true;
    private float astronautx;
    private float laikax;
    private float astronauty;
    private float laikay;
    private bool isDead;

    private void Awake()
    {
        PlayerEventManager.OnDeath += Die;
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        astronautx = shotgun.localPosition.x;
        astronauty = shotgun.localPosition.y;
        // laikax = astronautx + 0.611f;
        // laikay = astronauty + 0.089f;
        laikax = astronautx;
        laikay = astronauty;
        _weaponRight = new Vector3(-astronautx, astronauty, 0f);
        _weaponLeft = new Vector3(astronautx, astronauty, 0f);
        
    }

    private void OnDestroy()
    {
        PlayerEventManager.OnDeath -= Die;
    }

    private void Update()
    {
        if (isDead)
        {
            return;
        }
        _movement.Set(InputManager.Movement.x, InputManager.Movement.y);
        if (canMove)
            _rb.velocity = _movement * moveSpeed;
        _animator.SetFloat("Velocity", _rb.velocity.magnitude);
        Flip();
        if (InputManager.Swap)
        {
            if (fm.isActivated)
            {
                Swap();
                _animator.SetBool("IsAstronaut", !isAlternateSprite);
            }
        }
    }

    private void Flip()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bool shouldFaceRight = mousePosition.x > transform.position.x;
        if (shouldFaceRight && !isFacingRight)
        {
            isFacingRight = true;
            _sr.flipX = false;
            srWeapon.flipY = false;
            shotgun.localPosition = _weaponLeft;
            shotgun.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (!shouldFaceRight && isFacingRight)
        {
            isFacingRight = false;
            _sr.flipX = true;
            srWeapon.flipY = true;
            shotgun.localPosition = _weaponRight;
            shotgun.localRotation = Quaternion.Euler(0, 180f, 0);
        }
    }

    private void Swap()
    {
        if (Time.time >= lastSwap)
        {
            lastSwap = Time.time + swapCooldown;
            isAlternateSprite = !isAlternateSprite;
            if (isAlternateSprite)
            {
                _sr.sprite = alternateSprite;
                shoot.bulletCount = 4;
                shoot.fireRate = 1f;
                shoot.bulletSpeed = 15f;
                _weaponRight = new Vector3(-laikax, laikay, 0f);
                _weaponLeft = new Vector3(laikax, laikay, 0f);
            }
            else
            {
                _sr.sprite = defaultSprite;
                shoot.bulletCount = 1;
                shoot.fireRate = 0.5f;
                shoot.bulletSpeed = 30f;
                _weaponRight = new Vector3(-astronautx, astronauty, 0f);
                _weaponLeft = new Vector3(astronautx, astronauty, 0f);
            }
            SwapPlaces();
            fm.SwapSprite();
            shoot.spriteSwap();
        }
    }

    private void SwapPlaces()
    {
        Vector3 tempPosition = transform.position;
        transform.position = follower.position;
        follower.position = tempPosition + (tempPosition - transform.position).normalized * minDistance;
    }

    public void Szczek()
    {
        _animator.SetTrigger("Szczek");
    }

    public void SetMove(bool moveState) {
        canMove = moveState;
    }

    private void Die()
    {
        isDead = true;
        fm.Die();
        _rb.velocity = Vector2.zero;
    }
}