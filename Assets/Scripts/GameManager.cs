using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerData _playerData;
    [SerializeField] GameScene _scene;
    [SerializeField] UtilitiesSO _utilities;
    [SerializeField] AudioClipsSO _audioClipsSO;
    [SerializeField] CharacterDataList _characterDataList;
    [SerializeField] PlanetList _planetList;
    [SerializeField] MapList _mapList;
    [SerializeField] CharacterData _characterData;
    [SerializeField] MapsSO _map;

    public static GameManager instance;

    public UnityEvent OnChangeSceneEvent, OnGMLoadedEvent;

    private void Awake()
    {

        if (instance != null && instance != this)
        {
            // Destroy this object because it is a duplicate
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        OnGMLoadedEvent.Invoke();
        _scene = GameScene.MainMenu;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
    {
        OnChangeSceneEvent.Invoke();
    }

    public UtilitiesSO GetUtilities()
    {
        return _utilities;
    }

    public GameScene GetScene()
    {
        return _scene;
    }

    public AudioClipsSO GetAudioClips()
    {
        return _audioClipsSO;
    }

    public CharacterData GetCharacterData()
    {
        return _characterData;
    }


    public CharacterDataList GetCharacterDataList()
    {
        return _characterDataList;
    }

      public MapList GetMapList()
    {
        return _mapList;
    }

    public PlayerData GetPlayerData()
    {
        return _playerData;
    }

    public MapsSO GetMapSO()
    {
        return _map;
    }




    public void SetScene(GameScene scene)
    {
        _scene = scene;
    }

    public void SetCharacterData(CharacterData characterData)
    {
        _characterData = characterData;
    }

    public void SetMapSO(MapsSO mapSO)
    {
        _map = mapSO;
    }
}
