using UnityEngine;

public class AudioManager : BaseScriptHandler
{
    [SerializeField] AudioSource _audioSource;

    private void Awake()
    {
        if (_audioManager != null && _audioManager != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _audioManager = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public override void Start()
    {
        base.Start();
        SetBGM(_gameManager.GetScene());
    }

    public void SetBGM(GameScene scene)
    {
        switch(scene)
        {
            case GameScene.MainMenu:
            _audioSource.volume = .1f;
            _audioSource.clip = _audioClipSO.MainMenuBGM;
            break;
            case GameScene.CharacterSelection:
            _audioSource.volume = .1f;
            _audioSource.clip = _audioClipSO.CharacterSelectionBGM;
            break;
            case GameScene.Home:
            _audioSource.volume = 1f;
            _audioSource.clip = _audioClipSO.HomeBGM;
            break;
            case GameScene.Game:
           // _audioSource.clip = _audioClipSO.MapBGM;
            break;
        }
        _audioSource.Play();
    }

    public AudioSource GetAudioSource()
    {
        return _audioSource;
    }

    public virtual void PlayButtonSound(AudioClip audioClip, AudioSource audioSource)
    {
        audioSource.volume = .2f;
        audioSource.PlayOneShot(audioClip);
    }
}
