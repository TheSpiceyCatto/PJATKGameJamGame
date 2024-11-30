using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerEnemy : Enemy
{
    [SerializeField] private float moveSpeed = 5f;

    private void FixedUpdate() {
        if (invulnerable) {
            Friction(friction);
            return;
        }
        toPlayer = VecToTarget(player.transform);
        // RaycastHit2D los = Physics2D.Raycast(transform.position, toPlayer, Mathf.Infinity, ~ignoreLayer);
        // if (los.collider) {
        //     hasLineOfSight = los.collider.CompareTag("Player");
        // }
        // if (hasLineOfSight) {
        rb.velocity = toPlayer.normalized * moveSpeed;
        // } else {
        //     Friction(friction);
        // }
    }
}
