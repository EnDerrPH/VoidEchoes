using UnityEngine;

public class BaseTriggerManager : MonoBehaviour
{

    [SerializeField] protected CharacterController _characterController;

    public virtual void Start()
    {
        _characterController = GameObject.FindAnyObjectByType<CharacterController>();
    }
}
