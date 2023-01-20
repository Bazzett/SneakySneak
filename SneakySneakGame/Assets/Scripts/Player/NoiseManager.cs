using System.Collections.Generic;
using Enemy;
using UnityEngine;

    public class NoiseManager : MonoBehaviour
    {
        [HideInInspector] public List<GameObject> currentHitObjects = new List<GameObject>();

        [SerializeField] private float defaultNoiseRadius;
        public float noiseRadius;
        [SerializeField] private LayerMask detectionFilter;
        [SerializeField] private Transform playerBody;
        
        // Start is called before the first frame update
        void Start()
        {
            noiseRadius = defaultNoiseRadius;
        }

        // Update is called once per frame
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
                currentHitObjects.Add(hit.transform.gameObject);
                var hitFOV = hit.transform.GetComponent<EnemyFieldOfView>();
                var hitPatrolling = hit.transform.GetComponent<EnemyPatrolling>();
                hitFOV.alerted = true;
                hitPatrolling.SetPath(transform.position);
            }
        }

        private void RadiusController()
        {
            noiseRadius = Mathf.Lerp(noiseRadius, defaultNoiseRadius, 0.03f);
        }
    }
