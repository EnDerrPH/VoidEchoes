using System;
using UnityEngine;

public class MonsterProjectileHandler : WeaponManager
{
    [SerializeField] Transform _playerShip;
    Vector3 _startPosition;
    Vector3 _targetPosition;
    float _elapsedTime = 0f;
    float travelDuration = 1f;
    const float _maxHeight = 50f;
    float t;

    public Transform PlayerShip {get => _playerShip; set => _playerShip = value;}
    private void OnEnable()
    {
        _targetPosition = _playerShip.position;
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

        // Move along the XZ plane
        Vector3 pos = Vector3.Lerp(_startPosition, _targetPosition, t);

        // Apply height using a sine wave
        float height = Mathf.Sin(t * Mathf.PI) * _maxHeight; // Peak at t = 0.5
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
