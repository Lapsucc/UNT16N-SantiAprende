using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// CONTROLA LOS VALORES de la probabilidad de exito de cada comportamiento.
/// </summary>

// ****************** :::::::::   A TENER EN CUENTAA - El valor de interes, DEBERIA BAJAR al usarlo en cada accion. 04/06____
public class InterestBarValueController : MonoBehaviour
{
    [SerializeField] private InstructionValueController probabilityValue;
    [SerializeField] private Slider sliderValue;    

    public float value;

    [SerializeField] private float reductionSpeed = 0.1f;      // Controla la velocidad de reducción    

    [SerializeField] private bool isLow;
    [SerializeField] private bool isMid;
    [SerializeField] private bool isHigh;

    void Start()
    {   
        isLow = false;
        isMid = false;
        isHigh = false;
    }

    void Update()
    {
        value = sliderValue.value;

        if (value > 0.6f)  // REDUCCION DEL VALOR DEL SLIDER CON EL TIEMPO
        {
            value -= reductionSpeed * Time.deltaTime;
            if (value < 0.5f) value = 0.5f;
            sliderValue.value = value;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {            
            positiveBarValue();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            negativeBarValue();
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

    public void negativeBarValue()     
    {
        Debug.Log("Reduciendo Valor Barra Interes");
        value -= 0.05f;
        sliderValue.value = value;

    }
    public void positiveBarValue()
    {
        Debug.Log("Aumentando Valor Barra Interes");
        value += 0.05f;
        sliderValue.value = value;
    }
}
