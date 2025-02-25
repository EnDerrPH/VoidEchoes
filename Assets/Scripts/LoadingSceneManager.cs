using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField] GameObject _loadingElements;
    [SerializeField] Slider _loadingBar;         // The slider used for progress display (optional)
    [SerializeField] TMP_Text _loadingText;          // Text to display loading status (optional)
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
        if (asyncOperation != null)
        {
            // Update the progress bar based on loading progress (0 to 0.9)
            if (asyncOperation.progress < 0.9f)
            {
                if (_loadingBar != null)
                {
                    _loadingBar.value = asyncOperation.progress / 0.9f;  // Normalize the progress
                }

                // Optionally update loading text
                if (_loadingText != null)
                {
                    _loadingText.text = "Loading... " + (asyncOperation.progress * 100f).ToString("F0") + "%";
                }
            }
            else
            {
                // Once progress reaches 0.9, set the progress bar to 100%
                if (_loadingBar != null)
                {
                    _loadingBar.value = 1f;
                }

                // Optionally update loading text to show 100%
                if (_loadingText != null)
                {
                    _loadingText.text = "Loading... 100%";
                    asyncOperation.allowSceneActivation = true;
                }
            }

             _timer += _timeSpeed * Time.deltaTime;
            if(_timer >= _timerLimit)
            {
                _loadingElements.SetActive(!asyncOperation.allowSceneActivation);
                _isLodingScreen = false;
                _timer = 0;
            }
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
    }
}
