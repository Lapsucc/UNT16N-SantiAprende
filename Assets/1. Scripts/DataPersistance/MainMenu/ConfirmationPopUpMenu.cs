using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class ConfirmationPopUpMenu : Menu
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private Button confirmationButton;
    [SerializeField] private Button cancelButton;

    public void ActivateMenu(string displayText, UnityAction confirmAction, UnityAction cancelAction)
    { 
        this. gameObject.SetActive(true);

        //set display text
        this.displayText.text = displayText;

        // REMOVE EXISTINg listeners 
        confirmationButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();

        //
        confirmationButton.onClick.AddListener(() =>
        {
            DeactivateMenu();
            confirmAction();
        });

        cancelButton.onClick.AddListener(() =>
        {
            DeactivateMenu();
            cancelAction();
        });           
    }
    private void DeactivateMenu()
    { 
        this.gameObject.SetActive(false);    
    }
}
