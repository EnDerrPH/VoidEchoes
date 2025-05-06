using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource _audioPrefab;
    [SerializeField] AudioClipData _audioClipData;
    [SerializeField] AudioMixer _audioMixer;
    const int _poolSize = 2; 
    AudioSource _audioSourceBGM;
    private List<AudioSource> _audioPool = new List<AudioSource>();

    public static AudioManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        for (int i = 0; i < _poolSize; i++)
        {
            CreateAudioPool();
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
       SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public  void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetBGM(GameManager.instance.GetScene());
    }

    void Start()
    {
        CreateAudioPool();
        SetBGM(GameManager.instance.GetScene());
    }

    public AudioClipData GetAudioClipData()
    {
        return _audioClipData;
    }

    public void ResetAllAudioSource()
    {
        foreach (AudioSource audioSource in _audioPool)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            if (audioSource.clip != null)
            {
                audioSource.clip = null;
            }

            if (audioSource.loop)
            {
                audioSource.loop = false;
            }
        }
    }

    public AudioSource PlaySound(AudioClip clip, float volume = 1f)
    {
        // Find an available AudioSource
        AudioSource availableSource = _audioPool.Find(source => !source.isPlaying);

        if (availableSource == null || availableSource == _audioSourceBGM)
        {
            // Optionally expand the pool
            availableSource = CreateAudioPool();
        }
        // Play the sound
        availableSource.clip = clip;
        availableSource.volume = volume;
        availableSource.Play();

        return availableSource;
    }

    public AudioSource SetAudioSource()
    {
        return CreateAudioPool();
    }

    public void AmplifySound(float factor)
    {
        float dB = Mathf.Log10(factor) * 20f; // Convert factor to decibels
        _audioMixer.SetFloat("SFXVolume", dB);
    }


    private void SetBGM(GameScene scene)
    {
        switch(scene)
        {
            case GameScene.MainMenu:
            PlayBGM(_audioClipData.MainMenuBGM);
            break;
            case GameScene.CharacterSelection:
            PlayBGM(_audioClipData.CharacterSelectionBGM);
            break;
            case GameScene.Home:
            PlayBGM(_audioClipData.HomeBGM);
            break;
            case GameScene.Game:
            PlayBGM(_audioClipData.GameBGM);
            break;
        }
    }

    private void PlayBGM(AudioClip audioClip)
    {
        if(_audioSourceBGM == null)
        {
            _audioSourceBGM = PlaySound(audioClip);
            return;
        }

        _audioSourceBGM.clip = audioClip;
        _audioSourceBGM.loop = enabled;
        _audioSourceBGM.Play();
    }

    private AudioSource CreateAudioPool()
    {
        AudioSource newSource = Instantiate(_audioPrefab, transform);
        _audioPool.Add(newSource);
        return newSource;
    }
}
