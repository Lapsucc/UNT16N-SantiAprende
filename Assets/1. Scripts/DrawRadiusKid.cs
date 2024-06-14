using UnityEngine;

[ExecuteAlways]
public class DrawRadiusKid : MonoBehaviour
{
    public float radius = 1.0f;
    public Color gizmoColor;
    public int segments = 100;

    void OnDrawGizmos()
    {
        Color originalColor = Gizmos.color;
        Gizmos.color = gizmoColor;
        float angleStep = 360.0f / segments;

        for (int i = 0; i < segments; i++)
        {
            float angle1 = Mathf.Deg2Rad * angleStep * i;
            float angle2 = Mathf.Deg2Rad * angleStep * (i + 1);

            Vector3 point1 = new Vector3(Mathf.Cos(angle1) * radius, 0, Mathf.Sin(angle1) * radius) + transform.position;
            Vector3 point2 = new Vector3(Mathf.Cos(angle2) * radius, 0, Mathf.Sin(angle2) * radius) + transform.position;

            Gizmos.DrawLine(point1, point2);
        }

        Gizmos.color = originalColor;
    }
}