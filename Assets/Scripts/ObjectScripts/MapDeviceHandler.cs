using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MapDeviceHandler : InteractableObjectManager
{
    
    [SerializeField] Transform _topProjector;
    [SerializeField] VisualEffect _starsVFX;
    [SerializeField] Vector3 _originalPosition;
    [SerializeField] GameObject _mapDeviceBeam;
    [SerializeField] Vector3 _openPosition = new Vector3(0, 1.75f, 0);
    [SerializeField] Vector3 _currentTargetPosition;
    [SerializeField] Light _mainLight;
    float _mainInstensityLight = .8f;
    CinemachineManager _cinemachineManager;
    private List<GameObject> _planetList = new List<GameObject>();
    AudioSource _mapDeviceAudio;
    AudioSource _showPlanetAudio;
    AudioSource _loopingAudioSource;
    const float _speed = 1.5f;
    bool _isGoingUp;
    bool _isGoingDown;

    public override void Start()
    {
        base.Start();
        _originalPosition = _topProjector.localPosition;
        SetAudioSource();
    }

    public void SetPlanetList(GameObject planet)
    {
        _planetList.Add(planet);
    }

    public void SetCinemachineManager(CinemachineManager cinemachineManager)
    {
        _cinemachineManager = cinemachineManager;
    }

    public override void OnInteract()
    {
        if (_isPlayerColliding && Input.GetKeyDown(KeyCode.E))
        {
            _isInteracted = !_isInteracted;
            _currentTargetPosition = _isInteracted ? _openPosition : _originalPosition;
            _isGoingUp = _currentTargetPosition == _openPosition? true : false;
            _isGoingDown = _currentTargetPosition == _originalPosition? true : false;
            _cinemachineManager.SetTargetHologram();
        }
        MoveTopDevice(_currentTargetPosition);
    }

    private void MoveTopDevice(Vector3 targetPos)
    {
        if(targetPos == _openPosition && _isGoingUp)
        {
            OnMapDeviceOpenSFX();
            _topProjector.localPosition = Vector3.Lerp(_topProjector.localPosition, targetPos, Time.deltaTime * _speed);
            if (Vector3.Distance(_topProjector.localPosition, targetPos) < 0.02f)
            {
                MapDeviceLoopSFX();
                SnapPosition(targetPos);
                ShowPlanetsSFX();
                SetMapDeviceVFX(true);
                _isGoingUp = false;
            }
        }

        if(targetPos == _originalPosition && _isGoingDown)
        {
            _topProjector.localPosition = Vector3.Lerp(_topProjector.localPosition, targetPos, Time.deltaTime * _speed);
            if (Vector3.Distance(_topProjector.localPosition, targetPos) < 0.02f)
            {
                SnapPosition(targetPos);
                _isGoingDown = false;
            }
        }
    }

    private void SnapPosition(Vector3 targetPos)
    {
        if (_topProjector.localPosition == targetPos)
        {
            return;
        }
        _topProjector.localPosition = targetPos;
    }

    private void ShowPlanetsSFX()
    {
        if (_showPlanetAudio != null && _showPlanetAudio.isPlaying) return;
        _showPlanetAudio = _audioManager.PlaySound(_audioManager.GetAudioClipData().ShowPlanetsSFX);
    }

    private void MapDeviceLoopSFX()
    {
        if (_loopingAudioSource.isPlaying)
        {
            return;
        }
        _loopingAudioSource.Play();
        _loopingAudioSource.loop = true;
    }

    private void OnMapDeviceOpenSFX()
    {
        if (_mapDeviceAudio != null && _mapDeviceAudio.isPlaying) return;
        _mainLight.intensity = 0f;
        _mapDeviceAudio = _audioManager.PlaySound(_audioManager.GetAudioClipData().OpenMapDeviceSFX);
    }

    private void OnMapDeviceCloseSFX()
    {
        if (_mapDeviceAudio != null && _mapDeviceAudio.isPlaying) return;
        _mapDeviceAudio = _audioManager.PlaySound(_audioManager.GetAudioClipData().CloseMapDeviceSFX);
        _mapDeviceAudio.loop = false;
    }

    private void SetAudioSource()
    {
        _loopingAudioSource = _audioManager.SetAudioSource();
        _loopingAudioSource.clip = _audioManager.GetAudioClipData().OnMapDeviceLoopSFX;
        _mapDeviceAudio = _audioManager.SetAudioSource();
    }

    private void SetMapDeviceVFX(bool isActive)
    {
    
        _starsVFX.enabled = isActive;
        foreach (var planet in _planetList)
        {
            if (planet != null && planet.activeSelf != isActive)
            {
                planet.SetActive(isActive);
            }
        }
        _mapDeviceBeam.SetActive(isActive);
    }

    private void OnMapDeviceExit()
    {
        if(!_isInteracted)
        {
            return;
        }
        _isPlayerColliding = false;
        _isInteracted = false;
        _currentTargetPosition = _originalPosition;
        _isGoingDown = true;
        if(_loopingAudioSource != null)
        {
            _loopingAudioSource.loop = false;
            _loopingAudioSource.Stop();
        }
        SetMapDeviceVFX(false);
        OnMapDeviceCloseSFX();
        _mapDeviceBeam.SetActive(false);
        _mainLight.intensity = _mainInstensityLight;
        _cinemachineManager.SetTargetCharacter();
    }

    public override void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            OnMapDeviceExit();
        }
    }
}
