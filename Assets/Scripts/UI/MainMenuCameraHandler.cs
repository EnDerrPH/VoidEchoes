using UnityEngine;

public class MainMenuCameraHandler : BaseScriptHandler
{
    [SerializeField] GameObject _hyperDrive;
    [SerializeField] Transform _nextSceneObj;
    [SerializeField] UIMainMenuHandler _uiMainMenuHandler;
    [SerializeField] float _speed;
    bool _isPlay;

    public override void Start()
    {
        base.Start();
        _uiMainMenuHandler.OnPlayEvent.AddListener(OnPlay);
    }

    void Update()
    {
        OnCameraMovement();
    }

    private void OnPlay()
    {
        _isPlay = true;
        _audioManager.GetAudioSource().volume = 1f;
        _audioManager.PlayOneShot(_gameManager.GetAudioClipData().HyperDriveSFX , _audioManager.GetAudioSource() , .6f , 1f);
        _hyperDrive.gameObject.SetActive(true);
    }

    public void OnCameraMovement()
    {
        if(!_isPlay)
        {
            return;
        }
        transform.position = Vector3.Lerp(transform.position, _nextSceneObj.position, _speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        _loadingSceneManager.LoadScene("CharacterScene");
        _gameManager.SetScene(GameScene.CharacterSelection);
    }
    
}
