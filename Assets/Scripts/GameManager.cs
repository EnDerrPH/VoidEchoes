using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerData _playerData;
    [SerializeField] GameScene _scene;
    [SerializeField] CharacterDataList _characterDataList;
    [SerializeField] VFXData _VFXData;
    [SerializeField] MapList _mapList;
    [SerializeField] CharacterData _characterData;
    [SerializeField] MapData _mapData;
    [SerializeField] LoadingSceneManager _loadingSceneManager;

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

    private void OnEnable()
    {
        _scene = GameScene.MainMenu;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       OnChangeSceneEvent.Invoke();
    }

    public GameScene GetScene()
    {
        return _scene;
    }

    public CharacterData GetCharacterData()
    {
        return _characterData;
    }

    public LoadingSceneManager GetLoadingSceneManager()
    {
        return _loadingSceneManager;
    }


    public CharacterDataList GetCharacterDataList()
    {
        return _characterDataList;
    }

    public MapList GetMapList()
    {
        return _mapList;
    }

    public VFXData GetVFXData()
    {
        return _VFXData;
    }

    public PlayerData GetPlayerData()
    {
        return _playerData;
    }

    public MapData GetMapData()
    {
        return _mapData;
    }

    public void SetScene(GameScene scene)
    {
        _scene = scene;
    }

    public void SetCharacterData(CharacterData characterData)
    {
        _characterData = characterData;
    }

    public void SetMapSO(MapData mapData)
    {
        _mapData = mapData;
    }
}
