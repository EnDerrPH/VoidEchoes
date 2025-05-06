using UnityEngine;

public class InteractableObjectManager : MonoBehaviour
{
    [SerializeField] protected bool _isInteracted;
    [SerializeField] protected bool _isPlayerColliding;
    protected AudioManager _audioManager;

    public virtual void Start()
    {
        _audioManager = AudioManager.instance;
    }

    public virtual void Update()
    {
        OnInteract();
    }

    public virtual void OnInteract()
    {
     
    }


    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            _isPlayerColliding = true;
        }
    }
    
    public virtual void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            _isPlayerColliding = false;
            _isInteracted = false;
        }
    }
}
