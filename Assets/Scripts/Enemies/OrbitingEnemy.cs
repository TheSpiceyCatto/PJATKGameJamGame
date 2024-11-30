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
        // RaycastHit2D los = Physics2D.Raycast(transform.position, toPlayer, Mathf.Infinity, ~ignoreLayer);
        // if (los.collider) {
        //     hasLineOfSight = los.collider.CompareTag("Player");
        // }
        // if (hasLineOfSight) {
        //     lastKnownPosition = player.transform.position;
        rb.AddForce(toPlayer.normalized * moveSpeed); 
        // } else {
        //     rb.AddForce(lastKnownPosition.normalized * (moveSpeed * 0.5f));
        //     Friction(friction);
        // }
    }
}
