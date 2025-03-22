using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class GameSceneHandler : InitializeManager
{
   [SerializeField] Transform _terrainPos;
   [SerializeField] CinemachineManager _cinemachineManager;
   [SerializeField] GameObject _targetObj;
   [SerializeField] GameObject _crosshair;
   [SerializeField] List<ParticleSystem> _lootDropList = new List<ParticleSystem>();
   VFXData _VFXData;
   ShipCombatHandler _shipCombatHandler;
   MapHandler _mapHandler;
   MapData _mapData;
   Transform _startPosition;
   GameObject _playerShip;
   ShipAttackModeHandler _shipAttackModeHandler;

   public List<ParticleSystem> LootDropList {get => _lootDropList; set => _lootDropList = value;}

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
            MonsterProjectileHandler monsterProjectileHandler = monsterController.Weapon.GetComponent<MonsterProjectileHandler>();
            monsterProjectileHandler.PlayerShip = _playerShip.transform;
            monsterProjectileHandler.Damage = monsterData.Damage;
            monsterProjectileHandler.ParticleType = ParticleType.Monster;
            //SetMonsterCombat
            MonsterCombatHandler monsterCombatHandler = monsterObj.GetComponent<MonsterCombatHandler>();
            monsterCombatHandler.Health = monsterData.MonsterHealth;
            monsterCombatHandler.SetGameSceneHandler(this);
        }
    }

    private void InitializeShip()
    {
        Vector3 playerShipPosition = new Vector3(_startPosition.position.x , 1f , _startPosition.position.z);
        GameObject shipObject = Instantiate(_gameManager.GetCharacterData().Ship, playerShipPosition, _startPosition.rotation);
        _playerShip = shipObject;
        _shipCombatHandler = _playerShip.GetComponent<ShipCombatHandler>();
        foreach(GameObject fireAmmo in _playerShip.GetComponent<ShipAttackModeHandler>().FireAmmos)
        {
            WeaponManager weapon = fireAmmo.GetComponent<WeaponManager>();
            weapon.Damage = _gameManager.GetCharacterData().ShipDamage;
        }
        _shipCombatHandler.Health = _gameManager.GetCharacterData().ShipHealth;
        _shipCombatHandler.SetDeathSFX(_audioManager.GetAudioClipData().OnDeathSFX);
        _shipCombatHandler.enabled = true;
        ParticleSystem onHitVFX= Instantiate(_VFXData.ShipOnHitVFX, _playerShip.transform.position,  _playerShip.transform.rotation);
        ParticleSystem explosion01VFX = Instantiate(_VFXData.Explosion01VFX, _playerShip.transform.position,  _playerShip.transform.rotation);
        ParticleSystem explosion02VFX= Instantiate(_VFXData.Explosion02VFX, _playerShip.transform.position,  _playerShip.transform.rotation);
        _shipCombatHandler.SetVFX(onHitVFX, explosion01VFX, explosion02VFX);
    }

    IEnumerator InitializeShipScripts()
    {
        yield return new WaitForSeconds(1f);
        ShipMovementHandler shipMovementHandler = _playerShip.GetComponent<ShipMovementHandler>();
        shipMovementHandler.enabled = true;
        _shipAttackModeHandler = _playerShip.GetComponent<ShipAttackModeHandler>();
        _shipAttackModeHandler.SetGameObjects(_targetObj, _crosshair);
        _shipAttackModeHandler.enabled = true;
    }
}
