using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;


public class UIMainMenuHandler : UIBaseScript
{
    [SerializeField] Button _playButton;
    [SerializeField] Button _loadButton;
    [SerializeField] Button _settingsButton;
    [SerializeField] Button _exitButton;
    [SerializeField] Button []_mainMenuButtons;
    [SerializeField] AudioSource _audioSourceUI;
    [SerializeField] PlayableDirector _playerableDirector;
    AudioClipsSO _audioClipSO;

    public override void Start()
    {
        _audioClipSO = GameManager.instance.GetAudioClips();
        base.Start();   
    }

    public override void AddListener()
    {
        base.AddListener();
        _playButton.onClick.AddListener(OnPlay);
        _loadButton.onClick.AddListener(OnLoad);
        _settingsButton.onClick.AddListener(OnSettings);
        _exitButton.onClick.AddListener(OnExit);
        foreach(Button button in _mainMenuButtons)
        {
            button.onClick.AddListener(() => {PlayButtonSound(_audioClipSO.MainMenuButtonSFX, _audioSourceUI); });
        }
    }
    
    private void OnPlay()
    {
        _playerableDirector.Play();
        GameManager.instance.SetScene(GameScene.Home);
    }
    private void OnLoad()
    {

        
    }
    private void OnSettings()
    {
        
    }
    private void OnExit()
    {
        
        Application.Quit();
    }
}
