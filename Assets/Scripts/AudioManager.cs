using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : InitializeManager
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioSource _audioSFX;
    [SerializeField] AudioSource _audioSpeech;
    [SerializeField]  AudioMixer _audioMixer;

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

    public override void InitializeComponents()
    {
        base.InitializeComponents();
        SetBGM(_gameManager.GetScene());
    }

    public override void Start()
    {
        SetBGM(_gameManager.GetScene());
    }

    public void SetBGM(GameScene scene)
    {
        switch(scene)
        {
            case GameScene.MainMenu:
            _audioSource.clip = _audioClipData.MainMenuBGM;
            break;
            case GameScene.CharacterSelection:
            _audioSource.clip = _audioClipData.CharacterSelectionBGM;
            break;
            case GameScene.Home:
            _audioSource.clip = _audioClipData.HomeBGM;
            break;
            case GameScene.Game:
           // _audioSource.clip = _audioClipData.MapBGM;
            break;
        }
        _audioSource.volume = .5f;
        _audioSource.Play();
    }

    public AudioSource GetAudioSource()
    {
        return _audioSource;
    }

    public AudioSource GetAudioSFX()
    {
        return _audioSFX;
    }

    public AudioSource GetAudioSpeech()
    {
        return _audioSpeech;
    }

    public virtual void PlayOneShot(AudioClip audioClip, AudioSource audioSource , float volume , float mixerVolume)
    {
        audioSource.volume = volume;
        SetMixerVolume(mixerVolume);
        audioSource.PlayOneShot(audioClip);
    }

    private void SetMixerVolume(float multiplier)
    {
        float volume = Mathf.Log10(multiplier) * 20;
        _audioMixer.SetFloat("SFXVolume", volume); 
    }
}
