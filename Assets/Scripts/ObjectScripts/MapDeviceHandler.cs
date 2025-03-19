using UnityEngine;

public class MapDeviceHandler : ObjectManager
    {
    [SerializeField] Transform _topProjector;
    [SerializeField] GameObject _hologram;
    const float _minY = 0.09f;
    const float _maxY = 1.75f;
    const float _speed = 1.5f;
    const float _offsetLimit = .01f;

    void Update()
    {
        OpenProjector();
        CloseProjector();
    }

    private void OpenProjector()
    {
        if(!_isOpen || _hologram.activeSelf)
        {
            return;
        }
        ActivateCanvas(false);
        _topProjector.localPosition = new Vector3(_topProjector.localPosition.x, Mathf.Lerp(_topProjector.localPosition.y, _maxY, Time.deltaTime * _speed), _topProjector.localPosition.z);

        if(_topProjector.localPosition.y >= _maxY - _offsetLimit)
        {
            _hologram.SetActive(true);
        }
    }

    private void CloseProjector()
    {
        if(_isOpen || _topProjector.localPosition.y <= _minY + _offsetLimit)
        {
            return;
        }
        _hologram.SetActive(false);
        _topProjector.localPosition = new Vector3(_topProjector.localPosition.x, Mathf.Lerp(_topProjector.localPosition.y, _minY, Time.deltaTime * _speed), _topProjector.localPosition.z);
    }
}
