using UnityEngine;
using UnityEngine.UI;

public class UIBaseScript : MonoBehaviour
{
    protected AudioSource _audioSource;
    [SerializeField] protected LoadingSceneManager _loadingSceneManager;
    public virtual void Start()
    {
        AddListener();
        SetData();
    }

    public virtual void AddListener()
    {

    }

    public virtual void PlayButtonSound(AudioClip audioClip, AudioSource audioSource)
    {
        audioSource.volume = .2f;
        audioSource.PlayOneShot(audioClip);
    }

    private void SetData()
    {
        _audioSource = GameObject.FindAnyObjectByType<AudioSource>();
        _loadingSceneManager = GameObject.FindAnyObjectByType<LoadingSceneManager>();
    }
}
