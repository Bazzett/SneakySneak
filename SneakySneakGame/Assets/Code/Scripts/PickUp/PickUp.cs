using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class PickUp : MonoBehaviour
{
	private Rigidbody rb;
	[Range(1,20)] public int force;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	public void RandomTorque()
	{
		float range = 1f;
		float randRange = Random.Range(-range,range);
		print(randRange);
		
		rb.AddTorque(new Vector3(randRange,0f,randRange)*force);
		print("Torque added");
	}
	
	public void OnCollision(Collision other)
	{
		
	}
    
}
