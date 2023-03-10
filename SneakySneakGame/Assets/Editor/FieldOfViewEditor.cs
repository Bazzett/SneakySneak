

using Enemy;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyFieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {

        EnemyFieldOfView fov = (EnemyFieldOfView)target;
        Vector3 fovPos = fov.transform.position;
        
        Handles.color = Color.white;
        Handles.DrawWireArc(fovPos,Vector3.up, Vector3.forward,360,fov.radius);

        float fovEulerY = fov.transform.eulerAngles.y;
        Vector3 viewAngle01 = DirectionFromAngle(fovEulerY, -fov.angle * 0.5f);
        Vector3 viewAngle02 = DirectionFromAngle(fovEulerY, fov.angle * 0.5f);
        
        Handles.color = Color.red;
        Handles.DrawLine(fovPos,fovPos + viewAngle01 * fov.radius);
        Handles.DrawLine(fovPos,fovPos + viewAngle02 * fov.radius);
        
        Vector3 viewAngle03 = DirectionFromAngle(fovEulerY, -fov.peripheralAngle * 0.5f);
        Vector3 viewAngle04 = DirectionFromAngle(fovEulerY, fov.peripheralAngle * 0.5f);
        
        Handles.color = Color.yellow;
        Handles.DrawLine(fovPos,fovPos + viewAngle03 * fov.radius);
        Handles.DrawLine(fovPos,fovPos + viewAngle04 * fov.radius);

        if (fov.canSeeTarget)
        {
            Handles.color = Color.blue;
            Handles.DrawLine(fovPos, fov.playerReference.transform.position);
        }
        
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
