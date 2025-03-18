using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField] GameObject _loadingElements;
    bool _isLodingScreen;
    [SerializeField] float _timer = 0f;
    float _timerLimit = 1f;
    float _timeSpeed = 1f;
    private AsyncOperation asyncOperation;  // Reference to the async operation

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(!_isLodingScreen)
        {
            return;
        }
            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }

            _timer += _timeSpeed * Time.deltaTime;
            if(_timer >= _timerLimit)
            {
                _loadingElements.SetActive(!asyncOperation.allowSceneActivation);
                _isLodingScreen = false;
                _timer = 0;
            }
    }

    public void LoadScene(string sceneName)
    {
        _isLodingScreen = true;
        // Show the loading screen
        _loadingElements.SetActive(true);

        // Begin loading the scene asynchronously
        asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        // Don't allow the scene to activate until it's fully loaded
        asyncOperation.allowSceneActivation = false;
        SetScene(sceneName);
    }

    private void SetScene(string sceneName)
    {
        GameManager _gameManager = GameManager.instance;
        switch(sceneName)
        {
            case "MenuScene":
            _gameManager.SetScene(GameScene.MainMenu);
            break;
            case "CharacterScene":
            _gameManager.SetScene(GameScene.CharacterSelection);
            break;
            case "Home":
            _gameManager.SetScene(GameScene.Home);
            break;
            case "GameScene":
            _gameManager.SetScene(GameScene.Game);
            break;
        }
    }
}
