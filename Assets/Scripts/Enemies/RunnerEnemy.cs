using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerEnemy : Enemy
{
    [SerializeField] private float moveSpeed = 5f;
    [Header("Raycast")]
    [SerializeField] private LayerMask ignoreLayer;
    private bool hasLineOfSight;

    public override void TakeDamage(int damageAmount) {
        if (invulnerable)
            return;
        hp -= damageAmount;
        StartCoroutine(Knockback());
        if (hp <= 0) {
            Die();
        }
    }


    private void FixedUpdate() {
        if (invulnerable) {
            Friction(friction);
            return;
        }
        toPlayer = vecToTarget(player.transform);
        RaycastHit2D los = Physics2D.Raycast(transform.position, toPlayer, Mathf.Infinity, ~ignoreLayer);
        if (los.collider != null) {
            hasLineOfSight = los.collider.CompareTag("Player");
        }
        if (hasLineOfSight) {
            rb.velocity = toPlayer.normalized * moveSpeed;
            Debug.DrawRay(transform.position, toPlayer, Color.magenta);
        } else {
            Friction(friction);
            Debug.DrawRay(transform.position, toPlayer, Color.red);
        }
    }
    protected override void Die() {
        Destroy(gameObject);
    }
}
