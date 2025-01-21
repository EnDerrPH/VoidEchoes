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
    [SerializeField] AudioSource _audioSourceBGM;
    [SerializeField] AudioSource _audioSourceUI;
    [SerializeField] AudioClip _buttonSFX;
    [SerializeField] AudioClip _mainMenuBGM;
    [SerializeField] PlayableDirector _playerableDirector;

    public override void Start()
    {
        base.Start();
        _audioSourceBGM.clip = _mainMenuBGM;
        _audioSourceBGM.Play();
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
            button.onClick.AddListener(() => {PlayButtonSound(_buttonSFX, _audioSourceUI); });
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
