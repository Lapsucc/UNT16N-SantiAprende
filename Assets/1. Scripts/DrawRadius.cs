using UnityEngine;

[ExecuteAlways]
public class DrawRadius : MonoBehaviour
{
    // Variable p�blica para definir el radio desde el Inspector
    public float radius = 1.0f;

    // Color del Gizmo en el editor
    public Color gizmoColor = Color.red;

    // N�mero de segmentos del c�rculo
    public int segments = 100;

    // Esta funci�n se llama en el editor para dibujar Gizmos
    void OnDrawGizmos()
    {
        // Guardar el color original de los Gizmos
        Color originalColor = Gizmos.color;

        // Establecer el color de nuestro Gizmo
        Gizmos.color = gizmoColor;

        // Calcular el �ngulo entre cada segmento
        float angleStep = 360.0f / segments;

        // Dibujar el c�rculo
        for (int i = 0; i < segments; i++)
        {
            // Calcular los �ngulos en radianes
            float angle1 = Mathf.Deg2Rad * angleStep * i;
            float angle2 = Mathf.Deg2Rad * angleStep * (i + 1);

            // Calcular las posiciones de los puntos del segmento
            Vector3 point1 = new Vector3(Mathf.Cos(angle1) * radius, 0, Mathf.Sin(angle1) * radius) + transform.position;
            Vector3 point2 = new Vector3(Mathf.Cos(angle2) * radius, 0, Mathf.Sin(angle2) * radius) + transform.position;

            // Dibujar la l�nea entre los puntos
            Gizmos.DrawLine(point1, point2);
        }

        // Restaurar el color original de los Gizmos
        Gizmos.color = originalColor;
    }
}
