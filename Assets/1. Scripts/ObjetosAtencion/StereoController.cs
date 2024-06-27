using System.Collections;
using UnityEngine;

public class StereoController : MonoBehaviour
{
    [Header("Game Manager")]
    public GameManager gameManager;
    [Header("")]
    public InterestBarValueController accionStereo;
    public Transform actionLocationStereo;
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

        float distance = Vector3.Distance(actionLocationStereo.position, movePsic.gameObject.transform.position);

        if (distance < 1 && !action)
        {
            StartCoroutine(DoingStereoAction());
        }
    }

    void OnMouseDown()
    {
        if (!action)
        {
            Debug.Log("Dando clic sobre " + gameObject.name);
            movePsic.MoveToActionPosition(actionLocationStereo.position);
        }
    }

    IEnumerator DoingStereoAction()
    {
        Debug.Log("Accion Stereo");

        action = true;

        if (gameManager.likesMusic)
        {
            accionStereo.PositiveBarValue(value * gameManager.MusicGP * gameManager.gainInterestPercentage, duration);
        }

        yield return new WaitForSeconds(1); // Ajustar con la duracion de la accion o ANIMACION 
    }
}

