using UnityEngine;

public class ProjectorDeviceManager : InteractableHandler
{
   [SerializeField] Transform _topProjector;
   [SerializeField] GameObject _hologram;
   [Header("Top Projector")]
   [SerializeField] float _minY;
   [SerializeField] float _maxY;
   [SerializeField] float _speed;

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

        if(_topProjector.localPosition.y >= _maxY - .1)
        {
            _hologram.SetActive(true);
        }
    }

    private void CloseProjector()
    {
        if(_isOpen || _topProjector.localPosition.y <= _minY + .2)
        {
            return;
        }
        _hologram.SetActive(false);
        _topProjector.localPosition = new Vector3(_topProjector.localPosition.x, Mathf.Lerp(_topProjector.localPosition.y, _minY, Time.deltaTime * _speed), _topProjector.localPosition.z);
        //ActivateCanvas(!_isOpen);
    }
}
