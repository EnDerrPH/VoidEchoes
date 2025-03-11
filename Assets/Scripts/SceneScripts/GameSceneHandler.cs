using System;
using UnityEngine;

public class GameSceneHandler : MonoBehaviour
{
   [SerializeField] Transform _terrainPos;
   [SerializeField] Transform _camera;
   Transform _startPosition;
   GameObject _playerShip;
   const float _yPosOffset = 20f;
   const float _zPosOffset = -30f;
   const float _xRotOffset = 20f;

   private void Start()
   {
        InitializeTerrain();
        InitializeShip();
        SetCamera();
   }

    private void InitializeTerrain()
    {
        Terrain terrain = GameManager.instance.GetMapSO().Map;
        Terrain terrainObj = Instantiate(terrain, terrain.transform.position, terrain.transform.rotation , _terrainPos);
        terrainObj.gameObject.SetActive(true);
        _startPosition = terrainObj.GetComponent<MapHandler>().StartingPosition;
    }

    private void InitializeShip()
    {
        GameObject shipObject = Instantiate(GameManager.instance.GetCharacterData().Ship, _startPosition.position, _startPosition.rotation);
        _playerShip = shipObject;
    }

    private void SetCamera()
    {
        _camera.position = new Vector3(_playerShip.transform.position.x, _playerShip.transform.position.y +  _yPosOffset, _playerShip.transform.position.z + _zPosOffset);
        _camera.rotation = Quaternion.Euler(_xRotOffset, 0, 0);
        _camera.SetParent(_playerShip.transform);
    }
}
