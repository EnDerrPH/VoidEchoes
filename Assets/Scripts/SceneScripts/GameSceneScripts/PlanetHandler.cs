using UnityEngine;

public class PlanetHandler : MonoBehaviour
{
    [SerializeField] private float _orbitSpeed = .5f;
    [SerializeField] float _xOffsetPos;
    [SerializeField] float _yOffsetPos;
    [SerializeField] float _zOffsetPos;
    [SerializeField] private Transform _center;
    [SerializeField] GameObject _glowVFX;
    [SerializeField] float _rotationSpeed;
    [SerializeField] PlanetData _planetData;
    UIHomeSceneHandler _UIHomeScreenHandler;
    AudioManager _audioManager;
    private float _orbitRadius;
    private float _angle;

    void Start()
    {
        _audioManager = AudioManager.instance;
    }

    void Update()
    {
        if (_center != null)
        {
            _angle += _orbitSpeed * Time.deltaTime;
            float x = _center.position.x + Mathf.Cos(_angle) * _orbitRadius;
            float z = _center.position.z + Mathf.Sin(_angle) * _orbitRadius;
            transform.position = new Vector3(x, transform.position.y, z);
        }
        transform.Rotate(transform.rotation.x , _rotationSpeed * Time.deltaTime , transform.rotation.z);
    }

    public void SetOrbitState(Transform orbit_center)
    {
        _center = orbit_center;
        _angle = Random.Range(0f, 360f);
        transform.position = _center.position + new Vector3(_xOffsetPos, _yOffsetPos, _zOffsetPos);
        _orbitRadius = Vector3.Distance(transform.position, _center.position);
    }

    public void SetPlanetData(PlanetData planetData)
    {
        _planetData = planetData;
    }

    public void SetUIHomeScreenHandler(UIHomeSceneHandler uiHomeScreenHandler)
    {
        _UIHomeScreenHandler = uiHomeScreenHandler;
    }

    void OnMouseOver()
    {
        if(_glowVFX.activeSelf)
        {
            return;
        }
       _glowVFX.SetActive(true);
    }

    void OnMouseExit()
    {
        _glowVFX.SetActive(false);
    }

    void OnMouseDown()
    {
        _audioManager.PlaySound(_audioManager.GetAudioClipData().OnPlanetClickSFX);
        _UIHomeScreenHandler.gameObject.SetActive(true);
        _UIHomeScreenHandler.SetCurrentPlanetData(_planetData);
        _UIHomeScreenHandler.SetMapList();
    }
}

