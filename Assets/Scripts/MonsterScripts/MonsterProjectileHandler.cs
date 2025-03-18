using System;
using UnityEngine;

public class MonsterProjectileHandler : BaseWeaponScript
{
    [SerializeField] Transform _playerShip;
    [SerializeField] private AnimationCurve _animationCurve;  // Animation curve for the arc
    Vector3 _startPosition;
    Vector3 _targetPosition;
    float _elapsedTime = 0f;
    float travelDuration = 1f;
    float _maxHeight;
    private Vector3 _lastPosition;
    [SerializeField] float t;
    [SerializeField] float _speed;

    public Transform PlayerShip {get => _playerShip; set => _playerShip = value;}
    private void OnEnable()
    {
        _targetPosition = _playerShip.position;
        _maxHeight = _targetPosition.y + 15f;
    }

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        MoveTowardsPlayerShip();
    }
    private void MoveTowardsPlayerShip()
    {
        if (_playerShip == null || !gameObject.activeSelf) return;


        _elapsedTime += Time.deltaTime; 
        float t = Mathf.Clamp01(_elapsedTime / travelDuration); // Normalize time

        // Interpolate movement
        Vector3 pos = Vector3.Lerp(_startPosition, _targetPosition, t);
        pos.y += _animationCurve.Evaluate(t) * _maxHeight; // Apply arc

        Vector3 velocity = (pos - transform.position) / Time.deltaTime;
        transform.position = pos;

        // Smooth rotation towards movement direction
        Vector3 direction = (pos - _lastPosition).normalized;
        if (velocity != Vector3.zero) 
        {
        Quaternion targetRotation = Quaternion.LookRotation(velocity);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _speed);
        }

        _lastPosition = pos;

        if (t >= 1f)
        {
            ResetProjectile();
        }
    }

    private void ResetProjectile()
    {
        gameObject.SetActive(false);
        _elapsedTime = 0f;
        t = 0;
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.CompareTag("PlayerShip"))
        {
            ResetProjectile();
        }
    }
}
