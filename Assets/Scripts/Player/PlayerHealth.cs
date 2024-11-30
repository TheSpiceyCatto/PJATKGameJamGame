using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int hp = 3;
    [SerializeField] private float iframeTime = 1f;
    private SpriteRenderer _sr;
    private BoxCollider2D _box;
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
    }

    public void TakeDamage(int damage) {
        if (!_canDamage)
            return;
        hp -= damage;
        PlayerEventManager.UpdateHealth(hp);
        StartCoroutine(Invulnerability(iframeTime));
    }

    private void Die() {
        _sr.enabled = false;
        _box.enabled = false;
    }

    private IEnumerator Invulnerability(float duration) {
        _canDamage = false;
        if (hp <= 0) {
            PlayerEventManager.Death();
        }
        yield return new WaitForSeconds(duration);
        _canDamage = true;
    }
}
