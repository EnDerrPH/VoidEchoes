using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerWeapon : Weapon
{
    [SerializeField] ParticleSystem _ammo;
    [SerializeField] List<Enemy> _enemiesInRange = new List<Enemy>();
    [SerializeField] RectTransform _crosshair;
    [SerializeField] Transform _targetObject;
    [SerializeField] ShipMovement _playerMovement;
    [SerializeField] Transform _shipTransform;
    [SerializeField] float _targetDistance;
    [SerializeField] AudioClip _attackSFX;
    [SerializeField] AudioClip _attackStopSFX;
    [SerializeField] AudioSource _audioSource;
    Transform _currentTarget;
    bool _isFiring;
    string _hexLockedColor = "#06FF00";
    string _hexOriginalColor = "#9BFF99";
    Color _lockedCrosshairColor;
    Color _originalCrosshairColor;


    void Start()
    {
        AddListener();
        SetCrosshairColors();
    }

    void Update()
    {
        AttackMode();
    }

    public void OnFire(InputValue value)
    {
        if(!_playerMovement.GameStart)
        {
            return;
        }
        _isFiring = value.isPressed;
        SetAmmoEmmision(_ammo, _isFiring);
        PlayFireSFX(_isFiring , .2f);
        _crosshair.gameObject.SetActive(_isFiring);
        Cursor.visible = !_isFiring;
    }


    public override void SetAmmoEmmision(ParticleSystem ammo, bool isFiring)
    {
        base.SetAmmoEmmision(ammo,isFiring);
    }

    public override void AimTarget(Transform targetObject , Transform thisObject , float rotationSpeed)
    {
        base.AimTarget(_targetObject, _ammo.gameObject.transform , 5f);
        _shipTransform.rotation = Quaternion.Slerp(_shipTransform.rotation, thisObject.transform.rotation, Time.deltaTime * rotationSpeed);
    }

    private void PlayFireSFX(bool Enabled , float volume)
    {
        _audioSource.clip = _attackSFX;
        _audioSource.volume = volume;
        _audioSource.loop = enabled;

        if(_isFiring)
        {
            _audioSource.Play();
        }
        else
        {
            _audioSource.Stop();
        }
    }

    private void SetCrosshairPosition()
    {
        Vector3 targetScreenPosition = Camera.main.WorldToScreenPoint(_targetObject.position);
        _crosshair.position = targetScreenPosition;
    }

    private void SetCrosshairColors()
    {
        
        ColorUtility.TryParseHtmlString(_hexLockedColor, out _lockedCrosshairColor);
        ColorUtility.TryParseHtmlString(_hexOriginalColor, out _originalCrosshairColor);
    }

    private void AttackMode()
    {
        if(!_isFiring)
        {
            return;
        }
        SetCrosshairPosition();
        SetTargetPoint();
        AimTarget(_targetObject,_ammo.gameObject.transform , 5f);
    }

    private void SetTargetPoint()
    {
        Vector3 targetPointPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _targetDistance);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))  
        {
        
            if(hit.collider.gameObject.GetComponent<Enemy>())
            {
                float yOffset = 6f;
                Vector3 targetEnemey = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + yOffset , hit.collider.gameObject.transform.position.z);
                _targetObject.position = targetEnemey;
                _crosshair.gameObject.GetComponent<Animator>().SetBool("IsEnemy", true);
                _crosshair.GetComponent<Image>().color = _lockedCrosshairColor;
                return;
            }
        }
        
        _targetObject.position = Camera.main.ScreenToWorldPoint(targetPointPosition);
        _crosshair.gameObject.GetComponent<Animator>().SetBool("IsEnemy", false);
        _crosshair.GetComponent<Image>().color = _originalCrosshairColor;
    }


    private void AddListener()
    {
        _playerMovement.OnGameStartEvent.AddListener(SetCrosshairPosition);
    }
}
