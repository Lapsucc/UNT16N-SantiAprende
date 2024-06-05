using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("First Selected Button")] // Del EVENT MANAGER

    [SerializeField] private Button firstSelected;

    /*
    protected virtual void OnEnable()
    {
        SetFirstSelected(firstSelected);    
    }
    */
    
    public void SetFirstSelected(Button firstSelectedObject)
    {
        firstSelectedObject.Select();
    }
    
}
