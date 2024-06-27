using UnityEngine;

/// <summary>
/// En este script se plantea la logica para obtener la distancia a la que esta la Psicologa del Niño, asi
/// se implementa una reduccion del valor de la barra de interes segun un rango de distancias.
/// </summary>

public class AproachDecreseValueController : MonoBehaviour
{
    [Header("Game Manager")]
    public GameManager gameManager;

    [Header("Reduccion Valor Cercania")]
    public InterestBarValueController reducedValue;
    public GameObject psicologist;
    public DrawRadiusKid color;

    public float distanceSantiReaction;

    void Start()
    {
        
    }

    void Update()
    {
        if (!gameManager.isSociable)
        {
            if (psicologist != null)
            {
                float distance = Vector3.Distance(transform.position, psicologist.transform.position);

                if (distance > distanceSantiReaction)
                {
                    //Debug.Log("Fuera de rango");
                    color.gizmoColor = Color.green;                    
                }
                else if (distance > distanceSantiReaction*0.6f && distance <= distanceSantiReaction)
                {
                    //Debug.Log("Algo cerca");
                    color.gizmoColor = Color.blue;                    
                }
                else if (distance > distanceSantiReaction*0.1f && distance <= distanceSantiReaction*0.5f)
                {
                    //Debug.Log("Muy cerca");
                    color.gizmoColor = Color.red;
                    reducedValue.ReduceBarValue();
                    
                }
                else
                {
                    //Debug.Log("Extremadamente cerca");
                }
            }
            else
            {
                //Debug.LogWarning("psicologist no está asignado en el Inspector");
            }
        }
    }
}

