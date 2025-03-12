using UnityEngine;

public class MainMenuCameraHandler : BaseScriptHandler
{
    [SerializeField] GameObject _hyperDrive;

    public void OnCameraMovement()
    {
        _audioManager.GetAudioSource().volume = 1f;
        _audioManager.PlayButtonSound(_gameManager.GetAudioClips().HyperDriveSFX , _audioManager.GetAudioSource());
        _hyperDrive.gameObject.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        _loadingSceneManager.LoadScene("CharacterScene");
        _gameManager.SetScene(GameScene.CharacterSelection);
    }
    
}
