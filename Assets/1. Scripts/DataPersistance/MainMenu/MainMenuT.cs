using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuT : Menu
{
    [Header("Menu Navigation")]
    [SerializeField] private SaveSlotsMenu saveSlotsMenu;
    [SerializeField] private GameObject addDataMenu;

    [Header("Menu Buttons")]
    [SerializeField] private Button NewGameButton;
    [SerializeField] private Button ContinueGameButton;
    [SerializeField] private Button loadGameButton;

    [SerializeField] private CheckFolders checkFolders;

    private void Start()
    {
        DisableButtonDependingOnData();
    }

    private void OnEnable()
    {
        DisableButtonDependingOnData();
    }

    private void DisableButtonDependingOnData()
    {
        Debug.Log("MainMenuT.cs - Revisando si existenDatos, para deshabilitar botones Continuar y Cargar Juego");

        if (!DataPersistenceManager.instance.HasGameData())
        {
            ContinueGameButton.interactable = false;
            loadGameButton.interactable = false;
        }
    }

    public void OnNewGameClicked()
    {
        if (!checkFolders.slotsFull)
        {
            addDataMenu.SetActive(true);
            this.DeactivateMenu();
        }
        else
        {
            Debug.Log("Dando Click en Nuevo Juego - Slots Llenos");
        }
    }

    public void OnLoadGameClicked()
    {
        saveSlotsMenu.ActivateMenu(true);
        this.DeactivateMenu();
    }

    public void OnContinueGameClicked()
    {
        // REVISAR PARA AÑADIR DATO DE PERFIL DESDE EL MAIN MENU
        PlayerDataStatic.Algo = "sfbdfnsdjknfklsdnfklnsdlk";
        PlayerDataStatic.barValue = 1;


        DisableMenuButtons();
        SceneManager.LoadSceneAsync("EscenaGYM");
    }

    private void DisableMenuButtons()
    {
        NewGameButton.interactable = false;
        ContinueGameButton.interactable = false;
    }

    public void ActivateMenu()
    {
        this.gameObject.SetActive(true);
        DisableButtonDependingOnData();
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }

}

/// <summary> Recursos - Tutorial Carga datos .json
/// Tutorial: https://www.youtube.com/watch?v=aUi9aijvpgs&list=PL3viUl9h9k7-tMGkSApPdu4hlUBagKial&index=1&t=0s
/// </summary>
