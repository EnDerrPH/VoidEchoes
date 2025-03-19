using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class CharacterController : BaseController
{
    
    [SerializeField] float _moveSpeed;
    [SerializeField] float _mouseSensitivity;
    [SerializeField] CharacterState _characterState;
    [SerializeField] bool _isMouseRotate; 
    [SerializeField] Vector2 _movement;
    Transform _hologramView;
    [SerializeField] ObjectManager _interactableObject;
    [SerializeField] bool _isFacingStairs;
    [SerializeField] bool _isGoingUp;
    string _idle = "Idle";
    string _walkForward = "Walk Forward";
    string _walkBackward = "Walk Backward";
    string _walkRight = "Walk Right";
    string _walkLeft = "Walk Left";
    string _stairsUp = "Stairs Up";
    string _stairsDown = "Stairs Down";
    string _rightTurn = "Right Turn";
    string _leftTurn = "Left Turn";
    float _previousYPosition;
    float _currentYPosition;
    const float _stairUpSpeed = 25;
    const float _stairDownSpeed = 10;
    const float _stairSmoothSpeed = 1;
    const float _gravityMultiplier = 10f;
    float _Ytimer = 0f;
    float _YcheckInterval = .2f;
    Vector3 _moveDirection;
    Rigidbody _rb;
    public bool IsFacingStairs {get => _isFacingStairs; set => _isFacingStairs = value;}
    public bool IsGoingUp {get => _isGoingUp; set => _isGoingUp = value;}
    public UnityEvent OnMapDeviceEvent, OnPlayerControlEvent;

    public override void Start()
    {
        base.Start();
        InitializeComponents();
    }

    [System.Obsolete]
    void FixedUpdate()
    {
        OnCharacterMovement();
        OnMouseRotate();
        OnMovingRotate();
        GravityPull();
    }

    public void OnWalkForward(InputValue value)
    {
        if(value.isPressed)
        {
            _movement.y = 1f;
            _movement.x = 0f;
            OnPlayerControl();
        }
        else
        {
            StopMovement();
        }
    }

    public void OnWalkBackward(InputValue value)
    {
        if(value.isPressed)
        {
            _movement.y = -1f;
            _movement.x = 0f;
            OnPlayerControl();
        }
        else
        {
           StopMovement();
        }
    }

    public void OnWalkRight(InputValue value)
    {
        if(value.isPressed)
        {
            _movement.x = 1f;
            _movement.y = 0f;
            OnPlayerControl();
        }
        else
        {
            StopMovement();
        }
    }

    public void OnWalkLeft(InputValue value)
    {
         if(value.isPressed)
        {
            _movement.x = -1f;
            _movement.y = 0f;
            OnPlayerControl();
        }
        else
        {
            StopMovement();
        }
    }

    public void OnInteract()
    {
        if(_interactableObject == null)
        {
            return;
        }
        _interactableObject.ActivateObject(true);

        if(_interactableObject.ObjectType == ObjectType.MapDevice)
        {
            PlayerInteract(_hologramView);
            RenderSettings.reflectionIntensity = 0f;
            OnMapDeviceEvent.Invoke();
        }
    }

    public void OnMouseRotate(InputValue value)
    {
        _isMouseRotate = value.isPressed;
    }

    public override void InitializeComponents()
    {
        base.InitializeComponents();
        _rb = GetComponent<Rigidbody>();
    }

    private void PlayerInteract(Transform view)
    {
        UnlockCursor();
        _characterState = CharacterState.Interact;
    }
    private void StopMovement()
    {
        _movement.x = 0f;
        _movement.y = 0f;
    }

    private void LockCursor()
    {
        if(GameManager.instance.GetScene() != GameScene.Home)
        {
            return;
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnMouseRotate()
    {
        if(!_isMouseRotate || _characterState != CharacterState.PlayerControl)
        {
            return;
        }

        Vector2 mouse = _mouseSensitivity * Time.deltaTime * new Vector2(Input.GetAxisRaw("Mouse X") , Input.GetAxisRaw("Mouse Y"));
        transform.Rotate(mouse.x * Vector3.up);

        float currentYRotation = transform.rotation.eulerAngles.y;

        if (currentYRotation > 180f)
        {
            currentYRotation -= 360f;  // Adjust to get a continuous range from -180 to 180
        }

        if (currentYRotation < 0f && _movement.x == 0 && _movement.y == 0)
        {
            ChangeAnimation(_leftTurn, .5f);
        }

          if (currentYRotation > 0f && _movement.x == 0 && _movement.y == 0)
        {
            ChangeAnimation(_rightTurn, .5f);
        }
    }

    private void CheckYPosition()
    {
        if(!_isFacingStairs)
        {
            return;
        }

        _Ytimer += Time.deltaTime;

        // If 1 second has passed, check the Y position
        if (_Ytimer >= _YcheckInterval)
        {
            _currentYPosition = transform.position.y;

            // Check if the Y position has changed
            if (_currentYPosition > _previousYPosition)
            {
                // Ascending
                _isGoingUp = true;
            }
            else if (_currentYPosition < _previousYPosition)
            {
                // Descending
                _isGoingUp = false;
            }

            // Update the previous Y position to the current one for the next frame
            _previousYPosition = _currentYPosition;
            _Ytimer = 0f;
        }
    }

    private void OnMovingRotate()
    {
        if(!_isMouseRotate || _characterState != CharacterState.PlayerControl)
        {
            return;
        }
        Vector2 mouse = _mouseSensitivity * Time.deltaTime * new Vector2(Input.GetAxisRaw("Mouse X") , Input.GetAxisRaw("Mouse Y"));
        transform.Rotate(mouse.x * Vector3.up);

        float currentYRotation = transform.rotation.eulerAngles.y;
    }

    private void GravityPull()
    {
        Vector3 gravityForce = Physics.gravity * _gravityMultiplier;
        _rb.AddForce(gravityForce, ForceMode.Acceleration);
    }

    [System.Obsolete]
    private void OnCharacterMovement()
    {
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        float moveDirectionY = _movement.y;
        float moveDirectionX = _movement.x;
        _moveDirection = (transform.forward * moveDirectionY + transform.right * moveDirectionX) * _moveSpeed;
    
        _rb.velocity = new Vector3(_moveDirection.x, _rb.velocity.y, _moveDirection.z);
        CheckYPosition();
        MovementAnimation();
    }

    private void OnPlayerControl()
    {
        _characterState = CharacterState.PlayerControl;
        OnPlayerControlEvent.Invoke();
    }

    private void MovementAnimation()
    { 
        if(_movement.x == 0 && _movement.y == 0 && !_isMouseRotate)
        {
            FreezeRigidbody();
            ChangeAnimation(_idle, 0f);
            return;
        }

        if(_movement.y > 0 && !_isFacingStairs)
        {
            _moveSpeed = 20f;
            ChangeAnimation(_walkForward, 0f);
        }

        if(_movement.y > 0 && _isGoingUp && _isFacingStairs)
        {
            _moveSpeed = _stairUpSpeed;
            ChangeAnimation(_stairsUp, 0f);
        }

        if(_movement.y < 0 )
        {
            _moveSpeed = 20f;
            ChangeAnimation(_walkBackward, 0f);
        }

        if(_movement.y > 0 && !_isGoingUp && _isFacingStairs)
        {
            _moveSpeed = _stairDownSpeed;
            ChangeAnimation(_stairsDown, 0f);
        }

        if(_movement.x > 0)
        {
            ChangeAnimation(_walkRight, 0f);
        }

        if(_movement.x < 0)
        {
            ChangeAnimation(_walkLeft, 0f);
        }
    }

    private void FreezeRigidbody()
    {
        _rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
    }

    private void PlayStairsSFX()
    {
        // if(_audioSource.clip == _stairsFootStepSFX)
        // {
        //     return;
        // }
        // _audioSource.clip = _stairsFootStepSFX;
        // _audioSource.Play();
        // _isStairs = false;
    }  

    private void OnStep(float stepHeight , GameObject gameObject)
    {
        Vector3 targetPosition = new Vector3(transform.position.x,transform.position.y + stepHeight , transform.position.z + .5f);
        transform.position = Vector3.Lerp(transform.position, targetPosition, _stairSmoothSpeed * Time.deltaTime);
    }

    void OnCollisionStay(Collision collision)
    {
        ObjectManager objectManager = collision.gameObject.GetComponent<ObjectManager>();
        if(objectManager == null)
        {
            return;
        }
        if(objectManager.ObjectType == ObjectType.Step && _isFacingStairs)
        {
            StepHandler stepObject = collision.gameObject.GetComponent<StepHandler>();
            OnStep(stepObject.StepHeight, collision.gameObject);
        }

        if(objectManager.ObjectType == ObjectType.MapDevice)
        {
            _interactableObject = objectManager;
            objectManager.ActivateCanvas(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        ObjectManager objectManager = collision.gameObject.GetComponent<ObjectManager>();
        if(objectManager == null)
        {
            return;
        }
        
        if (objectManager.ObjectType == ObjectType.MapDevice)
        {
            _interactableObject.ActivateObject(false);
            _interactableObject.ActivateCanvas(false);
            _interactableObject = null;
            RenderSettings.reflectionIntensity = 1f;
            LockCursor();
            OnPlayerControl();
        }
    }
}
