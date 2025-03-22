using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializeManager : MonoBehaviour
{
    [SerializeField] protected GameManager _gameManager;
    [SerializeField] protected AudioManager _audioManager;
    protected LoadingSceneManager _loadingSceneManager;

    public virtual void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public virtual void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public virtual void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetData();
        InitializeComponents(); 
    }

    public virtual void Start()
    {
        SetData();
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
        _audioManager = AudioManager.instance;
        _loadingSceneManager = _gameManager.GetLoadingSceneManager();
    }
}
