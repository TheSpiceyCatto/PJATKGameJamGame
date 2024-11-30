using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int hp = 3;
    [SerializeField] private float knockbackSpeed = 5f;
    [SerializeField] private float knockbackDuration = 0.5f;
    [SerializeField] private float iframeTime = 1f;
    private SpriteRenderer _sr;
    private BoxCollider2D _box;
    private PlayerMovement _player;
    private Rigidbody2D _rb;
    private bool _canDamage = true;

    private void Awake() {
        PlayerEventManager.OnDeath += Die;
    }

    private void OnDestroy() {
        PlayerEventManager.OnDeath -= Die;
    }

    private void Start() {
        _sr = GetComponent<SpriteRenderer>();
        _box = GetComponent<BoxCollider2D>();
        _player = GetComponent<PlayerMovement>();
        _rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage) {
        if (!_canDamage)
            return;
        hp -= damage;
        PlayerEventManager.UpdateHealth(hp);
        StartCoroutine(Invulnerability());
    }
    
    protected void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.TryGetComponent(out IDamageable damageable)) {
            TakeDamage(1);
            StartCoroutine(Knockback(_player.transform.position - other.transform.position));
        }
    }
    private IEnumerator Knockback(Vector2 direction) {
        _player.SetMove(false);
        _rb.velocity = Vector2.zero;
        _rb.AddForce(direction.normalized * knockbackSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(knockbackDuration);
        _player.SetMove(true);
    }

    private void Die() {
        _sr.enabled = false;
        _box.enabled = false;
    }

    private IEnumerator Invulnerability() {
        _canDamage = false;
        if (hp <= 0) {
            PlayerEventManager.Death();
        }
        yield return new WaitForSeconds(iframeTime);
        _player.SetMove(true);
        _canDamage = true;
    }
}
