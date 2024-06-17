using UnityEngine;
using System.Text.RegularExpressions;
using TMPro;

/// <summary>
/// Este script hace parte de una combiancion entre los dos tutoriales por nuestra parte. En este se asigna el nombre de usuario al perfil del .json
/// Tambien se realiza una comprobacion para saber si el perfil ya existe en el "CheckProfileName". Crea los perfiles segun el nombre que escribamos en el
/// TMPro. Esta vinculado al canvas "AddDataMenu"
/// </summary>

public class NewGameController : MonoBehaviour
{
    #region Variables
    [SerializeField] public TMP_InputField profileId;
    [SerializeField] public string profilename;
    [SerializeField] private PopUpController popUpController; //Para mensaje de Advertencia o PupUp
    
    [SerializeField] private GameObject profileSelectMenu;
    [SerializeField] private GameObject addDataMenu;
    [SerializeField] private GameObject mainMenuT;

    [SerializeField] private SaveSlotsMenu saveSlotsMenu; //
    [SerializeField] private CheckFolders checkFolders;

    public UiController estaEnLoading;

    public bool limpiarSiTieneAlgo;
    #endregion

    public void EraseTypedName()
    {        
        profileId.text = null;        
        Debug.Log("Borrando TEXT del TMPro.Text - Null"); 
    }
    // ************************************************************************************************************************************************************
    public void CheckProfileName()
    {
        profilename = profileId.text.Trim();

        if (string.IsNullOrEmpty(profilename))       // Revisa si no se  ha escrito nada
        {
            popUpController.ActivarPopUp("Debes introducir tu nombre");
            return;
        }

        if (!IsValidName(profilename))               // Caracteres permit
        {
            popUpController.ActivarPopUp("El nombre no puede contener símbolos especiales o números");
            return;
        }

        if (ProfileStorage.ProfileExists(profilename)) // Verif si la cuenta existe
        {
            popUpController.ActivarPopUp("La cuenta ya existe");
            return;
        }
        else
        {
            profileSelectMenu.SetActive(true);
            addDataMenu.gameObject.SetActive(false);
            //saveSlotsMenu.ActivateMenu(false);
            Generate(profilename);

            Debug.Log("Se Creo el perfil: " + ProfileStorage.s_currentProfile.name);
        }
    }
    // ************************************************************************************************************************************************************
    public void testUpdate() // TEST - BORRAR
    {
        //Debug.Log("Test Update en el Clear");
        //profileSelectMenu.SetActive(false);
        //profileSelectMenu.SetActive(true);
    }
    // ************************************************************************************************************************************************************
    public void CheckSlotsAvailable()
    {
        if (checkFolders.slotsFull)
        {
            string message = "No hay espacio disponible. Dirigete al menu\n<b>Cargar Juego</b>\n para liberar espacio";
            popUpController.ActivarPopUp(message);
            addDataMenu.SetActive(false);
            mainMenuT.SetActive(true);
            return;
        }
        else
        {
            Debug.Log("Pasando por CheckFolders de NewGameController.");
        }
    }
    // ************************************************************************************************************************************************************
    public void Generate(string nombre)
    {
        profilename = profileId.text.Trim();
        ProfileStorage.CreateNewProfileName(nombre);
    }
    // ************************************************************************************************************************************************************
    public void DeleteProfileName()
    {        
        Debug.Log("DeleteMetodo - Borrando: " + ProfileStorage.s_currentProfile.name);
        ProfileStorage.DeleteProfile(ProfileStorage.s_currentProfile.name + ".xml");
        ProfileStorage.DeleteProfile(ProfileStorage.s_currentProfile.name + ".xml.meta");
    }
    // ************************************************************************************************************************************************************
    private bool IsValidName(string name)
    {
        Regex regex = new Regex("^[a-zA-Z ]+$"); // Si en algun momento, el nombre tiene valores diferentes a letras o espacios. Saltara PopUp
        return regex.IsMatch(name);
    }
}

/// <summary> Recursos - Tutorial Carga datos .json
/// Tutorial primario: https://www.youtube.com/watch?v=Q4S_rhPHwcs
/// Tutorial: https://www.youtube.com/watch?v=aUi9aijvpgs&list=PL3viUl9h9k7-tMGkSApPdu4hlUBagKial&index=1&t=0s
/// </summary>