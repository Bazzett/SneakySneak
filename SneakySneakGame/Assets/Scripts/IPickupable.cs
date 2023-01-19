using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickupable
{
	void OnCollision(Collision other);

}
