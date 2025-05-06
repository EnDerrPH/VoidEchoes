using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class HomeSceneHandler : InitializeManager
{
   [SerializeField] private Transform _playerPos;
   [SerializeField] private Transform _shipPos;
   [SerializeField] private Transform _planetContent;
   [SerializeField] CinemachineManager _cinemachineManager;
   [SerializeField] UIHomeSceneHandler _UIHomeSceneHandler;
   [SerializeField] Transform _orbitCenter; 
   [SerializeField] MapDeviceHandler _mapDeviceHandler;
   protected CharacterController _characterController;
   public UnityEvent PlayerHasSpawnEvent;

   public override void InitializeComponents()
   {
      InitializeCharacter();
      InitializeShip();
      InitializePlanets();
   }

   private void InitializeCharacter()
   {
      GameObject character = _gameManager.GetCharacterData().Character;
      GameObject characterObj = Instantiate(character, _playerPos.position, character.transform.rotation);
      _characterController = characterObj.GetComponent<CharacterController>();
      _characterController.OnPlayerControlEvent += () => _cinemachineManager.SetTargetCharacter();
      _cinemachineManager.SetCharacterTransform(characterObj.transform);
      _cinemachineManager.SetTargetCharacter();
      PlayerHasSpawnEvent.Invoke();
   }

   private void InitializeShip()
   {
      GameObject ship = _gameManager.GetCharacterData().Ship;
      GameObject shipObj = Instantiate(ship, _shipPos.position, ship.transform.rotation);
   }

   private void InitializePlanets()
   {
      List<PlanetData> planetList = _gameManager.GetPlanetDataList().GetPlanetList();

      foreach(PlanetData planetData in planetList)
      {
         GameObject planetObj = Instantiate(planetData.Planet, _orbitCenter.transform.position, Quaternion.identity , _planetContent);
         PlanetHandler planetHandler = planetObj.GetComponent<PlanetHandler>();
         planetHandler.SetOrbitState(_orbitCenter);
         planetHandler.SetPlanetData(planetData);
         planetHandler.SetUIHomeScreenHandler(_UIHomeSceneHandler);
         _mapDeviceHandler.SetPlanetList(planetObj);
         _mapDeviceHandler.SetCinemachineManager(_cinemachineManager);
      }
   }
}

