using System;
using UnityEngine;

public class RayCastPlayer : MonoBehaviour
{
    [Header("Throw")]
    [SerializeField] [Range(100, 1000)] private int throwForce = 500;
    [SerializeField] [Range(1, 3)] private float pickupDistance = 2f;
    
    [Header("Camera")]
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask pickup;
    
    [Header("Transforms and posistions")]
    [SerializeField] private Transform rightHand;
    [SerializeField] private Transform throwPosition;
    
    private Transform _selection;
    private bool equipped;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && equipped)
        {
            Drop();
        }

        if (Input.GetMouseButtonDown(0) && _selection != null)
        {
            ThrowItem();
            Drop();
        }

        HighlightTarget();
        if (equipped) _selection.transform.position = rightHand.transform.position;
    }

    private void HighlightTarget()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out hit, pickupDistance, pickup))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                var selection = hit.transform;
                //selection.transform.SetParent(rightHand);
                selection.transform.localPosition = Vector3.zero;
                selection.transform.localRotation = Quaternion.Euler(Vector3.zero);

                var itemInHandrb = selection.transform.GetComponent<Rigidbody>();

                CheckCollider(selection,false);
                
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
        var rightChildRB = rightChild.GetComponent<Rigidbody>();
        var rightChildPickUp = rightChild.GetComponent<PickUp>();
        
        CheckCollider(rightChild,true);
        rightChildRB.constraints = RigidbodyConstraints.None;
        rightChildRB.useGravity = true;
        
        rightChildPickUp.RandomTorque();
        
        equipped = false;
        _selection = null;
    }

    private void ThrowItem()
    {
        Vector3 dir;
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        bool hitting = Physics.Raycast(ray, out hit, Mathf.Infinity);
        Vector3 pos = _selection.position;
        if (hitting)
        {
            dir = (hit.point - pos).normalized;
        }
        else
        {
            _selection.transform.position = throwPosition.position;
            dir = cam.ScreenPointToRay(Input.mousePosition).direction;
        }
        
        var itemInHandRb = _selection.transform.GetComponent<Rigidbody>();
        itemInHandRb.AddForce(dir * throwForce);

    }

    private void CheckCollider(Transform select, bool state)
    {
        if (select.GetComponent<BoxCollider>())
        {
            select.GetComponent<BoxCollider>().enabled = state;
        }
        else if (select.GetComponent<SphereCollider>())
        {
            select.GetComponent<SphereCollider>().enabled = state;
        }
    }
}