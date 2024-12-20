using Managers;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private void Awake()
    {
        PlayerEventManager.OnDeath += PlayerDeath;
    }

    private void OnDestroy() {
        PlayerEventManager.OnDeath -= PlayerDeath;
    }

    private Animator animator;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private float lockedTill;
    private int currentState;
    
    #region State Booleans
    
    private bool damaged;
    private bool death;

    #endregion
    
    #region States

    private readonly int Idle = Animator.StringToHash("Idle");
    private readonly int Run = Animator.StringToHash("Run");
    private readonly int Death = Animator.StringToHash("Die");

    #endregion

    private void Start() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        //if (rb.velocity.x != 0 && !death) sr.flipX = rb.velocity.x < 0;

        var state = GetState();

        damaged = false;

        if (state == currentState) return;
        animator.CrossFade(state, 0, 0);
        currentState = state;
    }

    private int GetState()
    {
        if (Time.time < lockedTill) return currentState;

        // Priorities
        if (death) return Death;
        // if (damaged) return LockState(Hit, hitTime);

        return rb.velocity.magnitude > 0 ? Run : Idle;
        int LockState(int s, float t)
        {
            lockedTill = Time.time + t;
            return s;
        }
    }

    private void PlayerDeath() {
        death = true;
    }
}
