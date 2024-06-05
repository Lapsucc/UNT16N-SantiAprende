using UnityEngine;

public class InstructionValueController : MonoBehaviour
{
    private int levelBehavior = 1;
    public float probability1;
    public float probability2;
    public float probability3;
    public SuccessBController valor;

    void Start()
    {        
        probability1 = 0.1f;
        probability2 = 0.45f;
        probability3 = 0.45f;
    }

    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.V)) // PROBAR NUMERO AL AZAR COMPORTAMIENTOS.
        {
            InstructionValueGenerator(valor);            
        }
    }

    public void InstructionValueGenerator(SuccessBController value) // COn este se introducen cambios a cada Bejavior
    {
        //if (value.levelBehavior == 1)
        if (levelBehavior == 1)
        {
            probability1 = 0.1f;
            probability2 = 0.45f;
            probability3 = 0.45f;
        }

        if (levelBehavior == 2)
        {
            probability1 = 0.3f;
            probability2 = 0.35f;
            probability3 = 0.35f;
        }

        if (levelBehavior == 3)
        {
            probability1 = 0.60f;
            probability2 = 0.10f;
            probability3 = 0.30f;
        }

        // Logica para ejecutar comportamiento, segun el resultado de la INSTRUCCION.

        float randomFloat = Random.value;
        Debug.Log("Generando Valor de nivel: " + levelBehavior);
             
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
