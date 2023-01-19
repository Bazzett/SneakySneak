using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float walkSpeed = 10f;

    private Vector2 _moveInput;
    private Rigidbody _rb;
    

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        
    }


    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 playerVeclocity = new Vector3(_moveInput.x * walkSpeed, _rb.velocity.y, _moveInput.y * walkSpeed);
        _rb.velocity = transform.TransformDirection(playerVeclocity);
    }

    private void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }
}
