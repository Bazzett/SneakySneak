
using UnityEngine;

public class NoiseManagerEditor : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnDrawGizmosSelected()
    {
        NoiseManager noise = GetComponent<NoiseManager>();
        Vector3 noisePos = noise.transform.position;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(noisePos,noise.noiseRadius);
    }
}
