using UnityEngine;

public class AudioManager : UIBaseScript
{
    AudioManager _instance;
    AudioClipsSO _audioClipSO;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public override void Start()
    {
        base.Start();
        _audioClipSO = GameManager.instance.GetAudioClips();
        SetBGM(GameManager.instance.GetScene());
    }

    private void SetBGM(GameScene scene)
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

    public override void AddListener()
    {
        GameManager.instance.OnChangeSceneEvent.AddListener(() => { SetBGM(GameManager.instance.GetScene());});
    }
}
