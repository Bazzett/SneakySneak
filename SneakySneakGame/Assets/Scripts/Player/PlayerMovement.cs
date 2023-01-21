
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private GameObject mainCam;
    private Vector2 _moveInput;
    private Vector3 _targetVector;

    [Header("Jumping")]
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float groundDistance;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private bool _grounded;

    private NoiseManager _noise;
    private float _noisePulse;
    [SerializeField] [Range(0f,1f)] private float _noisePulseRate;
    
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _noise = GetComponent<NoiseManager>();
    }
    
    void Update()
    {
        Move(_targetVector);
        GroundCheck();
        Jump();
    }

    private void FixedUpdate()
    {
        Gravity();
    }

    private void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
        
        _targetVector = new Vector3(_moveInput.x, 0, _moveInput.y);
    }
    
    
    private void Move(Vector3 direction)
    {
        var speed = walkSpeed * Time.fixedDeltaTime;
        direction = Quaternion.Euler(0, mainCam.gameObject.transform.eulerAngles.y, 0) * direction;
        Vector3 norm = direction.normalized;
        direction = (direction.magnitude > norm.magnitude) ? norm : direction;
        
        _rb.velocity = new Vector3(direction.x * speed, _rb.velocity.y, direction.z * speed);
    }

    private void Gravity()
    {
        _rb.AddForce(transform.up * -gravity, ForceMode.Acceleration);
        if (!_grounded)
        {
            _noisePulse += _noisePulseRate;
        }
        else
        {
            _noisePulse = 0;
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && _grounded)
        {
            var jumpVel = Mathf.Sqrt(jumpHeight * -2f * -gravity);
            _rb.velocity = new Vector3(0, jumpVel, 0);
        }
    }

    private void GroundCheck()
    {
        _grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

        if (_grounded && _noisePulse > _noise.defaultNoiseRadius)
        {
            _noise.noiseRadius = _noisePulse;
        }
    }
}
