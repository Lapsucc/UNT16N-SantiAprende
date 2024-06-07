using TMPro;
using UnityEngine;

/// <summary>
/// Con este se controlan las alertas y el texto que estas contienen, mediante un Pop Up, en el que se va a cambiar el mensaje, usando solo un asset de pop up
/// </summary>

public class PopUpController : MonoBehaviour
{
    public GameObject PopUpPanel; 
    public TextMeshProUGUI messageText;

    public void ActivarPopUp(string mensaje)
    {
        messageText.text = mensaje;

        if (PopUpPanel != null)
        {
            PopUpPanel.SetActive(true);
        }
    }
}

