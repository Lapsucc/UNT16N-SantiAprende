using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deteccion : MonoBehaviour
{
    [SerializeField] public GameObject menuClic;
    private void OnMouseDown()
    {
        Debug.Log("si sirve");
        menuClic.SetActive(true);
    }
}
