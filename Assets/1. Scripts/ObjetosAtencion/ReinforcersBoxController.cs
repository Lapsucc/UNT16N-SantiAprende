using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReinforcersBoxController : MonoBehaviour
{
    public InterestBarValueController accionReinforcersBox;
    public Transform actionLocationReinforcerBox;
    public ClickToMove movePsic;
    public GameObject menuClic;
    [Header("_")]
    public float coolDownAction; // REVISAR, SITUACIONAL
    private float initialTimer;
    public bool action = false;

    void Start()
    {
        initialTimer = coolDownAction;
    }

    void Update()
    {
        if (action)
        {
            if (coolDownAction > 0)
            {
                coolDownAction -= Time.deltaTime;
            }
            else
            {
                coolDownAction = initialTimer;
                action = false;
            }
        }

        float distance = Vector3.Distance(actionLocationReinforcerBox.position, movePsic.gameObject.transform.position);

        if (distance < 1 && !action)
        {
            StartCoroutine(DoingReinformenceBox());            
        }

        if (distance > 1)
        {            
            menuClic.SetActive(false);
        }

    }

    void OnMouseDown()
    {
        if (!action)
        {            
            movePsic.MoveToActionPosition(actionLocationReinforcerBox.position);            
        }
    }

    IEnumerator DoingReinformenceBox()
    {
        action = true;
        menuClic.SetActive(true);
        yield return new WaitForSeconds(1); // Ajustar con la duracion de la accion o ANIMACION 
    }

    /*
  float distance = Vector3.Distance(actionLocation.position, movePsic.gameObject.transform.position);

    if (distance < 1 && !isClose)
    {
        //Debug.Log("Estoy cerca de caja reforzadores");
        isClose = true;
        menuClic.SetActive(true);
    }

    if (distance > 1)
    {
        isClose = false;
        menuClic.SetActive(false);
    }

 * */

}
