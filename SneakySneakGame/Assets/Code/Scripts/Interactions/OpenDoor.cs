using System;
using TMPro;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float OpenDistance = 5f;
    [SerializeField] private LayerMask Doors;
    private Door door;
    

    private void Update()
    {
        Open();
    }


    public void Open()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(ray, out hit, OpenDistance, Doors))
            {
                if (hit.collider.TryGetComponent(out Door door))
                {
                    if (door.IsOpen)
                    {
                        door.Close();
                    }
                    else
                    {
                        door.Open(transform.position);
                    }
                }
            }
        }
    }

}
