using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private int maxBounces = 3;
    private int bounceCount = 0;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_rb != null && _rb.velocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("BouncyWall"))
        {
            bounceCount++;
            if (bounceCount >= maxBounces)
            {
                Destroy(gameObject);
            }
        }else if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Destroy(gameObject);
        }
        // else
        // {
        //     Destroy(gameObject);
        // }
    }
}