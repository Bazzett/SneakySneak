using UnityEngine;
using UnityEngine.AI;


public class Enemy_Patrolling : MonoBehaviour
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
        FindPath();
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

    void FindPath()
    {
        if (_FOV.canSeeTarget)
        {
            UpdateDestination();
        }
        else if (_target != waypoints[_waypointsIndex].position)
        {
            UpdateDestination();
        }
        
        if (_target != _FOV.target.position && Vector3.Distance(transform.position, _target) < 1)
        {
            IterateWaypointIndex();
        }

        //Stop enemy when found player and is close
        if (Vector3.Distance(transform.position, _FOV.target.position) < 1 && _agent.destination != transform.position)
        {
            _agent.SetDestination(transform.position);
        }
        else if (!_agent.hasPath)
        {
            print("No path now path");
            UpdateDestination();
        }
    }
}
