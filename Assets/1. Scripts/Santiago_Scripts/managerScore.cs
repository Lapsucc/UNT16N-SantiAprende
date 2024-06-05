using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class managerScore : MonoBehaviour
{
    public float puntos = 0f;
    public float chocolate = 0.1f;
    public float peluche = -0.1f;

    public Slider barraProgreso; // Referencia al Slider que mostrará los puntos
    public void OnButtonClickChocolate()
    {
        
        puntos= puntos + chocolate; // Aumentar la cantidad de puntos cada vez que se toca el botón
        Debug.Log("chocolate");
        ActualizarUI(); // Actualizar la interfaz de usuario
    }
    public void OnButtonClickpeluche()
    {

        puntos = puntos+peluche; // Aumentar la cantidad de puntos cada vez que se toca el botón
        Debug.Log("peluche");
        ActualizarUI(); // Actualizar la interfaz de usuario
    }
    void ActualizarUI()
    {
        // Actualizar el valor del Slider para reflejar los puntos
        barraProgreso.value = puntos;
    }
}
