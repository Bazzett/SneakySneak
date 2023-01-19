using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RayCastPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform playerCamera;

	private float hitDistance = 10f;

	[SerializeField]
	private LayerMask Pickup;

	private void Update()
	{
		if (Physics.Raycast(playerCamera.position, transform.TransformDirection(Vector3.forward), hitDistance, Pickup))
		{
			Debug.Log("You can pick this up");
		}
	}
}
