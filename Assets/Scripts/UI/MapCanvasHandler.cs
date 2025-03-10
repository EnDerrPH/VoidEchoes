using System;
using UnityEngine;

public class MapCanvasHandler : BaseTriggerManager
{
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] float _elapsedTime = 0f;
    const float _fadeDuration = 3f;
    const float _alphaIncreaseRate = .4f;
    const float _targetAlpha = 1f;
    [SerializeField] bool _isActive;

    public override void Start()
    {
        base.Start();
        _characterController.OnMapDeviceEvent.AddListener(() => SetIsActive(true));
        _characterController.OnPlayerControlEvent.AddListener(DeactivateDevice);
    }

    private void Update()
    {
        SetAlpha();
    }

    private void SetIsActive(bool isActive)
    {
        _isActive = isActive;
    }

    private void DeactivateDevice()
    {
        _canvasGroup.alpha = 0;
        _elapsedTime = 0f;
    }

    public void SetAlpha()
    {
        if (_isActive)
        {
            _elapsedTime += Time.deltaTime;
     
            if (_elapsedTime >= _fadeDuration)
            {
                _canvasGroup.alpha = Mathf.Clamp(_canvasGroup.alpha + _alphaIncreaseRate * Time.deltaTime, 0f, _targetAlpha);
                if(_canvasGroup.alpha >= 1)
                {
                    _isActive = false;
                }
            }
        }
    }
}
