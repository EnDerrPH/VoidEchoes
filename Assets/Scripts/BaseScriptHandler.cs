using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseScriptHandler : MonoBehaviour
{
    protected GameManager _gameManager;
    protected AudioManager _audioManager;
    protected AudioClipsSO _audioClipSO;
    protected LoadingSceneManager _loadingSceneManager;

    public virtual void OnEnable()
    {
        SetData();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public virtual void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public virtual void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (GameManager.instance != null)
        {
            InitializeComponents();
        }
    }

    public virtual void Start()
    {
        AddListener();
    }

    public virtual void AddListener()
    {

    }

    public virtual void InitializeComponents()
    {

    }
    private void SetData()
    {
        _gameManager = GameManager.instance;
        _audioManager = _gameManager.GetAudioManager();
        _loadingSceneManager = _gameManager.GetLoadingSceneManager();
        _audioClipSO = _gameManager.GetAudioClips();
    }
}
