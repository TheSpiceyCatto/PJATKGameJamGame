using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int hp = 3;
    private SpriteRenderer sr;
    private BoxCollider2D box;
    private PlayerMovement player;

    private void Awake() {
        PlayerEventManager.OnDeath += Die;
    }

    private void OnDestroy() {
        PlayerEventManager.OnDeath -= Die;
    }

    private void Start() {
        sr = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
        player = GetComponent<PlayerMovement>();
    }

    public void TakeDamage(int damage) {
        hp -= damage;
        PlayerEventManager.UpdateHealth(hp);
        if (hp <= 0) {
            PlayerEventManager.Death();
        }
    }

    private void Die() {
        sr.enabled = false;
        box.enabled = false;
    }
}
