using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyPatrolling : MonoBehaviour
    {
        private NavMeshAgent _agent;
        public Transform[] waypoints;
        private int _waypointsIndex;
        private Vector3 _target;

        [SerializeField]
        private EnemyFieldOfView _FOV;

        [HideInInspector] public bool stopped;

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
            if (_target != _FOV.target.position && Vector3.Distance(transform.position, _target) < 1)
            {
                IterateWaypointIndex();
            }

            stopped = false;
            //Stop enemy when found player and is close
            if (Vector3.Distance(transform.position, _FOV.target.position) < 2f)
            {
                Debug.Log("Stopped");
                stopped = true;
                _agent.ResetPath();
            }
            else if (_FOV.canSeeTarget)
            {
                Debug.Log("Chase player");
                UpdateDestination();
            }
            else if (Vector3.Distance(transform.position, _target) < 1)
            {
                Debug.Log("Go back to path");
                UpdateDestination();
            }
            else if (!_agent.hasPath)
            {
                Debug.Log("No path, start new path");
                UpdateDestination();
            }
        }

        public void SetPath(Vector3 posistion)
        {
            _agent.SetDestination(posistion);
        }
    }
}
