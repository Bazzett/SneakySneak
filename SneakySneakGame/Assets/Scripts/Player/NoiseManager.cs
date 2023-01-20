using UnityEngine;

    public class NoiseManager : MonoBehaviour
    {
        [SerializeField] private float defaultNoiseRadius;
        [HideInInspector] public float noiseRadius;
        [SerializeField] private LayerMask detectionFilter;
        
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
            RaycastHit hit;
            
            Vector3 p1 = transform.position;
            Physics.SphereCast(p1,noiseRadius,transform.forward,out hit, Mathf.Infinity,detectionFilter);
        }

        private void RadiusController()
        {
            noiseRadius = Mathf.Lerp(noiseRadius, defaultNoiseRadius, 0.034f);
            print(noiseRadius);
        }
    }
