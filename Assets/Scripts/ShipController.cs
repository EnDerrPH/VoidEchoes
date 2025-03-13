using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    const float _moveSpeed = 50f;
    const float _returnSpeed = 1f;
    const float _maneuverSpeed = 30f;
    const float _minYClampRange = -10f;
    const float _maxYClampRange = 50f;
    const float _turnSpeed = 1f;
    const float _rollClampRange = 20f;
    const float _rollPower = 20f;
    const float _rollSpeed = 5f;
    Quaternion _originalRotation = Quaternion.Euler(0f,0f,0f);
    const float _originalYPos = 30f;
    const float _resetTimerLimit = 3f;
    Transform _parentTransform;
    Vector3 _originPosition;
    float _resetShipTransformTimer = 0f;
    Vector2 _movement; 
    bool _isDirectionalMovement;
    bool _isMovingForward;
    bool _isTurningRight;
    bool _isTurningLeft;
    bool _isResetting = false;
    public Transform ParentTransform {get => _parentTransform; set => _parentTransform = value;}
    [SerializeField] ShipState _shipState = ShipState.Default;

    void Start()
    {
        _shipState = ShipState.Initalize;
        _isResetting = true;
    }

    private void Update()
    {
        SetResetPositionTimer();
    }

    private void FixedUpdate()
    {
        ShipMovement();
        ResetShipTransform();
    }

    public void OnRightTurn(InputValue value)
    {
        _isTurningRight = value.isPressed;
        CheckShipMovement(value.isPressed);
    }
    public void OnLeftTurn(InputValue value)
    {
        _isTurningLeft = value.isPressed;
        CheckShipMovement(value.isPressed);
    }

    public void OnMove(InputValue value)
    {
        _movement = value.Get<Vector2>();
    }

    public void OnDirectionalMovement(InputValue value)
    {
        _isDirectionalMovement = value.isPressed;
        CheckShipMovement(value.isPressed);
    }

    public void OnForwardMovement(InputValue value)
    {
        _isMovingForward = value.isPressed;
        CheckShipMovement(value.isPressed);
    }

    private void CheckShipMovement(bool isPressed)
    {
        if( _shipState == ShipState.Initalize)
        {
            return;
        }
        _shipState = isPressed? ShipState.Maneuver : ShipState.ResetTransform;
        _isResetting = _shipState == ShipState.ResetTransform;
        RestartOnShipReset(_isResetting);

        if(_isMovingForward && !_isDirectionalMovement)
        {
            _shipState = ShipState.MovingFoward;
            RestartOnShipReset(false);
        }
    }

    private void RestartOnShipReset(bool isReset)
    {
        _isResetting = isReset;
        _resetShipTransformTimer = 0f;
    }

    private void SetOriginalTransform()
    {
        _originalRotation = transform.rotation;
    }

    private void ShipMovement()
    {
        if( _shipState == ShipState.Initalize)
        {
            return;
        }
        OnShipManeuver();
        MoveFoward();
        ShipTurn();
    }

    private void OnShipManeuver()
    {
        if(!_isDirectionalMovement)
        {
            return;
        }
        float xOffset =  _movement.x * _maneuverSpeed * Time.deltaTime;
        float yOffset =  _movement.y * _maneuverSpeed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;

        float ClampedYPos = Mathf.Clamp(rawYPos, _minYClampRange, _maxYClampRange);
        transform.localPosition = new Vector3(rawXPos, ClampedYPos, 0f);

        ShipRoll(_movement.x);
    }

    private void MoveFoward()
    {
        if(!_isMovingForward)
        {
            return;
        }
        Vector3 forwardDirection = transform.forward; 
        float zOffset = _moveSpeed * Time.deltaTime;
        _parentTransform.position += forwardDirection * zOffset; 
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

    private void ShipRoll(float direction)
    {
        float rollPower = _rollPower * direction;
        rollPower = Mathf.Clamp(rollPower, -_rollClampRange, _rollClampRange);
        Quaternion targetRotation = Quaternion.Euler(0f,0f, -rollPower);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, _rollSpeed * Time.deltaTime);
    }

    private void SetResetPositionTimer()
    {
        if(_isResetting)
        {
            _resetShipTransformTimer += 1 * Time.deltaTime;

            if(_resetShipTransformTimer >= _resetTimerLimit)
            {
                _shipState = ShipState.Idle;
                RestartOnShipReset(false);
            }
        }
    }

    private void ResetShipTransform()
    {
        if(_shipState == ShipState.Idle || _shipState == ShipState.Maneuver)
        {
            return;
        }
        Vector3 newOriginalPosition = new Vector3(transform.localPosition.x , _originalYPos, transform.localPosition.z);
        transform.localPosition = Vector3.Lerp(transform.localPosition, newOriginalPosition, _returnSpeed * Time.deltaTime);
        Quaternion newRotation = Quaternion.Euler(_originalRotation.x, transform.localRotation.eulerAngles.y , _originalRotation.z);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, newRotation, _returnSpeed * Time.deltaTime);
    }
}
