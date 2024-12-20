using System;
using System.Collections;
using Managers;
using UnityEngine;

//Will probably use this class for things every enemy should have
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : MonoBehaviour, IDamageable {
    [SerializeField] protected int hp = 3;
    [SerializeField] private int damage = 1;
    [Header("Knockback")]
    [SerializeField] protected float friction = 0.5f;
    [SerializeField] private float knockbackSpeed = 5f;
    [SerializeField] private float iframeTime = 1f;
    [Header("Raycast")]
    [SerializeField] protected LayerMask ignoreLayer;
    // protected bool hasLineOfSight;
    protected bool invulnerable = false;
    protected bool playerDead = false;
    protected Vector2 toPlayer;
    protected Rigidbody2D rb;
    protected GameObject player;

    private void Awake() {
        PlayerEventManager.OnDeath += RunOnPlayerDeath;
    }

    private void OnDestroy() {
        PlayerEventManager.OnDeath -= RunOnPlayerDeath;
    }
    
    protected void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void TakeDamage(int damageAmount) {
        if (invulnerable)
            return;
        hp -= damageAmount;
        StartCoroutine(Knockback());
        if (hp <= 0) {
            Die();
        }
    }

    private void RunOnPlayerDeath() {
        playerDead = true;
    }

    private void Die() {
        EnemySpawner.DecreaseCount();
        Destroy(gameObject);
    }

    
    
    #region Math Functions

    protected Vector2 VecToTarget(Transform to) {
        return (Vector2)to.position - (Vector2)transform.position;
    }
    
    private IEnumerator Knockback() {
        invulnerable = true;
        rb.velocity = Vector2.zero;
        rb.AddForce(-toPlayer.normalized * knockbackSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(iframeTime);
        invulnerable = false;
    }
    
    protected void Friction(float frictionAmount) {
        float frictionX = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));
        frictionX *= Mathf.Sign(rb.velocity.x);
        float frictionY = Mathf.Min(Mathf.Abs(rb.velocity.y), Mathf.Abs(frictionAmount));
        frictionY *= Mathf.Sign(rb.velocity.y);
        rb.AddForce(Vector2.one * -new Vector2(frictionX, frictionY), ForceMode2D.Impulse);
    }

    #endregion
}
