using System;
using UnityEngine;

public class RayCastPlayer : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask pickup;
    [SerializeField] private Transform rightHand;
    private Transform _selection;
    private bool equipped;

    private float _pickupDistance = 2f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && equipped)
        {
            Drop();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Drop();
            ThrowItem();
        }

        HighlightTarget();
        if (equipped) _selection.transform.position = rightHand.transform.position;
    }

    private void HighlightTarget()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out hit, _pickupDistance, pickup))
        {
            
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                var selection = hit.transform;
                //selection.transform.SetParent(rightHand);
                selection.transform.localPosition = Vector3.zero;
                selection.transform.localRotation = Quaternion.Euler(Vector3.zero);

                var itemInHandrb = selection.transform.GetComponent<Rigidbody>();

                selection.GetComponent<BoxCollider>().enabled = false;
                itemInHandrb.constraints = RigidbodyConstraints.FreezeAll;
                itemInHandrb.useGravity = false;
                equipped = true;
                _selection = selection;
            }
        }
    }

    private void Drop()
    {
        var rightChild = _selection.transform;
        var itemInHandRb = rightChild.GetComponent<Rigidbody>();

        rightChild.GetComponent<BoxCollider>().enabled = true;
        itemInHandRb.constraints = RigidbodyConstraints.None;
        itemInHandRb.useGravity = true;
        equipped = false;
    }

    private void ThrowItem()
    {
        var rightChild = _selection.transform;
        var itemInHandRb = rightChild.GetComponent<Rigidbody>();
        
        Vector3 direction = cam.ScreenPointToRay(Input.mousePosition).direction;
        itemInHandRb.AddForce(direction * 800);
    }
}