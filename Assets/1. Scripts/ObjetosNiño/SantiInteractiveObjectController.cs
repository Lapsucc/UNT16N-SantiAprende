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

        if (distance < actionDistance && isFree && isObjectActive)
        {
            isFree = false;
            StartCoroutine(OnActionObject());
        }



        /*
        if (distance > 1)
        {
            isInAction = false;
        }
        */

    }

    IEnumerator OnActionObject()
    {
        float distance = Vector3.Distance(santiAction.position, santiAgent.gameObject.transform.position);

        santiAgent.SantiMoveToAction(santiAction.position);
        
        if (distance < 1 && !isInAction)
        {
            isObjectActive = false;
            StartCoroutine(WaitAndMove());
        }
        yield return new WaitForSeconds(15);
    }

    IEnumerator WaitAndMove()
    {
        isInAction = true;
                
        if (isInAction)
        {
            accionObjeto.PositiveBarValue(0.2f, 5);
            yield return new WaitForSeconds(5);
        }

        isFree = true;
        isInAction = false;

        StartCoroutine(ActiveActionObject());
        yield return new WaitForSeconds(1);
    }

    IEnumerator ActiveActionObject()
    {
        // Wait for X seconds before reactivating the object
        yield return new WaitForSeconds(10);
        isObjectActive = true;
    }
}
