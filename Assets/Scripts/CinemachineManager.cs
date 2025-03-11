using UnityEngine;
using Unity.Cinemachine;

public class CinemachineManager : MonoBehaviour
{
    [SerializeField] CinemachineCamera _cinemachineCamera;
    [SerializeField] Transform _hologramView;

    public void SetCameraTarget(Transform target)
    {
        _cinemachineCamera.Follow = target;
    }

    public void SetTargetHologram()
    {
        _cinemachineCamera.Follow = _hologramView;
    }
    
}
