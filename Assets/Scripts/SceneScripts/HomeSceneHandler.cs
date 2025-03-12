using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class HomeSceneHandler : BaseScriptHandler
{
   [SerializeField] private Transform _playerPos;
   [SerializeField] private Transform _shipPos;
   [SerializeField] private Transform _mapParentTransform;
   [SerializeField] private TMP_Text _mapName;
   [SerializeField] private Button _rightButton;
   [SerializeField] private Button _leftButton;
   [SerializeField] private Button _confirmButton;
   [SerializeField] private GameObject _mapUI;
   [SerializeField] private List<MapsSO> _mapList = new List<MapsSO>();
   [SerializeField] private List<Terrain> _terrainList = new List<Terrain>();
   [SerializeField] protected CharacterController _characterController;
   [SerializeField] CanvasGroup _canvasGroup;
   [SerializeField] float _elapsedTime = 0f;
   [SerializeField] CinemachineManager _cinemachineManager;
   bool _onMapDevice;
   const float _fadeDuration = 3f;
   const float _alphaIncreaseRate = .4f;
   const float _targetAlpha = 1f;
   int _currentMapNumber;

   public UnityEvent PlayerHasSpawnEvent;

   void Update()
   {
      SetAlpha();
   }

   public override void AddListener()
   {
      _rightButton.onClick.AddListener(NextMap);
      _leftButton.onClick.AddListener(PreviousMap);
      _confirmButton.onClick.AddListener(SetTerrain);
   }

   public override void InitializeComponents()
   {
      InitializeCharacter();
      InitializeShip();
      InitializeMaps();
      InitializeCinemachine();
   }

   private void NextMap()
   {
      _terrainList[_currentMapNumber].gameObject.SetActive(false);
      _currentMapNumber += 1;
      if(_currentMapNumber >= _mapList.Count)
      {
         _currentMapNumber = 0;
      }
      _terrainList[_currentMapNumber].gameObject.SetActive(true);
      _mapName.text = _mapList[_currentMapNumber].MapName;
   }

   private void PreviousMap()
   {
      _terrainList[_currentMapNumber].gameObject.SetActive(false);
      _currentMapNumber -= 1;
      if(_currentMapNumber < 0)
      {
         _currentMapNumber = _mapList.Count -1;
      }
      _terrainList[_currentMapNumber].gameObject.SetActive(true);
      _mapName.text = _mapList[_currentMapNumber].MapName;
   }

   private void InitializeCharacter()
   {
      GameObject character = _gameManager.GetCharacterData().Character;
      GameObject characterObj = Instantiate(character, _playerPos.position, character.transform.rotation);
      _characterController = characterObj.GetComponent<CharacterController>();
      PlayerHasSpawnEvent.Invoke();
   }

   private void InitializeShip()
   {
      GameObject ship = _gameManager.GetCharacterData().Ship;
      GameObject shipObj = Instantiate(ship, _shipPos.position, ship.transform.rotation);
   }

   private void InitializeCinemachine()
   {
      _cinemachineManager.SetCameraTarget(_characterController.transform);
      _characterController.OnMapDeviceEvent.AddListener(() =>{ SetIsActive(true); _cinemachineManager.SetTargetHologram();});
      _characterController.OnPlayerControlEvent.AddListener(() =>{ DeactivateDevice(); _cinemachineManager.SetCameraTarget(_characterController.transform);});
   }

   private void InitializeMaps()
   {
      MapList mapList = _gameManager.GetMapList();
      for(int i = 0 ; i <= mapList.GetMapSOList().Count -1; i++)
      {
         MapsSO MapOBJ = mapList.GetMapSOList()[i];
         _mapList.Add(MapOBJ);
         Terrain terrain = Instantiate(MapOBJ.Map, _mapParentTransform.position, _mapParentTransform.transform.rotation, _mapParentTransform);
         _terrainList.Add(terrain);
         if(i <= 0 )
         {
            _mapName.text = MapOBJ.MapName;
            _currentMapNumber = i;
            terrain.gameObject.SetActive(true);
         }
      }
   }

   private void SetTerrain()
   {
      _gameManager.SetMapSO(_gameManager.GetMapList().GetMapSOList()[_currentMapNumber]);
      _loadingSceneManager.LoadScene("GameScene");
      _gameManager.SetScene(GameScene.Game);
   }

   private void SetAlpha()
   {
      if (_onMapDevice)
      {
         _elapsedTime += Time.deltaTime;
   
         if (_elapsedTime >= _fadeDuration)
         {
               _canvasGroup.alpha = Mathf.Clamp(_canvasGroup.alpha + _alphaIncreaseRate * Time.deltaTime, 0f, _targetAlpha);
               if(_canvasGroup.alpha >= 1)
               {
                  _onMapDevice = false;
               }
         }
      }
   }

   private void SetIsActive(bool isActive)
   {
      _onMapDevice = isActive;
   }

   private void DeactivateDevice()
   {
      _canvasGroup.alpha = 0;
      _elapsedTime = 0f;
   }
}

