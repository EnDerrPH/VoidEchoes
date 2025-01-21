using System.Collections;
using UnityEngine;

public class EnemyWeapon : Weapon
{
    Transform _player;
    [SerializeField] float _playerAlertRange;
    [SerializeField] ParticleSystem _ammo;
    [SerializeField] float _attackTimer;
    [SerializeField] CollisionHandler _playerDetector;
    bool _isTargetLocked;
    public bool TargetLocked => _isTargetLocked;

    void Update()
    {
        CheckIfPlayerInRange();
        
        if(_player == null)
        {
            return;
        }
            AimTarget(_player,this.transform , 5f);
            DelayAttack();
            _isTargetLocked = true;
    }

    private void DelayAttack()
    {
        _attackTimer += 1f * Time.deltaTime;

        SetAmmoEmmision(_ammo, _attackTimer >= 2f);

        if(_attackTimer >= 3f)
        {
            _attackTimer = 0f;
        }
    }

    private void CheckIfPlayerInRange()
    {
        if(_playerDetector.Player == null)
        {
            return;
        }
        _player = _playerDetector.Player.GetComponent<Transform>();
    }
}
