using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCameraHandler : UIBaseScript
{
    [SerializeField] AudioClip _cameraMovementSFX;
    [SerializeField] AudioSource _audioSource;

    public void OnCameraMovement()
    {
        _audioSource.volume = 1f;
        PlayButtonSound(_cameraMovementSFX, _audioSource);
    }

    private void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    void OnTriggerEnter(Collider other)
    {
        LoadSceneByIndex(1);
    }
    
}
