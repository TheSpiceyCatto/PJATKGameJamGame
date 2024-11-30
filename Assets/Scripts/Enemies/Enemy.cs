using System;
using UnityEngine;

//Will probably use this class for things every enemy should have
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : MonoBehaviour, IDamageable {
    [SerializeField] private int hp = 3;
    [SerializeField] private int damage = 1;
    protected Rigidbody2D rb;
    protected GameObject player;
    
    protected void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damageAmount) {
        hp -= damageAmount;
        if (hp <= 0) {
            Die();
        }
    }
    protected abstract void Die();
    
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.TryGetComponent(out IDamageable damageable)) {
            damageable.TakeDamage(damage);
        }
    }
    
    #region Math Functions

    protected Vector2 vecToTarget(Transform to) {
        return (Vector2)to.position - (Vector2)transform.position;
    }
    
    protected void Friction(float frictionAmount) {
        float friction = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));
        friction *= Mathf.Sign(rb.velocity.x);
        float frictionY = Mathf.Min(Mathf.Abs(rb.velocity.y), Mathf.Abs(frictionAmount));
        frictionY *= Mathf.Sign(rb.velocity.y);
        rb.AddForce(Vector2.one * -new Vector2(friction, frictionY), ForceMode2D.Impulse);
    }

    #endregion
}
