using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float walkSpeed;

    [SerializeField] private Transform playerBody;
    [SerializeField] private GameObject mainCam;
    private Vector2 _moveInput;
    private Vector3 _targetVector;
    private Rigidbody _rb;
    

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        
    }
    
    void Update()
    {
        Move(_targetVector);
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
}
