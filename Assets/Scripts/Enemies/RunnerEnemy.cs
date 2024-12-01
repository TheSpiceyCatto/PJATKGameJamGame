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
        if (playerDead)
            toPlayer *= -1;
        rb.velocity = toPlayer.normalized * moveSpeed;
    }
}
