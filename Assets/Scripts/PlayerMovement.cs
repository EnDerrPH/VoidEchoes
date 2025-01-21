using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerWeapon _playerWeapon;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _maneuverSpeed;
    [SerializeField] float _turnSpeed;
    [SerializeField] float _xClampRange;
    [SerializeField] float _minYClampRange;
    [SerializeField] float _maxYClampRange;
    [SerializeField] float _rollClampRange;
    [SerializeField] float _rollPower;
    [SerializeField] float _rollSpeed;
    [SerializeField] float _returnSpeed;
    [SerializeField] Transform _parentTransform;
    [SerializeField] Transform _shipTransform;
    [SerializeField] AudioClip _movementSFX;
    [SerializeField] AudioClip _stopMovementSFX;
    [SerializeField] AudioSource _audioSource;
    Quaternion _originRotation = Quaternion.Euler(0f,0f,0f);
    Vector3 _originPosition = new Vector3(0f,0f,0f);
    float _resetTransformRange = 1f;
    bool _isMovingForward;
    bool _gameStart = false;
    bool _isTurningRight;
    bool _isTurningLeft;
    bool _isDirectionalMovement;
    bool _hasStop;
    Vector2 _movement;
    public UnityEvent OnMoveForwardEvent, OnGameStartEvent, OnTurnEvent;

    public bool GameStart =>_gameStart;

    void Update()
    {
        OnPlayerControl();
    }

    public void OnMove(InputValue value)
    {
        if(!_gameStart)
        {
            return;
        }
        _movement = value.Get<Vector2>();
    }
    public void OnRightTurn(InputValue value)
    {
        if(!_gameStart)
        {
            return;
        }
        _isTurningRight = value.isPressed;
    }
    public void OnLeftTurn(InputValue value)
    {
        if(!_gameStart)
        {
            return;
        }
        _isTurningLeft = value.isPressed;
    }

    public void OnForwardMovement(InputValue value)
    {
        if(!_gameStart)
        {
            return;
        }
        OnMoveForwardEvent.Invoke();
        _isMovingForward = value.isPressed;
        _audioSource.clip = _movementSFX;
        _audioSource.loop = _isMovingForward;
        _audioSource.volume = 1f;
        _audioSource.Play();
        MoveForwardSound(_isMovingForward);
    }

    public void ActivatePlayerControl()
    {
       _gameStart = true;
    }

    public void OnDirectionalMovement(InputValue value)
    {
        if(!_gameStart)
        {
            return;
        }
        _isDirectionalMovement = value.isPressed;
    }

    private void MoveForwardSound(bool play)
    {
        if(!play)
        {
            _audioSource.Stop();
            _audioSource.PlayOneShot(_stopMovementSFX);
        }
    }

    private void OnPlayerControl()
    {
        if(!_gameStart)
        {
            return;
        }
        ShipMovement();
        MoveStraight();
        ShipTurn();
        ResetShipTransform();
        OnGameStartEvent.Invoke();
    }
    
    private void ShipRoll(float direction)
    {
        float rollPower = _rollPower * direction;
        rollPower = Mathf.Clamp(rollPower, -_rollClampRange, _rollClampRange);
        Quaternion targetRotation = Quaternion.Euler(0f,0f, -rollPower);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, _rollSpeed * Time.deltaTime);
    }

    private void ShipMovement()
    {
        if(!_isDirectionalMovement)
        {
            return;
        }
        float xOffset =  _movement.x * _maneuverSpeed * Time.deltaTime;
        float yOffset =  _movement.y * _maneuverSpeed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;

        float ClampedXPos = Mathf.Clamp(rawXPos, -_xClampRange , _xClampRange);
        float ClampedYPos = Mathf.Clamp(rawYPos, _minYClampRange, _maxYClampRange);
        transform.localPosition = new Vector3(ClampedXPos, ClampedYPos, 0f);

        ShipRoll(_movement.x);
    }

    private void MoveStraight()
    {
        if(!_isMovingForward)
        {
            return;
        }
        Vector3 forwardDirection = transform.forward; 
        float zOffset = _moveSpeed * Time.deltaTime;
        _parentTransform.position += forwardDirection * zOffset;
        float yPosClamped = Mathf.Clamp(_parentTransform.position.y, 40f, 40f);
        _parentTransform.position = new Vector3(_parentTransform.position.x , yPosClamped , _parentTransform.position.z); 
    }

    private void ShipTurn()
    {
        if(_isTurningRight)
        {
            CalculateTurnOffset(1f);
        }

        if(_isTurningLeft)
        {
           CalculateTurnOffset(-1f);
        }
    }

    private void CalculateTurnOffset(float direction)
    {
        Vector3 currentRotation = _parentTransform.rotation.eulerAngles;
        currentRotation.y += _turnSpeed * direction;
        _parentTransform.rotation = Quaternion.Euler(currentRotation);
        ShipRoll(direction);
    }

    private void ResetShipTransform()
    {
        if(_shipTransform.rotation.eulerAngles.z < _resetTransformRange && transform.localPosition.x < _resetTransformRange
        && transform.localPosition.y < _resetTransformRange
        && transform.localPosition.y > -_resetTransformRange
        || _isDirectionalMovement || _isTurningLeft || _isTurningLeft)
        {
            return;
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, _originPosition, _returnSpeed * Time.deltaTime);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, _originRotation, _returnSpeed * Time.deltaTime);
    }
}
