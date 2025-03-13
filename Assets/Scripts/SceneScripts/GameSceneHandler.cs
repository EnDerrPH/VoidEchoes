using System.Collections;
using UnityEngine;

public class GameSceneHandler : BaseScriptHandler
{
   [SerializeField] Transform _terrainPos;
   [SerializeField] Transform _camera;
   [SerializeField] Transform _shipParentTransform;
   [SerializeField] CinemachineManager _cinemachineManager;
   Transform _startPosition;
   GameObject _playerShip;
   const float _yPosOffset = 20f;
   const float _zPosOffset = -30f;
   const float _xRotOffset = 20f;

   public override void InitializeComponents()
   {
        InitializeTerrain();
        InitializeShip();
        _cinemachineManager.SetCameraTarget(_playerShip.transform);
        StartCoroutine(enableShipScript());
   }

    private void InitializeTerrain()
    {
        Terrain terrain = _gameManager.GetMapSO().Map;
        Terrain terrainObj = Instantiate(terrain, terrain.transform.position, terrain.transform.rotation , _terrainPos);
        terrainObj.gameObject.SetActive(true);
        _startPosition = terrainObj.GetComponent<MapHandler>().StartingPosition;
    }

    private void InitializeShip()
    {
        _shipParentTransform.position = _startPosition.position;
        Vector3 playerShipPosition = new Vector3(_shipParentTransform.position.x , 1f , _shipParentTransform.position.z);
        GameObject shipObject = Instantiate(_gameManager.GetCharacterData().Ship, playerShipPosition, _shipParentTransform.rotation);
        _playerShip = shipObject;
        _playerShip.transform.SetParent(_shipParentTransform);
        ShipController shipcontroller = _playerShip.GetComponent<ShipController>();
        shipcontroller.ParentTransform = _shipParentTransform;
    }

    IEnumerator enableShipScript()
    {
        yield return new WaitForSeconds(1f);
        _playerShip.GetComponent<ShipController>().enabled = true;
    }
}
