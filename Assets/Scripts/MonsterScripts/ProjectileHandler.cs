using System;
using UnityEngine;

public class ProjectileHandler : WeaponManager
{
    [SerializeField] Transform _playerShip;
    [SerializeField] AnimationCurve heightCurve; // Assign in Inspector
    AudioManager _audioManager;
    const float _travelDuration = 1f;
    const float _maxHeight = 50f;
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private float _elapsedTime;
    float t;

    public Transform PlayerShip {get => _playerShip; set => _playerShip = value;}

    private void OnEnable()
    {
        if(_playerShip == null)
        {
            return;
        }
        _targetPosition = _playerShip.position;
        _startPosition = transform.position;
        _elapsedTime = 0f;
    }

    void Start()
    {
        _audioManager = AudioManager.instance;
    }

    void FixedUpdate()
    {
        if(_particleType == ParticleType.Monster)
        {
            MoveTowardsPlayerShip(_targetPosition , 1f);
        }
        else if(_particleType == ParticleType.Loot)
        {
            MoveTowardsPlayerShip(_playerShip.position , .9f);
        }
    }

    private void MoveTowardsPlayerShip(Vector3 target, float resetValue)
    {
        if (_playerShip == null || !gameObject.activeSelf) return;

        _elapsedTime += Time.deltaTime;
        t = Mathf.Clamp01(_elapsedTime / _travelDuration); // Normalize time
        // Move along the XZ plane
        Vector3 pos = Vector3.Lerp(_startPosition, target, t);
        float height = heightCurve.Evaluate(t) * _maxHeight;
        pos.y = Mathf.Lerp(_startPosition.y, target.y, t) + height;

        transform.position = pos;

        if (t >= resetValue) ResetProjectile();
    }

    private void ResetProjectile()
    {
        if(_particleType == ParticleType.Loot)
        {
            _audioManager.PlaySound(_audioManager.GetAudioClipData().ObtainLootSFX, .7f);
        }
        gameObject.SetActive(false);
        _elapsedTime = 0f;
        t = 0;
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.CompareTag("PlayerShip") && _particleType == ParticleType.Monster)
        {
            ResetProjectile();
        }
    }
}
