using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    
    [SerializeField] float _moveSpeed;
    [SerializeField] float _mouseSensitivity;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _stairsFootStepSFX;
    [SerializeField] AudioClip _normalFootStepSFX;
    [SerializeField] bool _isFacingStairs;
    [SerializeField] bool _isGoingUp;
    [SerializeField] float _stairUpSpeed;
    [SerializeField] float _stairDownSpeed;
    [SerializeField] float _startSmoothSpeed;
    [SerializeField] float _stairOffset;
    [SerializeField] Transform _origin;
    Animator _animator;
    string _currentAnimation;
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
    float _gravityMultiplier = 10f;
    float _Ytimer = 0f;
    float _YcheckInterval = .2f;
    Vector2 _movement;
    Vector3 _moveDirection;
    Rigidbody _rb;
    bool _isMovingForward;
    bool _isMouseRotate; 
    public bool IsFacingStairs {get => _isFacingStairs; set => _isFacingStairs = value;}
    public bool IsGoingUp {get => _isGoingUp; set => _isGoingUp = value;}

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        LockCursor();
    }

    [System.Obsolete]
    void FixedUpdate()
    {
        OnCharacterMovement();
        OnMouseRotate();
        OnMovingRotate();
        GravityPull();
    }

    public void OnMove(InputValue value)
    {
        _movement = value.Get<Vector2>();
    }

    public void OnWalkForward(InputValue value)
    {
        _isMovingForward = value.isPressed;
    }

    public void OnMouseRotate(InputValue value)
    {
        _isMouseRotate = value.isPressed;
    }

    private void ChangeAnimation(string animation , float crossFadeTime)
    {
        if(_currentAnimation == animation)
        {
            return;
        }
        _currentAnimation = animation;
        _animator.CrossFade(animation , crossFadeTime);
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

    private void OnMouseRotate()
    {
        if(!_isMouseRotate || _isMovingForward)
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
        if(!_isMovingForward)
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
        RaycastForward();
    }

    private void MovementAnimation()
    {
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

        if(_movement.x == 0 && _movement.y == 0 && !_isMouseRotate)
        {
            FreezeRigidbody();
            ChangeAnimation(_idle, 0f);
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

    private void RaycastForward()
    {
        if(!_isMovingForward)
        {
            return;
        }
        RaycastHit hit;

        if (Physics.Raycast(_origin.position, transform.forward, out hit, 1f))
        {
            if(hit.collider.gameObject.tag == "Computer")
            {
                //Show List of Planets
            }
        }
    }


    void OnCollisionStay(Collision collision)
    {

        if(collision.gameObject.CompareTag("Stairs") && _isFacingStairs)
        {
            float scaleY =  collision.gameObject.transform.localScale.y + _stairOffset;
            Vector3 targetPosition = new Vector3(transform.position.x,transform.position.y + scaleY , transform.position.z + .5f);
            float smoothSpeed = _startSmoothSpeed;
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }
}
