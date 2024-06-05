using UnityEngine;

/// <summary> 31/05 - por revisar uso.
/// Debo revisar su fucnionamiento, ya que se utilizo para solucionar un problema al principio del trabajo pero creo que su dependencia ya no es necesaria.
/// </summary>

public class UiController : MonoBehaviour
{
    public GameObject addDataMenu;
    public GameObject saveSlotMenu;
    public GameObject mainMenuT;
    public bool estaEnData;
    public bool estaLoading = false;

    public void OnBackFromSaveSlotMenu()
    {
        addDataMenu.SetActive(false);
        saveSlotMenu.SetActive(false);
        mainMenuT.SetActive(true);
    }
    public void IsLoadingSlotsMenu()
    {
        Debug.Log("Dio Click a Load");
        estaLoading = true;
    }

    public void IsNotLoadingSlotsMenu()
    {
        Debug.Log("Dio Click a NewGame");
        estaLoading = false;
    }   

    public void EstaData()
    { estaEnData = true; }

    public void NoEstaEnData()
    { estaEnData = false; }
}
