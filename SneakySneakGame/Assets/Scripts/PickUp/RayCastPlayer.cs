using System;
using UnityEngine;

public class RayCastPlayer : MonoBehaviour
{
    [SerializeField] private LayerMask pickup;
    [SerializeField] private Transform rightHand;
    private Transform _selection;
    private bool equipped;

    private float _pickupDistance = 2f;

    private void Update()
    {
        
        Drop();
        HighlightTarget();
    }

    private void HighlightTarget()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out hit, _pickupDistance, pickup))
        {
            
            var selection = hit.transform;
            if (Input.GetKeyDown(KeyCode.E))
            {
                selection.transform.SetParent(rightHand);
                selection.transform.localPosition = Vector3.zero;
                selection.transform.localRotation = Quaternion.Euler(Vector3.zero);

                var itemInHandrb = rightHand.GetChild(0).GetComponent<Rigidbody>();

                selection.GetComponent<BoxCollider>().enabled = false;
                itemInHandrb.constraints = RigidbodyConstraints.FreezeAll;
                itemInHandrb.useGravity = false;
                equipped = true;
            }

            _selection = selection;
        }
    }

    private void Drop()
    {
        if (Input.GetKeyDown(KeyCode.E) && equipped)
        {
            var rightChild = rightHand.GetChild(0);
            var itemInHandrb = rightChild.GetComponent<Rigidbody>();

            rightChild.GetComponent<BoxCollider>().enabled = true;
            rightChild.SetParent(null);
            itemInHandrb.constraints = RigidbodyConstraints.None;
            itemInHandrb.useGravity = true;
            equipped = false;
        }
    }
}