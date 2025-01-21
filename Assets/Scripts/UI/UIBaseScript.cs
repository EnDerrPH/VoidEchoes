using UnityEngine;
using UnityEngine.UI;

public class UIBaseScript : MonoBehaviour
{
    public virtual void Start()
    {
        AddListener();
    }

    public virtual void AddListener()
    {

    }

    public virtual void PlayButtonSound(AudioClip audioClip, AudioSource audioSource)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
