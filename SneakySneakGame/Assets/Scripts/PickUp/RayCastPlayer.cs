using UnityEngine;

public class RayCastPlayer : MonoBehaviour
{
	[SerializeField] private LayerMask pickup;
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;

    private Transform _selection;
    
	private float _pickupDistance = 2f;

	

	private void Update()
	{
		
		if (_selection != null)
		{
			var selectionRenderer = _selection.GetComponent<Renderer>();
			selectionRenderer.material = defaultMaterial;
			_selection = null;
		}
		
		RaycastHit hit; ;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


		if (Physics.Raycast(ray, out hit, _pickupDistance, pickup))
		{
			var selection = hit.transform;
			var selectionRenderer = selection.GetComponent<Renderer>();
			if (selectionRenderer != null)
			{
				selectionRenderer.material = highlightMaterial;
			}

			_selection = selection;

			//Debug.Log("You can pick this up");
		}

		
	}
	
	
}
