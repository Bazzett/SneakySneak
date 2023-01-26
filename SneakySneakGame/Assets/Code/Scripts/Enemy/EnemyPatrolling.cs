using System.Collections;
using Unity.VisualScripting;
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

        public bool stopped;

        private bool _coroutineRunning = false;

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
            //Check if target is a waypoint and not sth else
            if (_target != _FOV.target.position && Vector3.Distance(transform.position, _target) < 1 && !_coroutineRunning)
            {
                var waypointScript = waypoints[_waypointsIndex].GetComponent<WaypointBehaviour>();
                if (waypointScript.StopAndWait)
                {
                    StartCoroutine(WaitAndPath());
                }
                else if (!waypointScript.StopAndWait)
                {
                    IterateWaypointIndex();
                }
            }
            else if (Vector3.Distance(transform.position, _target) > 1) //Check if outside of start coroutine range
            {
                _coroutineRunning = false;
            }

            stopped = false;
            //Stop enemy when found player and is close
            if (Vector3.Distance(transform.position, _FOV.target.position) < 2f)
            {
                //Debug.Log("Stopped");
                stopped = true;
                _agent.ResetPath();
            }
            else if (_FOV.canSeeTarget)
            {
                //Debug.Log("Chase player");
                UpdateDestination();
            }
            else if (Vector3.Distance(transform.position, _target) < 1)
            {
                _FOV.alerted = false;
                //Debug.Log("Go to next path");
                UpdateDestination();
            }
            else if (!_agent.hasPath)
            {
                _FOV.alerted = false;
                //Debug.Log("No path, start new path");
                UpdateDestination();
            }
        }

        public void SetPath(Vector3 posistion)
        {
            _agent.SetDestination(posistion);
        }

        private IEnumerator WaitAndPath()
        {
            _coroutineRunning = true;
            var waypointScript = waypoints[_waypointsIndex].GetComponent<WaypointBehaviour>();
            yield return new WaitForSeconds(waypointScript.Seconds);
            IterateWaypointIndex();
        }
    }
}
