using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _mainMenuBGM;
    [SerializeField] AudioClip _homeBGM;
    [SerializeField] AudioClip _gameBGM;
    AudioManager _instance;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            // Destroy this object because it is a duplicate
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        _gameManager = GameManager.instance;
        AddListener();
        SetBGM(_gameManager.GetScene());
    }

    private void SetBGM(GameScene scene)
    {
        switch(scene)
        {
            case GameScene.MainMenu:
            _audioSource.volume = .1f;
            _audioSource.clip = _mainMenuBGM;
            break;
            case GameScene.Home:
            _audioSource.volume = 1f;
            _audioSource.clip = _homeBGM;
            break;
            case GameScene.Map:
            _audioSource.clip = _gameBGM;
            break;
        }
        _audioSource.Play();
    }

    private void AddListener()
    {
        _gameManager.OnChangeSceneEvent.AddListener(() => { SetBGM(_gameManager.GetScene());});
    }
}
