using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakChance : MonoBehaviour
{
    [SerializeField] [Range(0f,1f)] private float chance;
    private float _breakCheck;

    public void Break()
    {
        _breakCheck = Random.Range(0f, 1f);
        if (_breakCheck < chance)
        {
            Destroy(gameObject);
        }
    }
}
