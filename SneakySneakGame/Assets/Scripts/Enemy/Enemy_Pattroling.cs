using UnityEngine;
using UnityEngine.AI;


public class Enemy_Pattroling : MonoBehaviour
{
    private NavMeshAgent _agent;
    public Transform[] waypoints;
    private int _waypointsIndex;
    private Vector3 _target;

    [SerializeField]
    private Enemy_FieldOfView _FOV;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _target) < 1)
        {
            IterateWaypointIndex();
            UpdateDestination();
        }
        else if (_FOV.canSeeTarget)
        {
            UpdateDestination();
        }
    }

    void UpdateDestination()
    {
        _target = _FOV.canSeeTarget ? _FOV.target.position : waypoints[_waypointsIndex].position;
        _agent.SetDestination(_target);
    }

    void IterateWaypointIndex()
    {
        _waypointsIndex++;
        if (_waypointsIndex == waypoints.Length)
        {
            _waypointsIndex = 0;
        }
    }
}
