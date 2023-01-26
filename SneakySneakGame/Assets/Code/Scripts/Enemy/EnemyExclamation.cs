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
        if (_fov.canSeeTarget || _fov.alerted)
        {
            _alert.gameObject.SetActive(true);
            _warning.gameObject.SetActive(false);
        }
        else if (!_fov.alerted)
        {
            _alert.gameObject.SetActive(false);
        }
        
        if ((_fov.peripheralCanSeeTarget) && !_fov.canSeeTarget)
        {
            _warning.gameObject.SetActive(true);
        }
        else if (!_fov.alerted)
        {
            _warning.gameObject.SetActive(false);
        }
    }
}
