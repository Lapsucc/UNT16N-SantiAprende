using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class managerScore : MonoBehaviour
{
    public float puntos = 0f;
    public float chocolate = 0.1f;
    public float peluche = -0.1f;

    public Slider barraProgreso; 

    public void OnButtonClickChocolate()
    {
        
        puntos= puntos + chocolate;
        Debug.Log("chocolate");
        ActualizarUI();
    }
    public void OnButtonClickpeluche()
    {

        puntos = puntos+peluche; // Aumentar la cantidad de puntos cada vez que se toca el botón
        Debug.Log("peluche");
        ActualizarUI(); // Actualizar la interfaz de usuario
    }
    void ActualizarUI() // SUBIR O BAJAR PUNTOS
    {        
        barraProgreso.value = puntos;
    }
}
