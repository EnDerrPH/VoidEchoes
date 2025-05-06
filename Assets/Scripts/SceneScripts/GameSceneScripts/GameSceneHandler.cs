using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class GameSceneHandler : InitializeManager
{
    [SerializeField] Transform _terrainPos;
    [SerializeField] CinemachineManager _cinemachineManager;
    [SerializeField] GameObject _targetObj;
    [SerializeField] GameObject _crosshair;
    [SerializeField] List<ParticleSystem> _lootDropPool = new List<ParticleSystem>();
    [SerializeField] List<ParticleSystem> _lootProjectilePool = new List<ParticleSystem>();
    [SerializeField] MapHandler _mapHandler;
    [SerializeField] UIGameSceneHandler _UIGameSceneHandler;
    [SerializeField] MapData _mapData;
    VFXData _VFXData;
    Transform _startPosition;
    GameObject _playerShip;

    public List<ParticleSystem> LootDropPool {get => _lootDropPool;}
    public List<ParticleSystem> LootProjectilePool {get => _lootProjectilePool;}

   public override void InitializeComponents()
   {
        _VFXData = _gameManager.GetVFXData();
        InitializeTerrain();
        InitializeShip();
        InitializeMonsters();
        _cinemachineManager.SetCameraTarget(_playerShip.transform);
        StartCoroutine(InitializeShipScripts());
   }

    private void InitializeTerrain()
    {
        _mapData = _gameManager.GetMapData();
        Terrain terrain = _mapData.Map;
        Terrain terrainObj = Instantiate(terrain, terrain.transform.position, terrain.transform.rotation , _terrainPos);
        terrainObj.gameObject.SetActive(true);
        _mapHandler = terrainObj.GetComponent<MapHandler>();
        _startPosition = _mapHandler.StartingPosition;
    }

    private void InitializeMonsters()
    {
        for(int i = 0 ; i < _mapHandler.MonsterPos.Length ; i++)
        {
            MonsterData monsterData = _mapData.MonsterData;
            //SetMonster
            GameObject monsterObj = Instantiate(monsterData.Monster, _mapHandler.MonsterPos[i].position, _mapHandler.MonsterPos[i].rotation);
            MonsterController monsterController = monsterObj.GetComponent<MonsterController>();
            monsterController.SetMonsterData(monsterData);
            monsterController.PlayerShip = _playerShip.transform;
            monsterController.MoveSpeed = monsterData.MoveSpeed;
            //SetMonsterWeapon
            GameObject monsterWeapon = Instantiate(monsterData.Weapon.gameObject, _mapHandler.MonsterPos[i].position, _mapHandler.MonsterPos[i].rotation, monsterObj.transform);
            monsterController.Weapon = monsterWeapon;
            ProjectileHandler monsterProjectileHandler = monsterController.Weapon.GetComponent<ProjectileHandler>();
            monsterProjectileHandler.PlayerShip = _playerShip.transform;
            monsterProjectileHandler.Damage = monsterData.Damage;
            monsterProjectileHandler.ParticleType = ParticleType.Monster;
            //SetMonsterCombat
            MonsterCombatHandler monsterCombatHandler = monsterObj.GetComponent<MonsterCombatHandler>();
            monsterCombatHandler.Health = monsterData.MonsterHealth;
            monsterCombatHandler.PlayerShip = _playerShip.transform;
            ParticleSystem lootDrop = Instantiate(_gameManager.GetVFXData().LootDropVFX, monsterCombatHandler.transform.position, monsterCombatHandler.transform.rotation);
            _lootDropPool.Add(lootDrop);
            ParticleSystem lootProjectile = Instantiate(_gameManager.GetVFXData().LootDropProjectileVFX, monsterCombatHandler.transform.position, monsterCombatHandler.transform.rotation);
            lootProjectile.GetComponent<ProjectileHandler>().PlayerShip = _playerShip.transform;
            _lootProjectilePool.Add(lootProjectile);
            monsterCombatHandler.SetGameSceneHandler(this);
        }
    }

    public ParticleSystem PlayLootDrop(List<ParticleSystem> particlePool, ParticleSystem particle, Vector3 position)
    {
        // Find an available AudioSource
        ParticleSystem availableParticle = particlePool.Find(source => !source.gameObject.activeSelf);

        if (availableParticle == null)
        {
            // Optionally expand the pool
            availableParticle = CreateParticle(particle, particlePool);
        }
        // Play the sound
        availableParticle.gameObject.transform.position = position;
        availableParticle.gameObject.SetActive(true);
        availableParticle.Play();

        return availableParticle;
    }

    private ParticleSystem CreateParticle(ParticleSystem particle, List<ParticleSystem> particlePool)
    {
        ParticleSystem newSource = Instantiate(particle, transform);
        particlePool.Add(newSource);
        return newSource;
    }

    private void InitializeShip()
    {
        CharacterData characterData = _gameManager.GetCharacterData();
        Vector3 playerShipPosition = new Vector3(_startPosition.position.x , 1f , _startPosition.position.z);
        GameObject shipObject = Instantiate(characterData.Ship, playerShipPosition, _startPosition.rotation);
        shipObject.GetComponent<Collider>().enabled = true;
        _playerShip = shipObject;
        ShipCombatHandler shipCombatHandler = _playerShip.GetComponent<ShipCombatHandler>();
        ShipMovementHandler shipMovementHandler = _playerShip.GetComponent<ShipMovementHandler>();
        foreach(GameObject fireAmmo in _playerShip.GetComponent<ShipAttackModeHandler>().FireAmmos)
        {
            WeaponManager weapon = fireAmmo.GetComponent<WeaponManager>();
            weapon.Damage = characterData.ShipDamage;
        }
        shipCombatHandler.Health = characterData.ShipHealth;
        shipCombatHandler.SetDeathSFX(_audioManager.GetAudioClipData().OnDeathSFX);
        shipCombatHandler.enabled = true;
        ParticleSystem onHitVFX= Instantiate(_VFXData.ShipOnHitVFX, _playerShip.transform.position,  _playerShip.transform.rotation);
        ParticleSystem explosion01VFX = Instantiate(_VFXData.Explosion01VFX, _playerShip.transform.position,  _playerShip.transform.rotation);
        ParticleSystem explosion02VFX= Instantiate(_VFXData.Explosion02VFX, _playerShip.transform.position,  _playerShip.transform.rotation);
        shipCombatHandler.SetVFX(onHitVFX, explosion01VFX, explosion02VFX);
        _UIGameSceneHandler.SetHpText(characterData.ShipHealth.ToString());
        _UIGameSceneHandler.SetFuelText(characterData.ShipFuel.ToString());
        shipMovementHandler.SetShipFuel(characterData.ShipFuel);
        shipMovementHandler.SetUIGameHandler(_UIGameSceneHandler);
    }

    IEnumerator InitializeShipScripts()
    {
        yield return new WaitForSeconds(1f);
        ShipMovementHandler shipMovementHandler = _playerShip.GetComponent<ShipMovementHandler>();
        shipMovementHandler.enabled = true;
        ShipAttackModeHandler shipAttackModeHandler = _playerShip.GetComponent<ShipAttackModeHandler>();
        shipAttackModeHandler.SetGameObjects(_targetObj, _crosshair);
        shipAttackModeHandler.enabled = true;
    }
}
