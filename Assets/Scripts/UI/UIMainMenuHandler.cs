using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;


public class UIMainMenuHandler : BaseScriptHandler
{
    [SerializeField] Button _playButton;
    [SerializeField] Button _loadButton;
    [SerializeField] Button _settingsButton;
    [SerializeField] Button _exitButton;
    [SerializeField] Button []_mainMenuButtons;
    [SerializeField] PlayableDirector _playerableDirector;

    public override void AddListener()
    {
        base.AddListener();
        _playButton.onClick.AddListener(OnPlay);
        _loadButton.onClick.AddListener(OnLoad);
        _settingsButton.onClick.AddListener(OnSettings);
        _exitButton.onClick.AddListener(OnExit);
        foreach(Button button in _mainMenuButtons)
        {
            button.onClick.AddListener(() => {_audioManager.PlayButtonSound(_audioClipSO.MainMenuButtonSFX, _audioManager.GetAudioSource()); });
        }
    }
    
    private void OnPlay()
    {
        _playerableDirector.Play();
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
