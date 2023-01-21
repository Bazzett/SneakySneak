using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    [SerializeField] private Transform orientation;
    [SerializeField] private Transform _cameraTrasnform;

    [SerializeField]
    [Range(0f, 90f)]
    private float maxYrotation;

    private float _xRotation;
    private float _yRotation;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    void Update()
    {
        RotateCamera();
        LeanCamera();
    }

    private void RotateCamera()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        _yRotation += mouseX;
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -maxYrotation, 90f);
        
        transform.rotation = Quaternion.Euler(_xRotation, _yRotation,0);
        orientation.rotation = Quaternion.Euler(0,_yRotation,0);
    }

    private void LeanCamera()
    {
        float lean = Input.GetAxis("Lean");
        
        if (lean != 0)
        {
            if (lean == 1)
            {
                print(lean);
            }
            else if (lean == -1)
            {
                print(lean);
            }
        }
    }
}
