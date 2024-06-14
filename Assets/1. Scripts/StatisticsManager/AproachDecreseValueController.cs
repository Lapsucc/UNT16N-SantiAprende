using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AproachDecreseValueController : MonoBehaviour
{
    public InterestBarValueController reducedValue;
    public GameObject psicologist;
    public DrawRadiusKid color;

    public bool isSociable = true;

    void Start()
    {
        
    }

    void Update()
    {
        if (!isSociable)
        {
            if (psicologist != null)
            {
                float distance = Vector3.Distance(transform.position, psicologist.transform.position);

                if (distance > 10)
                {
                    color.gizmoColor = Color.green;
                    Debug.Log("Fuera de rango");
                }
                else if (distance > 6 && distance <= 10)
                {
                    Debug.Log("Algo cerca");
                    color.gizmoColor = Color.blue;
                }
                else if (distance > 0.1 && distance <= 5)
                {
                    Debug.Log("Muy cerca");
                    color.gizmoColor = Color.red;
                    reducedValue.ReduceBarValue();
                    
                }
                else
                {
                    Debug.Log("Extremadamente cerca");
                }
            }
            else
            {
                Debug.LogWarning("psicologist no está asignado en el Inspector");
            }
        }
    }
}

