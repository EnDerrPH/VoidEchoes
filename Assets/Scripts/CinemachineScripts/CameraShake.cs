using Unity.Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;
    bool _isShaking;
    float _shakeTimer;
    const float _speed = 1f;
    const float _shakeTimerLimit = 1f;
    CinemachineBasicMultiChannelPerlin _noise;

    void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        _noise = GetComponent<CinemachineBasicMultiChannelPerlin>();
    }

    void Update()
    {
       OnShake();
    }

    private void OnShake()
    {
        if(!_isShaking)
        {
            return;
        }
        _shakeTimer += _speed * Time.deltaTime;

        if(_shakeTimer >= _shakeTimerLimit)
        {
            SetNoise(0);
            _shakeTimer = 0f;
            _isShaking = false;
        }

    }

    public void ShakeCamera()
    {
        SetNoise(1);
        _isShaking = true;
    }

    private void SetNoise(float gain)
    {
        _noise.AmplitudeGain = gain;
        _noise.FrequencyGain = gain;
    }
}
