using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    
    [SerializeField] float _moveSpeed;
    [SerializeField] float _mouseSensitivity;
    [SerializeField] GameObject _camera;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _stairsFootStepSFX;
    [SerializeField] AudioClip _normalFootStepSFX;
    [SerializeField] bool _isFacingStairs;
    [SerializeField] bool _isGrounded;
    [SerializeField] bool _isGoingUp;
    [SerializeField] float _gravityMultiplier;
    [SerializeField] float _stairUpSpeed;
    [SerializeField] float _stairDownSpeed;
    Animator _animator;
    string _currentAnimation;
    string _idle = "Idle";
    string _walkForward = "Walk Forward";
    string _stairsUp = "Stairs Up";
    string _stairsDown = "Stairs Down";
    Vector2 _movement;
    Rigidbody _rb;
    bool _isMoving;
    bool _isMouseRotate;   
    public bool IsFacingSairs {get => _isFacingStairs; set => _isFacingStairs = value;}
    public Vector2 Movement {get => _movement; set => _movement = value;}

    void Start()
    {
        _animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        OnCharacterMovement();
        OnCharcaterRotate();
        CheckStairs();
        GravityPull();
        CheckIfGoingUp();
        
    }

    public void OnMove(InputValue value)
    {
        _movement = value.Get<Vector2>();
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

    private void OnCharcaterRotate()
    {
        if(IsFacingSairs)
        {
            return;
        }
        Vector2 mouse = _mouseSensitivity * Time.deltaTime * new Vector2(Input.GetAxisRaw("Mouse X") , Input.GetAxisRaw("Mouse Y"));
        transform.Rotate(mouse.x * Vector3.up);
    }

    private void CheckStairs()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
        {
            if(hit.collider.CompareTag("Stairs"))
            {
                _isFacingStairs = true;
            }
            return;
        }
    }

    private void GravityPull()
    {
        if(!_isGrounded && !_isGoingUp)
        {
            Vector3 gravityForce = Physics.gravity * _gravityMultiplier;
            _rb.AddForce(gravityForce, ForceMode.Acceleration);
        }
    }


    private void OnCharacterMovement()
    {
        float ClampedY = Mathf.Clamp(_movement.y , 0f , 1f);
        float ClampedX = Mathf.Clamp(_movement.x , 0f , 0f);

        Vector3 moveDirection = (transform.forward * ClampedY + transform.right * ClampedX) * _moveSpeed;
        _rb.AddForce(moveDirection, ForceMode.Impulse);
        if(_isFacingStairs)
        {
            _movement.y = 1f;
            //transform.position = new Vector3(transform.position.x + moveDirection.x * Time.deltaTime, transform.position.y, transform.position.z);
            _rb.AddForce(moveDirection, ForceMode.Impulse);
            if(_movement.y > 0 && _isFacingStairs && _isGoingUp)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, 90f, transform.rotation.z);
                _moveSpeed = _stairUpSpeed;
                ChangeAnimation(_stairsUp, 0f);
            }

            if(_movement.y > 0 && _isFacingStairs && !_isGoingUp)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, -90f, transform.rotation.z);
                _moveSpeed = _stairDownSpeed;
                ChangeAnimation(_stairsDown, 0f);
            }
            return;
        }


        if(_movement.x == 0 && _movement.y == 0)
        {
            _isMoving= false;
            _rb.linearVelocity = Vector3.zero;
            ChangeAnimation(_idle, 0f);
        }
        _isMoving = true;
        _rb.AddForce(moveDirection, ForceMode.Impulse);

        if(_movement.y > 0 && !_isFacingStairs)
        {
            _moveSpeed = 10f;
            ChangeAnimation(_walkForward, 0f);
        }
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

    private void CheckIfGoingUp()
    {
        if(transform.rotation.eulerAngles.y > 0f && transform.rotation.eulerAngles.y < 180f)
        {
            _isGoingUp = true;
        }

        if(transform.rotation.eulerAngles.y >= 180f || transform.rotation.eulerAngles.y <= 0f  )
        {
            _isGoingUp = false;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }

        if(collision.gameObject.CompareTag("Stairs"))
        {
            _isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Stairs") || collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }
   
}
