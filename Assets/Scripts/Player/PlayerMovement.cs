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
    [SerializeField] public GameObject gun;
    [SerializeField] private bool ascendOnEnd = false;
    private SpriteRenderer sr;
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
    private bool cutscene;

    private void Awake()
    {
        PlayerEventManager.OnDeath += Die;
        GameEventManager.OnEnemiesDefeated += StartEndSequence;
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
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnDestroy()
    {
        PlayerEventManager.OnDeath -= Die;
        GameEventManager.OnEnemiesDefeated -= StartEndSequence;
    }

    private void Update()
    {
        if (isDead || cutscene)
        {
            return;
        }
        _movement.Set(InputManager.Movement.x, InputManager.Movement.y);
        if (canMove)
        {
            _rb.velocity = _movement * moveSpeed;
        }

        
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
    
    private void StartEndSequence()
    {
        if (ascendOnEnd)
        {
            cutscene = true;
            _rb.velocity = Vector2.zero;
            GameEventManager.OnAscDialogueEnd += Ascension;
            EndLevelDialogue.Instance.TriggerDialogue();
        }
    }
    
    private void Ascension()
    {
        GameEventManager.OnAscDialogueEnd -= Ascension;
        StartCoroutine(AscendAndDisappear());
    }
    
    private IEnumerator AscendAndDisappear()
    {
        shoot.nextFireTime += 1000;
        Destroy(gun);
        Collider2D playerCollider = GetComponent<Collider2D>();
        if (playerCollider != null)
        {
            playerCollider.enabled = false;
        }
        if (isAlternateSprite)
        {
            _animator.enabled = false;
            _sr.sprite = defaultSprite;
            isAlternateSprite = false;
            _animator.enabled = true;
            _animator.SetBool("IsAstronaut", !isAlternateSprite);
            fm.SwapSprite();
        }
        if (fm != null)
        {
            Rigidbody2D followerRb = fm.GetComponent<Rigidbody2D>();
            if (followerRb != null)
            {
                followerRb.velocity = Vector2.zero;
                fm.maxFollowSpeed = 0;
                fm.minFollowSpeed = 0;
            }
        }
        float ascendSpeed = 2f;
        float fadeDuration = 2f;
        float targetHeight = 15f;
        float initialAlpha = _sr.color.a;
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;
        Cinemachine.CinemachineVirtualCamera virtualCamera = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
        while (transform.position.y < startPosition.y + targetHeight)
        {
            transform.position += Vector3.up * (ascendSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            if (transform.position.y >= startPosition.y + 5f && virtualCamera != null)
            {
                virtualCamera.Follow = null;
            }
            float alpha = Mathf.Lerp(initialAlpha, 0f, elapsedTime / fadeDuration);
            _sr.color = new Color(_sr.color.r, _sr.color.g, _sr.color.b, alpha);
            yield return null;
        }
        _sr.color = new Color(_sr.color.r, _sr.color.g, _sr.color.b, 0f);
        gameObject.SetActive(false);
        GameEventManager.EndCutscene();
    }
}