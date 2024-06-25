using System.Collections;
using UnityEngine;

/// <summary>
/// 1906 - 
/// 2106 - Ya funciona "ideal" por ahora.
/// </summary>
public class SantiInteractiveObjectController : MonoBehaviour
{
    public Transform santiAction;

    public RandomMovement santiAgent;
    public InterestBarValueController accionObjeto;

    public float actionDistance;
    public float actionTime;
    public float timer;

    public bool isFree = true;
    public bool isObjectActive = true;
    public bool isInAction = false;

    private void Start()
    {
        timer = actionTime;
    }

    public void Update()
    {
        if (!isInAction)
        {
            float distance = Vector3.Distance(santiAction.position, santiAgent.transform.position);
            if (distance < actionDistance && isObjectActive && !santiAgent.movingAwayFromPsicologist)
            {
                isFree = false;
                StartCoroutine(OnActionObject());
                Debug.Log("Estoy dentro de la distancia de acción");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Kid"))
        {
            if (isObjectActive)
            {
                isObjectActive = false;
                timer = actionTime; // Reinicia el timer al valor de acción inicial
                Debug.Log("ONTRIGGER ENTER");
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Kid"))
        {
            Debug.Log("ON TRIGGER STAY");

            if (!isInAction)
            {
                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    StartCoroutine(GetPositiveInterest());
                    isFree = true;
                }
            }
        }
    }

    public IEnumerator OnActionObject()
    {
        santiAgent.SantiMoveToAction(santiAction.position);
        // Espera hasta que el agente llegue a su destino
        while (santiAgent.agent.pathPending || santiAgent.agent.remainingDistance > santiAgent.agent.stoppingDistance)
        {
            yield return null;
        }
        // Ejecuta la acción cuando llega al destino
        accionObjeto.PositiveBarValue(0.2f, 5);
        yield return new WaitForSeconds(5); // Espera durante 5 segundos        

        santiAgent.setTimer();
    }

    public IEnumerator GetPositiveInterest()
    {
        isInAction = true;        
        yield return new WaitForSeconds(10f); // Espera durante 10 segundos

        StartCoroutine(ActiveActionObject());
    }

    public IEnumerator ActiveActionObject()
    {
        yield return new WaitForSeconds(20);
        isInAction = false;
        isObjectActive = true;
    }
}
