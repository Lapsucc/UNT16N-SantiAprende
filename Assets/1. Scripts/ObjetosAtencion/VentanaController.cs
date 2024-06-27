using System.Collections;
using UnityEngine;

public class VentanaController : MonoBehaviour
{
    [Header("Game Manager")]
    public GameManager gameManager;
    [Header("")]
    public InterestBarValueController accionVentana;
    public Transform actionLocation;
    public ClickToMove movePsic;
    [Header("_")]    
    public float value;
    public float duration; // Duracion en Segundos.
    [Header("_")]
    public float coolDownAction;
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

        float distance = Vector3.Distance(actionLocation.position, movePsic.gameObject.transform.position);        

        if (distance < 1 && !action)
        {            
            StartCoroutine(DoingVentanaAction());
        }
    }

    void OnMouseDown()
    {
        if (!action)
        {
            Debug.Log("Dando clic sobre " + gameObject.name);
            movePsic.MoveToActionPosition(actionLocation.position);            
        }
    }

    IEnumerator DoingVentanaAction()
    {
        Debug.Log("Haciendo Ventana Action");

        action = true;

        if (gameManager.likesFreeSpace)
        {
            accionVentana.PositiveBarValue(value * gameManager.freeSpaceGP * gameManager.gainInterestPercentage, duration);
        }

        yield return new WaitForSeconds(1); // Ajustar con la duracion de la accion o ANIMACION 
    }
}


