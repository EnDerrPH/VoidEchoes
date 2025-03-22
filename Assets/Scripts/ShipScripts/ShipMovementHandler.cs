using UnityEngine;
using UnityEngine.InputSystem;
using System;


public class ShipMovementHandler : ShipManager
{
    [SerializeField] ShipAttackModeHandler _shipAttackModeHandler;
    const float _moveSpeed = 50f;
    const float _returnSpeed = 1f;
    const float _maneuverSpeed = 30f;
    const float _turnSpeed = 35f;
    const float _rollClampRange = 20f;
    const float _rollPower = 20f;
    const float _rollSpeed = 5f;
    const float _maxUpwardPosition = 70f;
    Quaternion _originalRotation = Quaternion.Euler(0f,0f,0f);
    const float _originalYPos = 30f;
    const float _resetTimerLimit = 3f;
    float _resetShipTransformTimer = 0f;
    Vector2 _movement; 
    bool _isMovingForward;
    bool _hasInitialized;
    AudioSource _currentMoveAudioSource;
    public static event Action ShipInitializedEvent;
    
    private void Update()
    {
        InitializeShip();
        CheckShipMovement();
    }

    private void FixedUpdate()
    {
        ShipMovement();
        ResetShipTransform();
    }

    public void OnRightTurn(InputValue value)
    {
        if(_shipState == ShipState.Death)
        {
            return;
        }
        if(value.isPressed)
        {
            _shipState = ShipState.TurnRight;
        }
        else
        {
            _shipState = _isMovingForward? ShipState.MovingFoward : ShipState.ResetTransform;
        }
    }
    public void OnLeftTurn(InputValue value)
    {
        if(_shipState == ShipState.Death)
        {
            return;
        }
        if(value.isPressed)
        {
            _shipState = ShipState.TurnLeft;
        }
        else
        {
            _shipState = _isMovingForward? ShipState.MovingFoward : ShipState.ResetTransform;
        }
    }

    public void OnMove(InputValue value)
    {
        if(_shipState == ShipState.Death)
        {
            return;
        }
        _movement = value.Get<Vector2>();
    }

    public void OnDirectionalMovement(InputValue value)
    {
        if(_shipState == ShipState.Death)
        {
            return;
        }
        if(value.isPressed)
        {
            _shipState = ShipState.Maneuver;
        }
        else
        {
            _shipState = _isMovingForward? ShipState.MovingFoward : ShipState.ResetTransform;
        }

    }

    public void OnForwardMovement(InputValue value)
    {
        _isMovingForward = value.isPressed;

        if(!_hasInitialized || _shipState == ShipState.Death)
        {
            return;
        }
        if(_shipState == ShipState.Idle)
        _shipState = value.isPressed? ShipState.MovingFoward : ShipState.ResetTransform;

        if(!_isMovingForward)
        {
            if(_shipAttackModeHandler.ShipState == ShipState.AttackMode ||  _shipAttackModeHandler.ShipState == ShipState.Death)
            {
                return;
            }
            OnStopMovementSFX();
        }
    }

    public void SetShipState(ShipState shipState)
    {
        _shipState = shipState;
    }

    private void CheckShipMovement()
    {
        switch (_shipState)
        {
            case ShipState.Idle:
                return;
            case ShipState.Maneuver:
            case ShipState.MovingFoward:
                _resetShipTransformTimer = 0f;
                break;
            case ShipState.ResetTransform:
                SetResetPositionTimer();
                break;
        }
    }

    private void ShipMovement()
    {
        if(!_hasInitialized || _shipState == ShipState.Death)
        {
            return;
        }
        OnShipManeuver();
        MoveFoward();
        ShipTurn();
    }

