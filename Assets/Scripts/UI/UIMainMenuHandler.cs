using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIMainMenuHandler : InitializeManager
{
    [SerializeField] Button _playButton;
    [SerializeField] Button _loadButton;
    [SerializeField] Button _settingsButton;
    [SerializeField] Button _exitButton;
    [SerializeField] Button []_mainMenuButtons;
    [SerializeField] Animator _animator;
   // [SerializeField] AudioManager _mainMenuAudioManger;
    public UnityEvent OnPlayEvent;

    public override void AddListener()
    {
        base.AddListener();
        _playButton.onClick.AddListener(OnPlayAnim);
        _loadButton.onClick.AddListener(OnLoad);
        _settingsButton.onClick.AddListener(OnSettings);
        _exitButton.onClick.AddListener(OnExit);
        foreach(Button button in _mainMenuButtons)
        {
            button.onClick.AddListener(OnUIButton);
        }
    }

    public void OnPlay()
    {
        OnPlayEvent?.Invoke();
    }
    
    private void OnPlayAnim()
    {
        _animator.Play("OnPlayAnim");
    }
    
    private void OnLoad()
    {

        
    }
    private void OnSettings()
    {
        
    }

    private void OnUIButton()
    {
        _audioManager.PlaySound(_audioManager.GetAudioClipData().MainMenuButtonSFX, .6f);
    }

    private void OnExit()
    {
        Application.Quit();
    }
}
