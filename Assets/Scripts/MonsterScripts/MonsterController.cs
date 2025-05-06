using UnityEngine;

public class MonsterController : BaseController
{
    [SerializeField] Transform _playerShip;
    [SerializeField] float _moveSpeed;
    [SerializeField] MonsterState _currentState;
    [SerializeField] LayerMask _obstacleLayer;
    [SerializeField] GameObject _weapon;
    [SerializeField] MonsterCombatHandler _monsterCombatHandler;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] float _attackingDistance;
    MonsterData _monsterData;
    string _onAlert = "OnAlert";
    string _onMoveForward = "OnMoveForward";
    string _onAttack = "OnAttack";
    string _onHit = "OnHit";
    string _onDeath = "OnDeath";
    string _onIdle = "OnIdle";
    const float _rotationSpeed = 5f;
    float _onHitTimerLimit = 1f;
    float _onHitTimer = 0f;
    float _timerSpeed = 1f;
    bool _isHit;
    Quaternion _targetRotation;
    public Transform PlayerShip {get => _playerShip ; set => _playerShip = value;}
    public float MoveSpeed {get => _moveSpeed ; set => _moveSpeed = value;}
    public GameObject Weapon {get => _weapon ; set => _weapon = value;}

    private void Update()
    {
        CalculateDistance();
        HitTimerCooldown();
        HandleMonsterState();
    }

    public void OnAttack()
    {
        _weapon.transform.position = transform.position;
        _weapon.SetActive(true);
        _audioManager.PlaySound(_monsterData.OnAttackSFX, .5f);
    }

    public void OnMove()
    {
        _audioManager.PlaySound(_monsterData.OnMoveSFX , .6f);
    }

    public void OnDeath()
    {
        _audioManager.PlaySound(_monsterData.OnDeathSFX);
    }

    public void OnHit()
    {
        _audioManager.PlaySound(_monsterData.OnHitSFX , .4f);
    }

    public void SetMonsterData(MonsterData monsterData)
    {
        _monsterData = monsterData;
    }

    public override void InitializeComponents()
    {
        base.InitializeComponents();
        _monsterCombatHandler.OnDeathEvent.AddListener(OnDeathState);
        _monsterCombatHandler.OnHitEvent.AddListener(() =>{SetHitTimer(); OnHit();});
    } 

    private void CalculateDistance()
    {
        if(_currentState == MonsterState.OnDeath)
        {
            return;
        }
        CheckPath();
        float distance = Vector3.Distance(_playerShip.position, transform.position);

        if (distance <= _attackingDistance)
        {
            _currentState = MonsterState.Attacking;
        }
        else
        {
            _currentState = MonsterState.Chasing;
        }
    }

    private void HandleMonsterState()
    {
        if(_currentState == MonsterState.OnDeath || _isHit)
        {
            return;
        }
        CheckPath();
        switch (_currentState)
        {
            case MonsterState.Attacking:
                OnAttackAnimation();
                break;
            case MonsterState.Chasing:
                OnChase();
                break;
            case MonsterState.Alert:
                PlayAnimation(_onAlert);
                break;
        }
    }

    private void OnDeathState()
    {
        PlayAnimation(_onDeath);
        _currentState = MonsterState.OnDeath;
    }

    private void MoveTowardsPlayer()
    {
        transform.position = Vector3.Lerp(transform.position, _playerShip.position, Time.deltaTime * _moveSpeed);
    }

    private void OnChase()
    {
        PlayAnimation(_onMoveForward);
        MoveTowardsPlayer();
    }

    private void RotateTowardsPlayer()
    {
        if (Quaternion.Angle(transform.rotation, _targetRotation) < 1f)
        {
            return;
        }
        Vector3 direction = _playerShip.position - transform.position;
        _targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, Time.deltaTime * _rotationSpeed);
    }

    private void CheckPath()
    {
        Vector3 direction = (_playerShip.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, _playerShip.position);

        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, distance, _obstacleLayer))
        {
            _currentState = MonsterState.Alert;
            return;
        }
    }

    private void OnAttackAnimation()
    {
        if(_weapon.activeSelf || _playerShip.GetComponent<ShipCombatHandler>().Health <= 0)
        {
            PlayAnimation(_onIdle);
            return;
        }
        PlayAnimation(_onAttack);
        RotateTowardsPlayer();
    }

    private void HitTimerCooldown()
    {
        if(!_isHit)
        {
            return;
        }

        _onHitTimer += _timerSpeed * Time.deltaTime;

        if(_onHitTimer >= _onHitTimerLimit)
        {
            _isHit = false;
            _onHitTimer = 0f;
        }
    }

    private void SetHitTimer()
    {
        _isHit = true;
        _onHitTimer = 0f;
        _animator.Play(_onHit, 0, 0f);
    }
}
