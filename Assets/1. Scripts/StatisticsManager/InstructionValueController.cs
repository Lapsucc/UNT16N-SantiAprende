using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ESTE Script ejecuta la accion de GENERAR un valor, segun una probabilidad que cambiara SEGUN el interes, en InterestBarValueController.cs.
/// </summary>

public class InstructionValueController : MonoBehaviour
{    
    public float probability1;
    public float probability2;
    public float probability3;

    public SuccessBController valor;
    public InterestBarValueController interestValue;

    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.V)) // PROBAR NUMERO AL AZAR COMPORTAMIENTOS.
        {
            InstructionValueGenerator(valor);            
        }
    }

    public void InstructionValueGenerator(SuccessBController value) // COn este se introducen cambios a cada Bejavior
    {   
        float randomFloat = Random.value;
        //Debug.Log("Generando Valor de nivel: " + levelBehavior);
             
        if (randomFloat < probability1)
        {
            Debug.Log("El valor generado es 1.");
            value.behaviorValueResult = 1;
        }
        else if (randomFloat < probability1 + probability2)
        {
            Debug.Log("El valor generado es 2.");
            value.behaviorValueResult = 2;
        }
        else
        {
            Debug.Log("El valor generado es 3.");
            value.behaviorValueResult = 3;
        }
    }
}
