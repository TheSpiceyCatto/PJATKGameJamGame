using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float maxFollowSpeed = 5f;
    [SerializeField] private float minFollowSpeed = 1;  
    [SerializeField] private float minDistance = 2f;
    [SerializeField] private Vector2 offset = new Vector2(-2f, 0f);
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite alternateSprite;
    private bool _isAlternateSprite = false;
    private SpriteRenderer _sr;
    private Animator _animator;
    private Rigidbody2D _rb; 
    
    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector3 targetPosition = player.position + (Vector3)offset;
        float distance = Vector3.Distance(transform.position, targetPosition);
        if (distance > minDistance)
        {
            float followSpeed = Mathf.Lerp(minFollowSpeed, maxFollowSpeed, (distance - minDistance) / minDistance);
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * followSpeed * Time.deltaTime;
            _animator.SetFloat("Velocity", followSpeed);
        }
        else
        {
            MoveToTargetPosition(targetPosition);
            _animator.SetFloat("Velocity", 0);
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
}
