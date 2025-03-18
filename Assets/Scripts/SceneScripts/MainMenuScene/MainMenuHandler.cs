using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MainMenuHandler : InitializeManager
{
    [SerializeField] Button _playButton;
    [SerializeField] Button _loadButton;
    [SerializeField] Button _settingsButton;
    [SerializeField] Button _exitButton;
    [SerializeField] Button []_mainMenuButtons;
    [SerializeField] Animator _animator;
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
            button.onClick.AddListener(() => {_audioManager.PlayOneShot(_audioClipData.MainMenuButtonSFX, _audioManager.GetAudioSource() , .5f , 1f); });
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
    private void OnExit()
    {
        
        Application.Quit();
    }
}
