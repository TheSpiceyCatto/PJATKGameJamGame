using UnityEngine;

//Will probably use this class for things every enemy should have
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : MonoBehaviour {
    protected Vector2 vecToTarget(Transform to) {
        return (Vector2)to.position - (Vector2)transform.position;
    }
}
