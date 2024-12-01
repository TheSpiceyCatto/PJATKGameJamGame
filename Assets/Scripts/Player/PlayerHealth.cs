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
    private PlayerMovement _player;
    private Rigidbody2D _rb;
    private bool _canDamage = true;

    private void Start() {
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
        if (_canDamage && other.gameObject.TryGetComponent(out IDamageable damageable)) {
            TakeDamage(1);
            StartCoroutine(Knockback(VecToTarget(other.transform)));
        }
    }
    private IEnumerator Knockback(Vector2 direction) {
        _player.SetMove(false);
        _rb.velocity = Vector2.zero;
        _rb.AddForce(-direction.normalized * knockbackSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(knockbackDuration);
        _player.SetMove(true);
    }
    
    private Vector2 VecToTarget(Transform to) {
        return (Vector2)to.position - (Vector2)transform.position;
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
