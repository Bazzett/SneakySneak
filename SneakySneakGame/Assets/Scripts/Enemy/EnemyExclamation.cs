using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class EnemyExclamation : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private Transform _warning;
    [SerializeField] private Transform _alert;

    private EnemyFieldOfView _fov;

    private void Start()
    {
        _fov = GetComponent<EnemyFieldOfView>();
    }

    private void Update()
    {
        ExclamationManager();
    }

    private void ExclamationManager()
    {
        if (_fov.canSeeTarget)
        {
            _alert.gameObject.SetActive(true);
        }
        else
        {
            _alert.gameObject.SetActive(false);
        }
        
        if (_fov.peripheralCanSeeTarget)
        {
            _alert.gameObject.SetActive(true);
        }
        else
        {
            _alert.gameObject.SetActive(false);
        }
    }
}
