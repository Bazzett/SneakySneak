using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
    private bool _isGrounded;


    private Rigidbody _rb;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        Move(_targetVector);
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
    }

    private void Jump()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            var jumpVel = Mathf.Sqrt(jumpHeight * -2f * -gravity);
            _rb.velocity = new Vector3(0, jumpVel, 0);
        }
    }
}
