using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    [SerializeField] ShipState _shipState = ShipState.Default;
    const float _moveSpeed = 50f;
    const float _returnSpeed = 1f;
    const float _maneuverSpeed = 30f;
    const float _turnSpeed = 20f;
    const float _rollClampRange = 20f;
    const float _rollPower = 20f;
    const float _rollSpeed = 5f;
    const float _maxUpwardPosition = 50f;
    Quaternion _originalRotation = Quaternion.Euler(0f,0f,0f);
    const float _originalYPos = 30f;
    const float _resetTimerLimit = 3f;
    Transform _parentTransform;
    float _resetShipTransformTimer = 0f;
    Vector2 _movement; 
    bool _isMovingForward;
    public Transform ParentTransform {get => _parentTransform; set => _parentTransform = value;}

    void Start()
    {
        _shipState = ShipState.Initalize;
    }

    private void Update()
    {
        CheckShipMovement();
    }

    private void FixedUpdate()
    {
        ShipMovement();
        ResetShipTransform();
    }

    public void OnRightTurn(InputValue value)
    {
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
        _movement = value.Get<Vector2>();
    }

    public void OnDirectionalMovement(InputValue value)
    {
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
        _shipState = value.isPressed? ShipState.MovingFoward : ShipState.ResetTransform;
    }

    private void CheckShipMovement()
    {
        switch (_shipState)
        {
            case ShipState.Initalize:
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

        transform.position += horizontalMovement + verticalMovement;

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
        transform.position += forwardDirection * zOffset; 
    }

    private void ShipTurn()
    {
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
        if(_shipState == ShipState.Maneuver || _shipState == ShipState.Idle)
        {
            return;
        }
        Vector3 newOriginalPosition = new Vector3(transform.position.x , _originalYPos, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newOriginalPosition, _returnSpeed * Time.deltaTime);
        Quaternion newRotation = Quaternion.Euler(_originalRotation.x, transform.rotation.eulerAngles.y , _originalRotation.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, _returnSpeed * Time.deltaTime);
    }
}
