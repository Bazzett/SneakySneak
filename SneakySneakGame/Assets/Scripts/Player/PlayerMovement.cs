
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using static Unity.Burst.Intrinsics.X86.Avx;

public class PlayerMovement : MonoBehaviour
{
	[Header("Movement")]
	[SerializeField] private float walkSpeed;
	[SerializeField] private float sprintSpeed;
	[SerializeField] float currentSpeed;
	[SerializeField] private GameObject mainCam;
	[SerializeField] private float sprintNoise;
	private Vector2 _moveInput;
	private Vector3 _targetVector;

	[Header("Stamina")]
	[SerializeField] private float stamina;
	[SerializeField] private float maxStamina;
	[SerializeField] private float staminaDecreaseRate;
	[SerializeField] private float staminaIncreaseRate;
	[SerializeField] private bool isSprinting;

	[Header("Jumping")]
	[SerializeField] private float gravity;
	[SerializeField] private float jumpHeight;
	[SerializeField] private float groundDistance;
	[SerializeField] private Transform groundCheck;
	[SerializeField] private LayerMask groundLayer;
	private bool _grounded;

	[Header("Equipment")]
	[SerializeField] private GameObject lamp;
	[SerializeField] private float minOil = 0f;
	[SerializeField] private float maxOil = 10f;
	private float currentOil = 10f;

	private bool isLampOn = false;
	private bool oilSupplyEmpty = true;

	private NoiseManager _noise;
	private float _noisePulse;
	[SerializeField][Range(0f, 1f)] private float _noisePulseRate;

	private Rigidbody _rb;


	private void Awake()
	{
		_rb = GetComponent<Rigidbody>();
		_noise = GetComponent<NoiseManager>();
	}

	void Start()
	{
		currentSpeed = walkSpeed;
		stamina = maxStamina;
		lamp.gameObject.SetActive(false);

	}

	void Update()
	{
		
		Move(_targetVector);
		GroundCheck();
		Run();
		Lamp();
		OilBurn();
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
		var speed = currentSpeed * Time.fixedDeltaTime;
		direction = Quaternion.Euler(0, mainCam.gameObject.transform.eulerAngles.y, 0) * direction;
		Vector3 norm = direction.normalized;
		direction = (direction.magnitude > norm.magnitude) ? norm : direction;

		_rb.velocity = new Vector3(direction.x * speed * 10, _rb.velocity.y, direction.z * speed * 10);
	}

	private void OnJump()
	{
		if (_grounded)
		{
			var jumpVel = Mathf.Sqrt(jumpHeight * -2f * -gravity);
			_rb.velocity = new Vector3(0, jumpVel, 0);
		}
	}

	private void Run()
	{
		if (Input.GetKeyDown(KeyCode.LeftShift) && stamina > 5)
		{
			isSprinting = true;
			currentSpeed = sprintSpeed;
		}
		else if (Input.GetKeyUp(KeyCode.LeftShift) || stamina <= 0)
		{
			isSprinting = false;
			currentSpeed = walkSpeed;
		}


		if (isSprinting)
		{
			stamina -= staminaDecreaseRate * Time.deltaTime;
			_noise.noiseRadius = sprintNoise;
		}
		else
		{
			stamina += staminaIncreaseRate * Time.deltaTime;
		}

		if (stamina < 0)
		{
			stamina = 0;
		}
		if (stamina > maxStamina)
		{
			stamina = maxStamina;
		}
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

	private void GroundCheck()
	{
		_grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

		if (_grounded && _noisePulse > _noise.defaultNoiseRadius)
		{
			_noise.noiseRadius = _noisePulse;
		}
	}

	private void Lamp()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			if (isLampOn == false)
			{
				lamp.gameObject.SetActive(true);
				isLampOn = true;
			}
			else
			{
				lamp.gameObject.SetActive(false);
				isLampOn = false;
			}
		}
	}

	private void OilBurn()
	{
		if ((currentOil > minOil) && isLampOn == true)
		{
			currentOil -= Time.deltaTime;
		}
		if (currentOil < minOil)
		{
			lamp.gameObject.SetActive(false);
		}
	}
}