    private void OnShipManeuver()
    {
        if(_shipState != ShipState.Maneuver)
        {
            return;
        }
        float xOffset =  _movement.x * _maneuverSpeed * Time.deltaTime;
        float yOffset =  _movement.y * _maneuverSpeed * Time.deltaTime;

        Vector3 horizontalMovement = transform.right  * xOffset;
        horizontalMovement.y = 0f;
        Vector3 verticalMovement = transform.up * yOffset;

        float newYPosition = transform.position.y + verticalMovement.y;

        if (newYPosition > _maxUpwardPosition)
        {
            verticalMovement.y = 0f;
        }
        //transform.position = Vector3.Lerp(transform.position, transform.position + horizontalMovement + verticalMovement, Time.deltaTime * _moveSpeed);
        transform.position += horizontalMovement + verticalMovement;

        ShipRoll(_movement.x);
    }

    private void MoveFoward()
    {
        if(!_isMovingForward || _shipAttackModeHandler.ShipState == ShipState.AttackMode)
        {
            return;
        }
        Vector3 forwardDirection = transform.forward; 
        float zOffset = _moveSpeed * Time.deltaTime;
        transform.position += forwardDirection * zOffset; 
        OnMoveForwardSFX();
    }

    private void ShipHasInitialized()
    {
        if(_hasInitialized)
        {
            return;
        }
        ShipInitializedEvent?.Invoke();
        _hasInitialized = true;
    }

    private void ShipTurn()
    {
        if(_shipAttackModeHandler.ShipState == ShipState.AttackMode)
        {
            return;
        }
        if(_shipState == ShipState.TurnRight)
        {
            CalculateTurnOffset(1f);
        }

        if(_shipState == ShipState.TurnLeft)
        {
           CalculateTurnOffset(-1f);
        }
    }

    private void CalculateTurnOffset(float direction)
    {
        transform.Rotate(direction * _turnSpeed * Time.deltaTime * Vector3.up);
        float currentYRotation = transform.rotation.eulerAngles.y;

        if (currentYRotation > 180f)
        {
            currentYRotation -= 360f;
        }
        ShipRoll(direction);
    }

    private void ShipRoll(float direction)
    {
        float rollPower = _rollPower * direction;
        rollPower = Mathf.Clamp(rollPower, -_rollClampRange, _rollClampRange);
        Quaternion targetRotation = Quaternion.Euler(0f,transform.rotation.eulerAngles.y, -rollPower);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rollSpeed * Time.deltaTime);
    }

    private void SetResetPositionTimer()
    {
        if(_shipState == ShipState.ResetTransform)
        {
            _resetShipTransformTimer += 1 * Time.deltaTime;

            if(_resetShipTransformTimer >= _resetTimerLimit)
            {
                _shipState = ShipState.Idle;
                _resetShipTransformTimer = 0f;
            }
        }
    }

    private void ResetShipTransform()
    {
        if(_shipState == ShipState.Maneuver || _shipState == ShipState.Idle || _shipAttackModeHandler.ShipState == ShipState.AttackMode)
        {
            return;
        }
        SetShipPosition();
        Quaternion newRotation = Quaternion.Euler(_originalRotation.x, transform.rotation.eulerAngles.y , _originalRotation.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, _returnSpeed * Time.deltaTime);
    }

    private void OnMoveForwardSFX()
    {
        if(_currentMoveAudioSource == null || !_currentMoveAudioSource.isPlaying)
        {
           _currentMoveAudioSource =  _audioManager.PlaySound(_audioManager.GetAudioClipData().OnMoveForwardSFX);
        }
    }

    private void InitializeShip()
    {
        if(_hasInitialized)
        {
            return;
        }
        SetShipPosition();
        if(transform.position.y >= _originalYPos - 1f)
        {
            ShipHasInitialized();
        }
    }

    private void SetShipPosition()
    {
        Vector3 newOriginalPosition = new Vector3(transform.position.x , _originalYPos, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newOriginalPosition, _returnSpeed * Time.deltaTime);
    }

    public void OnStopMovementSFX()
    {
        if(_currentMoveAudioSource == null)
        {
            return;
        }
        _currentMoveAudioSource.Stop();
        _audioManager.PlaySound(_audioManager.GetAudioClipData().OnStopMovementSFX);
    }
}
