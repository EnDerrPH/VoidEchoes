using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class HomeSceneHandler : UIBaseScript
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

   int _currentMapNumber;

   public UnityEvent PlayerHasSpawnEvent;

   private void Awake()
   {
      InitializePlayer();
   }

   public override void Start()
   {
      InitializeMaps();
      base.Start();
   }

   public override void AddListener()
   {
      _rightButton.onClick.AddListener(NextMap);
      _leftButton.onClick.AddListener(PreviousMap);
      _confirmButton.onClick.AddListener(SetTerrain);
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

   private void InitializePlayer()
   {
      GameObject character = GameManager.instance.GetCharacterData().Character;
      GameObject ship = GameManager.instance.GetCharacterData().Ship;
      GameObject characterObj = Instantiate(character, _playerPos.position, character.transform.rotation);
      _characterController = characterObj.GetComponent<CharacterController>();
      GameObject shipObj = Instantiate(ship, _shipPos.position, ship.transform.rotation);
      PlayerHasSpawnEvent.Invoke();
   }

   private void InitializeMaps()
   {
      MapList mapList = GameManager.instance.GetMapList();
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
      GameManager.instance.SetMapSO(GameManager.instance.GetMapList().GetMapSOList()[_currentMapNumber]);
      _loadingSceneManager.LoadScene("GameScene");
      GameManager.instance.SetScene(GameScene.Game);
   }
}
