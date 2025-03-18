using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class GameSceneHandler : BaseScriptHandler
{
   [SerializeField] Transform _terrainPos;
   [SerializeField] CinemachineManager _cinemachineManager;
   List<AudioClip> _audioAttackList = new List<AudioClip>();
   [SerializeField] GameObject _targetObj;
   [SerializeField] GameObject _crosshair;
   MapHandler _mapHandler;
   MapData _mapData;
   AudioClip _currentAttackModeSpeechSFX;
   Transform _startPosition;
   GameObject _playerShip;
   bool _isAttackmode;
   ShipAttackModeHandler _shipAttackModeHandler;

   public override void InitializeComponents()
   {
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
        }
    }

    private void InitializeShip()
    {
        Vector3 playerShipPosition = new Vector3(_startPosition.position.x , 1f , _startPosition.position.z);
        GameObject shipObject = Instantiate(_gameManager.GetCharacterData().Ship, playerShipPosition, _startPosition.rotation);
        _playerShip = shipObject;
        ShipCombatHandler shipCombatHandler = _playerShip.GetComponent<ShipCombatHandler>();
        foreach(GameObject fireAmmo in _playerShip.GetComponent<ShipAttackModeHandler>().FireAmmos)
        {
            BaseWeaponScript weapon = fireAmmo.GetComponent<BaseWeaponScript>();
            weapon.Damage = _gameManager.GetCharacterData().ShipDamage;
        }
        shipCombatHandler.Health = _gameManager.GetCharacterData().ShipHealth;
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
