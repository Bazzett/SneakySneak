using System;
using UnityEngine;

public class RayCastPlayer : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask pickup;
    [SerializeField] private Transform rightHand;
    [SerializeField] private Transform throwPosition;
    private Transform _selection;
    private bool equipped;

    [SerializeField] private float _pickupDistance = 2f;

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

        var rightChild = _selection.transform;
        var itemInHandRb = rightChild.GetComponent<Rigidbody>();

        itemInHandRb.AddForce(dir * 500);
    }
}