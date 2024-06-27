using System.Collections;
using UnityEngine;

/// <summary>
/// 1906 - 
/// 2106 - Ya funciona "ideal" por ahora.
/// </summary>
public class SantiInteractiveObjectController : MonoBehaviour
{
    [Header("Game Manager")]
    public GameManager gameManager;
    [Header("")]

    public Transform santiAction;

    public RandomMovement santiAgent;
    public InterestBarValueController accionObjeto;

    public float actionDistance;
    public float actionTime;
    public float timer;

    public bool isFree = true;
    public bool isObjectActive = true;
    public bool isInAction = false;

    [Header("Valor incremento")]
    public float value;
    public float time;

    private void Start()
    {
        timer = actionTime;
    }

    public void Update()
    {
        if (!isInAction && !santiAgent.movingAwayFromPsicologist)
        {
            float distance = Vector3.Distance(santiAction.position, santiAgent.transform.position);
            if (distance < actionDistance && isObjectActive && !santiAgent.movingAwayFromPsicologist)
            {
                isFree = false;
                StartCoroutine(OnActionObject());
                //Debug.Log("Estoy dentro de la distancia de acción");
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
                //Debug.Log("ONTRIGGER ENTER");
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Kid"))
        {
            //Debug.Log("ON TRIGGER STAY");
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
        accionObjeto.PositiveBarValue(value * gameManager.santiObjectGP * gameManager.gainInterestPercentage, time);
        yield return new WaitForSeconds(5); 

        santiAgent.setTimer();
    }

    public IEnumerator GetPositiveInterest()
    {
        isInAction = true;        
        yield return new WaitForSeconds(10f);

        StartCoroutine(ActiveActionObject());
    }

    public IEnumerator ActiveActionObject()
    {
        yield return new WaitForSeconds(20);
        isInAction = false;
        isObjectActive = true;
    }
}
