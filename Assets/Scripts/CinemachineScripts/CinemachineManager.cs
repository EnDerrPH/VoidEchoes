using UnityEngine;
using Unity.Cinemachine;

public class CinemachineManager : MonoBehaviour
{
    [SerializeField] CinemachineCamera _cinemachineCamera;
    [SerializeField] Transform _hologramView;
    [SerializeField] Transform _characterTransform;

    public void SetTargetCharacter()
    {
        _cinemachineCamera.Follow = _characterTransform;
    }

    public void SetCameraTarget(Transform target)
    {
        _cinemachineCamera.Follow = target;
    }

    public void SetTargetHologram()
    {
        _cinemachineCamera.Follow = _hologramView;
    }

    public void SetCharacterTransform(Transform character)
    {
        _characterTransform = character;
    }
    
}
