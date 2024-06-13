using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// CONTROLA LOS VALORES de la probabilidad de exito de cada comportamiento.
/// </summary>

// ****************** :::::::::   A TENER EN CUENTAA - El valor de interes, DEBERIA BAJAR al usarlo en cada accion. 04/06____
public class InterestBarValueController : MonoBehaviour
{
    [SerializeField] private InstructionValueController probabilityValue;
    [SerializeField] private ClickToMove psicologa;
    [SerializeField] private Slider sliderValue;
    [SerializeField] private float reductionSpeed;      // Controla la velocidad de reducción

    [SerializeField] private bool isLow;
    [SerializeField] private bool isMid;
    [SerializeField] private bool isHigh;

    public float value;

    public float timerNoActions;
    public float timerNoActionsIni;
    public bool inactivity = false;

    void Start()
    {
        isLow = false;
        isMid = false;
        isHigh = false;

        timerNoActionsIni = timerNoActions;
    }

    void Update()
    {
        value = sliderValue.value;

        #region Logica - Reduccion Valor Barra por Inactividad
        //POR REVISAR EFECTIVIDAD SCRIPT
        if (!psicologa.IsMoving() && timerNoActions >= 0.01)
        {
            timerNoActions -= Time.deltaTime;
        }

        if (psicologa.IsMoving())
        {
            timerNoActions = timerNoActionsIni;
            inactivity = false;
        }

        if (timerNoActions <= 0.01 && !inactivity)
        {
            //
            Debug.Log("El timer de inactividad esta en 0 y la inactividad es true");
            inactivity = true;
            timerNoActions = 0;
        }              

        if (value > 0.1f && inactivity)  // REDUCCION DEL VALOR DEL SLIDER CON EL TIEMPO & Si NO SE ESTA MOVIENDO LA PSICOLOGA
        {
            value -= reductionSpeed * Time.deltaTime;
            if (value < 0.1f) value = 0.1f;
            sliderValue.value = value;
        }
        #endregion

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            positiveBarValue(0.5f);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            negativeBarValue(0.5f);
        }

        // Logica de comprobacion de cambio de estado en la barra de interes. Estado anterior de CADA ESTADO de interes.
        bool wasLow = isLow;
        bool wasMid = isMid;
        bool wasHigh = isHigh;

        if (value >= 0 && value <= 0.30f)
        {
            isLow = true;
            isMid = false;
            isHigh = false;
        }
        else if (value >= 0.31 && value <= 0.6f)
        {
            isLow = false;
            isMid = true;
            isHigh = false;
        }
        else if (value > 0.6f)
        {
            isLow = false;
            isMid = false;
            isHigh = true;
        }

        // Se dice. Tiene valores bajos Y NO TENIA VALORES BAJOS (El cambio de valores y su inicializacion como falsos haciendo Was = a Is)
        if (isLow && !wasLow)
        {
            OnLowInterestValue();
        }

        if (isMid && !wasMid)
        {
            OnMidInterestValue();
        }

        if (isHigh && !wasHigh)
        {
            OnHighInterestValue();
        }
    }

    public void OnLowInterestValue()
    {
        Debug.Log("Bajos valores de interes");
        probabilityValue.probability1 = 0.1f;
        probabilityValue.probability2 = 0.45f;
        probabilityValue.probability3 = 0.45f;
    }

    public void OnMidInterestValue()
    {
        Debug.Log("Medios valores de interes");
        probabilityValue.probability1 = 0.5f;
        probabilityValue.probability2 = 0.25f;
        probabilityValue.probability3 = 0.25f;
    }

    public void OnHighInterestValue()
    {
        Debug.Log("Altos valores de interes");
        probabilityValue.probability1 = 0.80f;
        probabilityValue.probability2 = 0.10f;
        probabilityValue.probability3 = 0.10f;
    }

    // **********************************************************************
    public IEnumerator ChangeBarValueSmoothly(float changeAmount) // Corrutina para cambiar el valor de la barra de manera suave
    {
        float targetValue = Mathf.Clamp(sliderValue.value + changeAmount, 0, 1); //Valor a modificar
        float startValue = sliderValue.value;
        float duration = 0.5f;               //Velocidad con la que cambia de valor.
        float elapsed = 0f;

        while (elapsed < duration) // INTENTO DE HACER EL MOVIMIENTO DE LA BARRA MAS "SMOOTH"
        {
            float t = elapsed / duration;
            t = Mathf.SmoothStep(0f, 1f, t); 
            sliderValue.value = Mathf.Lerp(startValue, targetValue, t); 
            elapsed += Time.deltaTime;
            yield return null;
        }

        sliderValue.value = targetValue;
    }

    public void negativeBarValue(float value)
    {
        Debug.Log("Reduciendo Valor Barra Interes");
        StartCoroutine(ChangeBarValueSmoothly(-value)); // Llamar a la corrutina con un valor negativo
    }

    public void positiveBarValue(float value)
    {
        Debug.Log("Aumentando Valor Barra Interes");
        StartCoroutine(ChangeBarValueSmoothly(value)); // Llamar a la corrutina con un valor positivo
    }
}
