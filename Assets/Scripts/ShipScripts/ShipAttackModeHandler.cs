using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections.Generic;

public class ShipAttackModeHandler : ShipManager
{
    [SerializeField] ShipMovementHandler _shipMovementHandler;
    [SerializeField] GameObject []_fireAmmos;
    [SerializeField] bool _isFiring;
    [SerializeField] ParticleSystem _fireAmmoVFX;
    List<AudioClip> _audioAttackList = new List<AudioClip>();
    Coroutine _rotationCoroutine;
    GameObject _crosshair;
    GameObject _targetObj;
    AudioClip _currentAttackModeSpeechSFX;
    AudioClip _fireSFX;
    const float _shipRotationSpeed = 40f;
    const float _targetDistance = 250f;
    Quaternion _rotationTarget;
    Quaternion _shipRotationTarget;
    int _previousParticleCount = 0;
    bool _hasInitialized;
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
        InitializeFireAmmo();
        SetAttackModeSFXList();
    }

    private void Update()
    {
        Firing();
        SetTargetObjPosition();
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
        _audioManager.PlayOneShot(_currentAttackModeSpeechSFX, _audioSpeech, .7f , 1f);
    }

    private void OnAttackModeSFX()
    {
        _audioManager.PlayOneShot(GameManager.instance.GetAudioClipData().OnAttackModeSFX, _audioVFX , .5f , 1f);
    }

   private void InitializeFireAmmo()
   {
        _fireAmmoVFX = _fireAmmos[0].GetComponent<ParticleSystem>();
        _fireSFX = GameManager.instance.GetAudioClipData().OnFireSFX;
   }

   private void SetTargetObjPosition()
   {
        if(_shipState != ShipState.AttackMode || _shipState == ShipState.Death)
        {
            return;
        }

        Vector3 targetPointPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _targetDistance);
        _targetObj.transform.position = Camera.main.ScreenToWorldPoint(targetPointPosition);
        SetShipRotation();
   }

   private void SetShipRotation()
   {
        if(_shipMovementHandler.ShipState ==  ShipState.Maneuver || _shipState != ShipState.AttackMode)
        {
            return;
        }

        if(Quaternion.Angle(transform.rotation, _shipRotationTarget) < 1f)
        {
            return;
        }
        Vector3 direction = _targetObj.transform.position - transform.position;
        _shipRotationTarget = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _shipRotationTarget, _shipRotationSpeed * Time.deltaTime);
   }

   private void SetBeamRotation()
   {
        foreach(GameObject fireAmmo in _fireAmmos)
        {
            Vector3 fireDirection = _targetObj.transform.position - transform.position;
            _rotationTarget = Quaternion.LookRotation(fireDirection);
            fireAmmo.transform.rotation = _rotationTarget;
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
            _audioManager.PlayOneShot(_fireSFX , _audioVFX , .5f , 1f);
        }
        _previousParticleCount = currentParticleCount; 
    }

    private void ShipHasInitialized()
    {
        _hasInitialized = true;
    }
}
