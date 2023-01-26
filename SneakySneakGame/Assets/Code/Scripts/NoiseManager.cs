using System.Collections.Generic;
using Enemy;
using UnityEngine;

    public class NoiseManager : MonoBehaviour
    {
        [HideInInspector] public List<GameObject> currentHitObjects = new List<GameObject>();

        public float defaultNoiseRadius;
        public float noiseRadius;
        [SerializeField] private LayerMask detectionFilter;
        [SerializeField] private Transform playerBody;
        
        void Start()
        {
            noiseRadius = defaultNoiseRadius;
        }
        
        void Update()
        {
            NoiseForwarding();
            RadiusController();
        }

        public void NoiseForwarding()
        {
            currentHitObjects.Clear();
            
            Vector3 p1 = playerBody.position;
            RaycastHit[] hits = Physics.SphereCastAll(p1,noiseRadius,-transform.up, noiseRadius,detectionFilter);
            foreach (RaycastHit hit in hits)
            {
                currentHitObjects.Add(hit.transform.gameObject); //Add hit target to a list
                var hitFOV = hit.transform.GetComponent<EnemyFieldOfView>(); //Get script from hit target
                var hitPatrolling = hit.transform.GetComponent<EnemyPatrolling>(); //Get script from hit target
                hitFOV.alerted = true; //Set state to alerted
                if (!hitPatrolling.stopped) hitPatrolling.SetPath(transform.position); //Set path if not stopped
            }
        }

        private void RadiusController()
        {
            noiseRadius = Mathf.Lerp(noiseRadius, defaultNoiseRadius, 0.015f);
        }
    }
