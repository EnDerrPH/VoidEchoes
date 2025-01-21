using UnityEngine;

public class Enemy : Entity
{
    Transform _player;
    [SerializeField] float _stopMovementRange;
    [SerializeField] EnemyWeapon _enemyWeapon;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _enemyWeapon = GetComponent<EnemyWeapon>();
    }
    void Update()
    {
        ApproachTarget();
    }

    private void ApproachTarget()
    {
        if(!_enemyWeapon.TargetLocked)
        {
            return;
        }

        float distance = Vector3.Distance(transform.position , _player.position);

        if(distance <= _stopMovementRange)
        {
            return;
        }

       transform.position = Vector3.MoveTowards(transform.position, _player.position,  .5f);
    }
}
