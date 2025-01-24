using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameScene _scene;
    [SerializeField] UtilitiesSO _utilities;

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

    public void SetScene(GameScene scene)
    {
        _scene = scene;
    }
}
