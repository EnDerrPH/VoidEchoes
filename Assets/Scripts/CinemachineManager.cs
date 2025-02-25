using UnityEngine;
using Unity.Cinemachine;

public class CinemachineManager : MonoBehaviour
{
    [SerializeField] CinemachineCamera _cinemachineCamera;
    Transform _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _cinemachineCamera.Follow = _player;
    }
    
}
