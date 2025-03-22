using UnityEngine;

public class MainMenuCameraHandler : InitializeManager
{
    [SerializeField] GameObject _hyperDrive;
    [SerializeField] Transform _nextSceneObj;
    [SerializeField] MainMenuHandler _mainMenuHandler;
    [SerializeField] float _speed;
    AudioSource _audioHyperDrive;
    bool _isPlay;

    public override void Start()
    {
        base.Start();
        _mainMenuHandler.OnPlayEvent.AddListener(OnPlay);
    }

    void Update()
    {
        OnCameraMovement();
    }

    private void OnPlay()
    {
        _isPlay = true;
        _audioHyperDrive = _audioManager.PlaySound(_audioManager.GetAudioClipData().HyperDriveSFX);
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
        _audioHyperDrive.Stop();
        _loadingSceneManager.LoadScene("CharacterScene");
    }
    
}
