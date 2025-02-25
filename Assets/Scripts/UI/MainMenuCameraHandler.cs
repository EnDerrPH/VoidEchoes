using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCameraHandler : UIBaseScript
{
    [SerializeField] GameObject _hyperDrive;

    public void OnCameraMovement()
    {
        _audioSource.volume = 1f;
        PlayButtonSound(GameManager.instance.GetAudioClips().HyperDriveSFX , _audioSource);
        _hyperDrive.gameObject.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        _loadingSceneManager.LoadScene("CharacterScene");
        GameManager.instance.SetScene(GameScene.CharacterSelection);
    }
    
}
