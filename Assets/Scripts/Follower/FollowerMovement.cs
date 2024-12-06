using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] public float maxFollowSpeed = 5f;
    [SerializeField] public float minFollowSpeed = 1;  
    [SerializeField] private float minDistance = 2f;
    [SerializeField] private Vector2 offset = new Vector2(-2f, 0f);
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite alternateSprite;
    [SerializeField] private float activationDistance = 1.5f;
    private bool _isAlternateSprite = false;
    private SpriteRenderer _sr;
    private Animator _animator;
    private Rigidbody2D _rb;
    public bool isActivated = false;
    
    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (!isActivated)
        {
            if (distanceToPlayer <= activationDistance)
            {
                isActivated = true;
            }
        }
        else
        {
            Vector3 targetPosition = player.position + (Vector3)offset;
            float distance = Vector3.Distance(transform.position, targetPosition);

            if (distance > minDistance)
            {
                float followSpeed = Mathf.Lerp(minFollowSpeed, maxFollowSpeed, (distance - minDistance) / minDistance);
                Vector3 direction = (targetPosition - transform.position).normalized;
                transform.position += direction * followSpeed * Time.deltaTime;
                _animator.SetFloat("Velocity", followSpeed);
                UpdateSpriteDirection(direction);
            }
            else
            {
                MoveToTargetPosition(targetPosition);
                _animator.SetFloat("Velocity", 0);
                Vector3 directionToPlayer = (player.position - transform.position).normalized;
                UpdateSpriteDirection(directionToPlayer);
            }
        }
    }

    private void MoveToTargetPosition(Vector3 targetPosition)
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * minFollowSpeed);
    }

    public void SwapSprite()
    {
        _animator.SetBool("IsLaika", _isAlternateSprite);
        _isAlternateSprite = !_isAlternateSprite;
        if (_isAlternateSprite)
        {
            _sr.sprite = alternateSprite;
        }
        else
        {
            _sr.sprite = defaultSprite;
        }
    }

    public void Die()
    {
        _animator.SetTrigger("Death");
    }
    
    private void UpdateSpriteDirection(Vector3 direction)
    {
        if (direction.x > 0)
        {
            _sr.flipX = false;
        }
        else if (direction.x < 0)
        {
            _sr.flipX = true;
        }
    }
}
