using System;
using UnityEngine;

public class MonsterProjectileHandler : WeaponManager
{
    [SerializeField] Transform _playerShip;
    [SerializeField] AnimationCurve heightCurve; // Assign in Inspector
    const float _travelDuration = 1f;
    const float _maxHeight = 50f;
    
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private float _elapsedTime;
    float t;

    public Transform PlayerShip {get => _playerShip; set => _playerShip = value;}
    private void OnEnable()
    {
        _targetPosition = _playerShip.position;
        _startPosition = transform.position;
        _elapsedTime = 0f;
    }

    void FixedUpdate()
    {
       MoveTowardsPlayerShip();
    }

    private void MoveTowardsPlayerShip()
    {
        if (_playerShip == null || !gameObject.activeSelf) return;

        _elapsedTime += Time.deltaTime;
        t = Mathf.Clamp01(_elapsedTime / _travelDuration); // Normalize time

        // Move along the XZ plane
        Vector3 pos = Vector3.Lerp(_startPosition, _targetPosition, t);

        // Apply height using the AnimationCurve
        float height = heightCurve.Evaluate(t) * _maxHeight; 
        pos.y = Mathf.Lerp(_startPosition.y, _targetPosition.y, t) + height;

        transform.position = pos;

        if (t >= 1f) ResetProjectile();
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
