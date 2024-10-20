using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Detection))]
public class NewBehaviourScript : Editor
{
    private void OnSceneGUI()
    {
        Detection detection = (Detection)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(detection.transform.position, Vector3.up, Vector3.forward, 360, detection.radius);
        if(detection.tag == "Camera")
        {
            Handles.DrawWireArc(detection.transform.position, Vector3.up, Vector3.forward, 360, detection.cutoffRadius);
        }
        Handles.DrawWireArc(detection.transform.position, Vector3.up, Vector3.forward, 360, detection.awarenessRadius);

        Vector3 viewAngleA = DirectionFromAngle(detection.transform.eulerAngles.y, -detection.angle / 2);
        Vector3 viewAngleB = DirectionFromAngle(detection.transform.eulerAngles.y, detection.angle / 2);
        Vector3 cutoffAngleA = DirectionFromAngle(detection.transform.eulerAngles.y, -detection.cutoffAngle / 2);
        Vector3 cutoffAngleB = DirectionFromAngle(detection.transform.eulerAngles.y, detection.cutoffAngle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(detection.transform.position, detection.transform.position + viewAngleA * detection.radius);
        Handles.DrawLine(detection.transform.position, detection.transform.position + viewAngleB * detection.radius);
        Handles.DrawLine(detection.transform.position, detection.transform.position + cutoffAngleA * detection.radius);
        Handles.DrawLine(detection.transform.position, detection.transform.position + cutoffAngleB * detection.radius);


        if (detection.canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(detection.transform.position, detection.playerRef.transform.position);
        }
        
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
