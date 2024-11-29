using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerEnemy : Enemy
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float frictionAmount = 0.5f;
    private bool hasLineOfSight;
    private GameObject player;
    private Rigidbody2D rb;
    
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        Vector2 toPlayer = vecToTarget(player.transform);
        RaycastHit2D los = Physics2D.Raycast(transform.position, toPlayer);
        if (los.collider != null) {
            hasLineOfSight = los.collider.CompareTag("Player");
        }
        if (hasLineOfSight) {
            rb.AddForce(toPlayer * moveSpeed);
            Debug.DrawRay(transform.position, toPlayer, Color.magenta);
        } else {
            Friction();
            Debug.DrawRay(transform.position, toPlayer, Color.red);
        }
    }
    private void Friction() {
        float friction = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));
        friction *= Mathf.Sign(rb.velocity.x);
        float frictionY = Mathf.Min(Mathf.Abs(rb.velocity.y), Mathf.Abs(frictionAmount));
        frictionY *= Mathf.Sign(rb.velocity.y);
        rb.AddForce(Vector2.one * -new Vector2(friction, frictionY), ForceMode2D.Impulse);
    }
}
