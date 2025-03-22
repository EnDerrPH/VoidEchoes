using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections.Generic;

public class ShipAttackModeHandler : ShipManager
{
    [SerializeField] ShipMovementHandler _shipMovementHandler;
    [SerializeField] GameObject []_fireAmmos;
    [SerializeField] ParticleSystem _fireAmmoVFX;
    List<AudioClip> _audioAttackList = new List<AudioClip>();
    Camera _mainCamera;
    GameObject _crosshair;
    GameObject _targetObj;
    AudioClip _currentAttackModeSpeechSFX;
    AudioClip _fireSFX;
    Vector3 _targetPosition;
    const float _shipRotationSpeed = 2f;
    const float _targetDistance = 250f;
    Vector3 _shipRotateDirection;
    Quaternion _beamRotationTarget;
    Quaternion _shipRotationTarget;
    int _previousParticleCount = 0;
    bool _hasInitialized;
    bool _isFiring;
    public GameObject []FireAmmos {get => _fireAmmos; set => _fireAmmos = value;}
    public List<AudioClip> AudioAttackList {get => _audioAttackList; set => _audioAttackList = value;}

    public static event Action OnAttackModeEvent;

    public override void OnEnable()
    {
        base.OnEnable();
        ShipMovementHandler.ShipInitializedEvent +=  ShipHasInitialized;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        ShipMovementHandler.ShipInitializedEvent -=  ShipHasInitialized;
    }

    public override void Start()
    {
        base.Start();
        _mainCamera = Camera.main;
        Cursor.visible = false;
        InitializeFireAmmo();
        SetAttackModeSFXList();
    }

    private void FixedUpdate()
    {
        Firing();
        SetTargetObjPosition();
        SetShipRotation();
    }

    public void OnFire(InputValue value)
    {
        _isFiring = value.isPressed;

        if(!_isFiring)
        {
            SetEmissionModule(_isFiring);
        }
    }

    public void OnAttackMode()
    {
        if(!_hasInitialized || _shipState == ShipState.Death)
        {
            return;
        }
        _shipState = _shipState == ShipState.AttackMode? ShipState.Idle : ShipState.AttackMode;
        _shipMovementHandler.OnStopMovementSFX();
        OnAttackModeEvent?.Invoke();
        SetAttackModeSFX();

        if(_shipState == ShipState.Idle)
        {
            _isFiring = false;
            SetEmissionModule(_isFiring);
            _previousParticleCount = 0;
        }
    }

    private void SetAttackModeSFXList()
    {
        CharacterData characterData = _gameManager.GetCharacterData();
        _audioAttackList.AddRange(characterData.AudioAttackList);
    }

    private void SetAttackModeSFX()
    {
        if( _shipState == ShipState.AttackMode)
        {
            int randomIndex = UnityEngine.Random.Range(0, _audioAttackList.Count);
            SetAttackModeSpeechSFX(_audioAttackList[randomIndex]);
            OnAttackModeSFX();
            OnTarget(true);
        }
        else
        {
            OnAttackModeSFX();
            OnTarget(false);
        }
    }

    private void OnTarget(bool isTarget)
    {
        _crosshair.SetActive(isTarget);
        _targetObj.SetActive(isTarget);
    }

    private void SetAttackModeSpeechSFX(AudioClip audioClip)
    {
        _currentAttackModeSpeechSFX = audioClip;
        _audioManager.PlaySound(_currentAttackModeSpeechSFX , .8f);
    }

    private void OnAttackModeSFX()
    {
        _audioManager.PlaySound(_audioManager.GetAudioClipData().OnAttackModeSFX , .6f);
    }

   private void InitializeFireAmmo()
   {
        _fireAmmoVFX = _fireAmmos[0].GetComponent<ParticleSystem>();
        _fireSFX = _audioManager.GetAudioClipData().OnFireSFX;
   }

   private void SetTargetObjPosition()
   {
        if(_shipState != ShipState.AttackMode || _shipState == ShipState.Death)
        {
            return;
        }

        if (Vector3.Distance(_targetObj.transform.position, _mainCamera.ScreenToWorldPoint(_targetPosition)) < 0.02f) 
        {
            return;
        }

        _targetPosition.Set(Input.mousePosition.x, Input.mousePosition.y, _targetDistance);
        _targetObj.transform.position = _mainCamera.ScreenToWorldPoint(_targetPosition);
   }

   private void SetShipRotation()
   {
        if(_shipMovementHandler.ShipState ==  ShipState.Maneuver || _shipState != ShipState.AttackMode)
        {
            return;
        }

        _shipRotateDirection = _targetObj.transform.position - transform.position;
        _shipRotationTarget = Quaternion.LookRotation(_shipRotateDirection);

        if (Quaternion.Angle(transform.rotation, _shipRotationTarget) < 0.2f) 
        {
            return;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, _shipRotationTarget, _shipRotationSpeed * Time.deltaTime);
   }

   private void SetBeamRotation()
   {
        foreach(GameObject fireAmmo in _fireAmmos)
        {
            Vector3 fireDirection = _targetObj.transform.position - transform.position;
            _beamRotationTarget = Quaternion.LookRotation(fireDirection);
            fireAmmo.transform.rotation = _beamRotationTarget;
        }
   }

   public void SetGameObjects(GameObject targetObj , GameObject crosshairObj)
   {
        _targetObj = targetObj;
        _crosshair = crosshairObj;
   }

   private void Firing()
   {
        if(_shipState != ShipState.AttackMode || !_isFiring || _shipState == ShipState.Death)
        {
            return;
        }
        SetBeamRotation();
        SetEmissionModule(_isFiring);
        OnFireSFX();
   }

   private void SetEmissionModule(bool isFiring)
   {
        if(_fireAmmos.Length <= 0)
        {
            return;
        }
        foreach(GameObject fireammo in _fireAmmos)
        {
            ParticleSystem.EmissionModule emissionModule = fireammo.GetComponent<ParticleSystem>().emission;
  
            emissionModule.enabled = isFiring;
        }
   }

    private void OnWeaponStandby()
    {
        if(_shipState == ShipState.Idle)
        {
            return;
        }
        _shipState = ShipState.Idle;
        _isFiring = false;
        SetEmissionModule(_isFiring);
    }


    private void OnFireSFX()
    {
        int currentParticleCount = _fireAmmoVFX.particleCount;

        if(currentParticleCount > _previousParticleCount)
        {
            _audioManager.PlaySound(_fireSFX , .3f);
        }
        _previousParticleCount = currentParticleCount; 
    }

    private void ShipHasInitialized()
    {
        _hasInitialized = true;
    }
}
