using UnityEngine;

public class BaseTriggerManager : MonoBehaviour
{
    [SerializeField] protected HomeSceneHandler _homeSceneHandler;
    [SerializeField] protected CharacterMovement _characterMovement;

    void Start()
    {
        _homeSceneHandler.PlayerHasSpawnEvent.AddListener(SetCharacterMovement);
    }

    void Update()
    {
        SetCharacterMovement();
    }

    private void SetCharacterMovement()
    {
        if(_characterMovement != null)
        {
            return;
        }
        _characterMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
    }
}
