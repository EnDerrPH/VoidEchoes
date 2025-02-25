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
    [SerializeField] CharacterData _characterData;

    public static GameManager instance;

    public UnityEvent OnChangeSceneEvent;

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

    public PlayerData GetPlayerData()
    {
        return _playerData;
    }

    public void SetScene(GameScene scene)
    {
        _scene = scene;
    }

    public void SetCharacterData(CharacterData characterData)
    {
        _characterData = characterData;
    }
}
