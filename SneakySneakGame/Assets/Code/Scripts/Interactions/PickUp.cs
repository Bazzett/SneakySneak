using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class PickUp : MonoBehaviour
{
	[Header("Torque force")]
	[SerializeField] [Range(1,20)] public int force;
	private Rigidbody rb;

	[Header("Noise")]
	[SerializeField] public bool thrown;
	[SerializeField] [Range(1,100)] private int noisePulse = 1;
	private NoiseManager _noise;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		if (_noise == null) 
			_noise = GetComponent<NoiseManager>();
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (thrown)
		{
			_noise.noiseRadius = noisePulse;
			thrown = false;
			
			//Check if item has the breakchance script. Then run function.
			if (GetComponent<BreakChance>())
			{
				GetComponent<BreakChance>().Break();
			}
		}
	}

	public void RandomTorque()
	{
		float range = 1f;
		float randRange = Random.Range(-range,range);

		rb.AddTorque(new Vector3(randRange,0f,randRange)*force);
	}

}
