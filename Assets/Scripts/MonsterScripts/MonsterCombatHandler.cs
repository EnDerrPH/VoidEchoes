using UnityEngine;

public class MonsterCombatHandler : CombatManager
{
    Transform _playerShip;
    GameSceneHandler _gameSceneHandler;
    GameObject _currentLootDrop;
    VFXData _vfxData;
    bool _hasLoot;
    public Transform PlayerShip {get => _playerShip ; set => _playerShip = value;}

    public override void Start()
    {
        base.Start();
        _vfxData = _gameManager.GetVFXData();
    }

    private void Update()
    {
        if(_health > 0 || _hasLoot)
        {
            return;
        }
        float distance = Vector3.Distance(transform.position , _playerShip.transform.position);
        if(distance <= 100)
        {
            _currentLootDrop.SetActive(false);
            _gameSceneHandler.PlayLootDrop(_gameSceneHandler.LootProjectilePool, _vfxData.LootDropProjectileVFX , transform.position);
            _hasLoot = true;
        }
    }

    public override void AfterDeath()
    {
        base.AfterDeath();
        _rb.constraints = RigidbodyConstraints.FreezeAll;
        GetComponent<BoxCollider>().enabled = false;
        _currentLootDrop = _gameSceneHandler.PlayLootDrop(_gameSceneHandler.LootDropPool, _vfxData.LootDropVFX , transform.position).gameObject;
    }

    public void SetGameSceneHandler(GameSceneHandler gameSceneHandler)
    {
        _gameSceneHandler = gameSceneHandler;
    }

    private void OnParticleCollision(GameObject other)
    {
        if(_health <= 0)
        {
            return;
        }
        if (other.GetComponent<WeaponManager>().ParticleType == ParticleType.Player)
        {
            OnHit(other.GetComponent<WeaponManager>().Damage); 
        }
    }
}
