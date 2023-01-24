using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointBehaviour : MonoBehaviour
{
    [Header("Taking a break?")]
    public bool StopAndWait;
    [Range(1,15)] public int Seconds = 1;

}
