using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    private Animator animator;
    private readonly int Death = Animator.StringToHash("Death");
    private BoxCollider2D _box;

    private void Awake() {
        PlayerEventManager.OnDeath += Die;
    }
    private void OnDestroy() {
        PlayerEventManager.OnDeath -= Die;
    }
    private void Start() {
        animator = GetComponent<Animator>();
        //_box = GetComponent<BoxCollider2D>();
    }
    
    private void Die() {
        animator.SetTrigger(Death);
        //_box.enabled = false;
    }
}
