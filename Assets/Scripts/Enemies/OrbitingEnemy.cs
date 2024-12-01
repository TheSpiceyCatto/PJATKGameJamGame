using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitingEnemy : Enemy
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float velocityClamp = 10f;
    private Vector2 lastKnownPosition;

    private void FixedUpdate() {
        if (invulnerable) {
            Friction(friction);
            return;
        }

        float clampedX = Mathf.Clamp(rb.velocity.x, -velocityClamp, velocityClamp);
        float clampedY = Mathf.Clamp(rb.velocity.y, -velocityClamp, velocityClamp);
        rb.velocity = new Vector2(clampedX, clampedY);
        toPlayer = VecToTarget(player.transform);
        if (playerDead)
            toPlayer *= -1;
        rb.AddForce(toPlayer.normalized * moveSpeed); 
    }
}
