using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SaveSlotsMenu : Menu
{
    [Header("Menu Navigation")]
    [SerializeField] private MainMenuT mainMenu;    

    [Header("Menu Buttons")]
    [SerializeField] private Button backButton;
    [SerializeField] private ConfirmationPopUpMenu confirmationPopUpMenu;

    
    public bool estaLoadingDEUI = false;
    public SaveSlot[] saveSlots;
    private bool isLoadingGame = false;

    public DatosJuego data;

    public UiController estaEnLoading; // ARREGLO 20/05
    public UiController uiController; // Para borrar el perfil index tuto 1
    public NewGameController newGameController;

    private void Awake()
    {
        saveSlots = this.GetComponentsInChildren<SaveSlot>();
    }
    public void Update()
    {
        if (estaEnLoading.estaLoading)
        {
            estaLoadingDEUI = true;
        }
        else
        { 
            estaLoadingDEUI = false;
        }
    }

    public void OnSaveSlotClicked(SaveSlot saveSlot)
    {        
        DisableMenuButtons();

        if (isLoadingGame) // DANDOLE A LOAD
        {
            DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId());
            Debug.Log("Se Cargo el perfil: " + saveSlot.nombrePerfil.text + " De la carpeta: " + saveSlot.GetProfileId());
            SaveGameAndLoadScene();
        }

        else if (saveSlot.hasData)
        {
            confirmationPopUpMenu.ActivateMenu(
                "Si deseas iniciar una partida nueva sobre este perfil, se sobreescribira\n¿estas segur@?", 
                
                //funcion para ejecutar si se le da a SI
                () => {
                    // Combinacion 2 tutoriales. PARA USAR PERFILES y EL SAVE DATA.
                    Debug.Log("Se SobreEscribio el perfil: " + saveSlot.nombrePerfil.text + " De la carpeta: " + saveSlot.GetProfileId());

                    ProfileStorage.DeleteProfile(saveSlot.GetProfileIdToDelete() +".xml");         // Borrar Archivo
                    ProfileStorage.DeleteProfile(saveSlot.GetProfileIdToDelete() +".xml.meta");  // Borrar Archivo.meta
                    DataPersistenceManager.instance.DeleteProfileData(saveSlot.GetProfileId());   // Borrar Carpeta Enumerada 

                    DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId()); // AQUI HAY ALGOOOOOO                    
                    DataPersistenceManager.instance.NewGame();                    
                    SaveGameAndLoadScene();
                },
                //funcion si se le da a Cancelar
                () => {
                    this.ActivateMenu(isLoadingGame);
                });
        }
        else
        {
            DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId());            
            DataPersistenceManager.instance.NewGame();            
            SaveGameAndLoadScene();

        }

    }

    private void SaveGameAndLoadScene()
    {
        DataPersistenceManager.instance.SaveGame();

        SceneManager.LoadSceneAsync("EscenaGYM");
    }


    public void OnDeleteClicked(SaveSlot saveSlot) // ON CLEAR DEL TUTO
    {
        DisableMenuButtons();

        confirmationPopUpMenu.ActivateMenu(
            "¿Estas segur@ que quieres borrar este perfil?",
            () =>            {                
                DataPersistenceManager.instance.DeleteProfileData(saveSlot.GetProfileId());
                ProfileStorage.DeleteProfile(saveSlot.GetProfileIdToDelete() + ".xml");
                ProfileStorage.DeleteProfile(saveSlot.GetProfileIdToDelete() + ".xml.meta");
                ProfileStorage.UpdateProfileIndex();

                //
                Debug.Log("AlPrecionar Delete");

                 ActivateMenu(isLoadingGame);

                if (!estaEnLoading.estaLoading)
                {
                    Debug.Log("Di Confirm y estoy en NewGame");
                    saveSlot.clearButton.interactable = true;                    
                    EnableMenuButtons();
                    ActivateMenu(false);

                    newGameController.DeleteProfileName();
                    this.gameObject.SetActive(false);
                    mainMenu.ActivateMenu();
                    
                }
                else
                {
                    Debug.Log("Di Confirm y estoy en LoadGame");
                    saveSlot.saveSlotButton.interactable = false;
                    backButton.interactable = true;
                    ActivateMenu(true);
                }
            },
            // si Cancel
            () =>
            {
                if (!estaEnLoading.estaLoading)
                {
                    Debug.Log("Di Cancel y estoy en NewGame");
                    DisableMenuButtons();
                    saveSlot.saveSlotButton.interactable = false;
                    EnableMenuButtons();
                    ActivateMenu(false);
                }
                else
                {
                    Debug.Log("Di Cancel y estoy en LoadGame");
                    backButton.interactable = true;
                    saveSlot.saveSlotButton.interactable = true;
                    DisableMenuButtons();
                    ActivateMenu(true);
                }

                ActivateMenu(isLoadingGame);
            }
        );

        this.ActivateMenu(true);

    }


    public void OnBackClicked()
    {
        if(!estaLoadingDEUI)
        {
            newGameController.DeleteProfileName();
        }
    }


    public void ActivateMenu(bool isLoadingGame)
    {        
        //set this menu to be active
        this.gameObject.SetActive(true);

        //set mode
        this.isLoadingGame = isLoadingGame;
        
        //
        Dictionary<string, DatosJuego> profilesGameData = DataPersistenceManager.instance.GetAllProfilesGameData();

        //
        backButton.interactable = true;

        //
        GameObject firstSelected = backButton.gameObject;

        //loop
        foreach (SaveSlot saveSlot in saveSlots)
        {
            DatosJuego profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileId(), out profileData);
            saveSlot.SetData(profileData);

            if (profileData == null && isLoadingGame)
            { 
                saveSlot.SetInteractable(false); // Hace que los botones de Continuar y Load esten activos o interactuables al haber datos
            }
            else
            {
                saveSlot.SetInteractable(true);

                if (firstSelected.Equals(backButton.gameObject))
                { 
                    firstSelected = saveSlot.gameObject;
                }
            }

            if (profileData != null && !isLoadingGame) // SOLUCION DESESPERADA 21/05 - DESABILITAR EN NEW GAME LAS PARTIDAS GUARDADAS
            {
                saveSlot.SetInteractable(false);
                saveSlot.clearButton.interactable = true;
            }
        }

        Button firstSelectedButton = firstSelected.GetComponent<Button>();

        this.SetFirstSelected(firstSelectedButton);
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);    
    }

    
    private void EnableMenuButtons() // NUEVO 20/05
    {
        foreach (SaveSlot saveSlot in saveSlots)
        {
            saveSlot.SetInteractable(true);
        }

        backButton.interactable = true;
    }
    

    private void DisableMenuButtons()
    {
        foreach (SaveSlot saveSlot in saveSlots)
        {
            saveSlot.SetInteractable(false);
        }

        backButton.interactable = false;
    }


}
