using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1906 - 
/// </summary>

public class SantiInteractiveObjectController : MonoBehaviour
{
    public Transform santiAction;
    public Transform freeSantiAction;

    public RandomMovement santiAgent;
    public InterestBarValueController accionObjeto;

    public float actionDistance;

    public bool isSantiClose = false;
    public bool isInAction = false;
    public bool isFree = true;
    public bool isObjectActive = true;

    void Update()
    {
        float distance = Vector3.Distance(santiAction.position, santiAgent.gameObject.transform.position);

        if (distance < actionDistance && !isSantiClose && isFree && isObjectActive)
        {            
            StartCoroutine(OnActionObject());            
        }

        if (distance < 1 && !isInAction)
        {
            StartCoroutine(WaitAndMove());
            accionObjeto.PositiveBarValue(0.2f, 5);            
        }

        if (distance > actionDistance)
        {
            isSantiClose = false;
            isInAction = false;
            isFree = true;
        }
    }

    IEnumerator OnActionObject()
    {
        isSantiClose = true;        
        isFree = false;
        santiAgent.SantiMoveToAction(santiAction.position);
        yield return new WaitForSeconds(5);

    }

    IEnumerator WaitAndMove()
    {        
        isInAction = true;
        isObjectActive = false;
        isSantiClose = false;

        yield return new WaitForSeconds(3);
                
        santiAgent.SantiMoveToAction(freeSantiAction.position);
        isFree = true;
        
        yield return new WaitForSeconds(4);
        StartCoroutine(ActiveActionObject());
    }

    IEnumerator ActiveActionObject()
    {
        yield return new WaitForSeconds(10);
        isObjectActive = true;
    }
}
