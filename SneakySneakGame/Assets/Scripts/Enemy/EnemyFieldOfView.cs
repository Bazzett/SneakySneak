using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class EnemyFieldOfView : MonoBehaviour
    {
        public float radius;
        [Range(0,360)]
        public float angle;
        [Range(0,360)]
        public float peripheralAngle;
    
        public GameObject playerReference;
        public LayerMask targetMask;
        public LayerMask obstructionMask;
        public bool canSeeTarget;
        public bool alerted;
        public bool peripheralCanSeeTarget;
        public Transform target;

        private void Start()
        {
            playerReference = GameObject.FindGameObjectWithTag("Player");
            target = playerReference.transform;
            StartCoroutine(FOVRoutine());
        }

        private IEnumerator FOVRoutine()
        {
            float delay = 0.2f;
            WaitForSeconds wait = new WaitForSeconds(delay);

            while (true)
            {
                yield return wait;
                FieldOfViewCheck();
                PeripheralViewCheck();
            }
        }

        private void FieldOfViewCheck()
        {
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
            if (rangeChecks.Length != 0)
            {
                target = rangeChecks[0].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, directionToTarget) < angle * 0.5f)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    {
                        canSeeTarget = true;
                    }
                    else
                    {
                        canSeeTarget = false;
                    }
                }
                else
                {
                    canSeeTarget = false;
                }
            }
            else if (canSeeTarget == true)
            {
                canSeeTarget = false;
            }
        }
        private void PeripheralViewCheck()
        {
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
            if (rangeChecks.Length != 0)
            {
                target = rangeChecks[0].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;
    
                if (Vector3.Angle(transform.forward, directionToTarget) < peripheralAngle * 0.5f)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);
    
                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    {
                        peripheralCanSeeTarget = true;
                    }
                    else
                    {
                        peripheralCanSeeTarget = false;
                    }
                }
                else
                {
                    peripheralCanSeeTarget = false;
                }
            }
            else if (peripheralCanSeeTarget == true)
            {
                peripheralCanSeeTarget = false;
            }
        }
    }
}
