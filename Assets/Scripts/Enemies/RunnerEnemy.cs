using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerEnemy : Enemy
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float friction = 0.5f;
    [SerializeField] private LayerMask ignoreLayer;
    private bool hasLineOfSight;

    protected override void Die() {
        Destroy(gameObject);
    }

    private void FixedUpdate() {
        Vector2 toPlayer = vecToTarget(player.transform);
        RaycastHit2D los = Physics2D.Raycast(transform.position, toPlayer, Mathf.Infinity, ~ignoreLayer);
        if (los.collider != null) {
            hasLineOfSight = los.collider.CompareTag("Player");
        }
        if (hasLineOfSight) {
            // rb.AddForce(toPlayer * moveSpeed, ForceMode2D.Force);
            rb.velocity = toPlayer.normalized * moveSpeed;
            Debug.DrawRay(transform.position, toPlayer, Color.magenta);
        } else {
            Friction(friction);
            Debug.DrawRay(transform.position, toPlayer, Color.red);
        }
    }
}
