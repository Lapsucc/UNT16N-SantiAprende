using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;
/// <summary>
/// Este lleva varias funciones y es el mas robusto del sistema de Guardado/Carga en .json.
/// En este se crean los archivos
/// 
/// </summary>

public class DataPersistenceManager : MonoBehaviour
{
    #region Variables
    [Header("Debugging PersistData")]
    [SerializeField] private bool disableDataPersistance = false;
    [SerializeField] private bool initializeDataIfNull = false;
    [SerializeField] private bool overrrideSelectedProfileId = false;
    [SerializeField] private string testSelectedProfileId = "test";

    [Header("File Storage Config")]
    [SerializeField] private string fileName;               // ESTA ESCRITO EN EL INSPECTOR COMO 1
    [SerializeField] private bool useEncryption;            // Para encriptar los datos (Del tutorial)

    [Header("AutoSave Config")]
    [SerializeField] private float autoSaveTimeSenconds = 60f;

    private DatosJuego gameData;
    private List<IDataPersistance> dataPersistancesObjects;
    private FileDataHandler dataHandler;

    //DEL video PROFILES
    private string selectedProfileId = "";
    private Coroutine autoSaveCoroutine;
    public static DataPersistenceManager instance { get; private set; }
    #endregion

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Borrando el Data Persistance que exista en la nueva escena");
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        if (disableDataPersistance)
        {
            Debug.LogWarning("DataPersistance esta desactivado");
        }

        // Creacion del archivo formato .json Que contiene los datos de partida.
        this.dataHandler = new FileDataHandler(Application.dataPath + "/DatosDeUsuario/Slots", fileName, useEncryption); 

        InitializeSelectedProfileId();
    }

    // *******  ACCIONES AL CARGAR UNA ESCENA Y AL CERRARLA
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;        
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    // **************************
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)          // Al Cargar La escena
    {
        Debug.Log("De DataPersistanceManager.cs On Scene Load llamado");
        this.dataPersistancesObjects = FindAllDataPersistanceObjects();
        LoadGame(); // Cargar el juego al iniciar

        // AUTOSAVE - Start up el autosave
        if (autoSaveCoroutine != null)
        { 
            StopCoroutine(autoSaveCoroutine);        
        }
        autoSaveCoroutine = StartCoroutine(AutoSave());
    }    
    // **************************
    public void ChangeSelectedProfileId(string newProfileId)
    {
        this.selectedProfileId = newProfileId;
        LoadGame();
    }
    // **************************
    public void DeleteProfileData(string profileId) //Debe ser llamado con el nombre del profile. Llamado por SaveSlotsMenu
    {
        dataHandler.Delete(profileId);        
        InitializeSelectedProfileId();        
        LoadGame();
    }
    // **************************
    private void InitializeSelectedProfileId()
    {
        this.selectedProfileId = dataHandler.GetMostRecentlyUpdatedProfileId();

        if (overrrideSelectedProfileId)
        {
            this.selectedProfileId = testSelectedProfileId;
            Debug.LogWarning("Sobrescribio el perfil actual con TEST id: " + testSelectedProfileId);
        }
    }

    // **************************
    public void NewGame()
    { 
        this.gameData = new DatosJuego();
    }
    // **************************
    public void LoadGame()
    {        
        if(disableDataPersistance)
        {
            return;
        }
                
        this.gameData = dataHandler.Load(selectedProfileId);
                
        if (this.gameData == null && initializeDataIfNull) // inicia una nueva partida si Data es Null
        {
            NewGame();
        }
        
        if (this.gameData == null)                         // Si no  hay carga de datos, no continua (MSG de advertencia).
        {
            Debug.Log("No se ha encontrado ningun perfil. Es necesario iniciar un Juego Nuevo");            
            return;
        }

        foreach (IDataPersistance dataPersistanceObj in dataPersistancesObjects)
        {
            dataPersistanceObj.LoadData(gameData);
        }        
    }
    // **************************
    public void SaveGame() 
    {
        if (disableDataPersistance)
        {
            return;
        }
        
        if (this.gameData == null)  // Si no hay datos para guardar, saltar un WARNING
        {
            Debug.LogWarning("No hay datos. Se debe iniciar un juego nuevo para poder grabar los datos");
            return;
        }

        foreach (IDataPersistance dataPersistanceObj in dataPersistancesObjects)
        {
            dataPersistanceObj.SaveData(gameData);
        }
        
        gameData.lastUpdated = System.DateTime.Now.ToBinary();  // En esta linea se guarda la fecha de guardado         
        dataHandler.save(gameData, selectedProfileId);          // Save that data to a file using the data handler ** Tuto
    }
    //**************************
    private void OnApplicationQuit()
    {
        SaveGame(); // Guardar juego al salir
    }
    //**************************
    private List<IDataPersistance> FindAllDataPersistanceObjects()
    { 
        IEnumerable<IDataPersistance> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>(true).OfType<IDataPersistance>();

        return new List<IDataPersistance>(dataPersistanceObjects);
    }
    //**************************
    public bool HasGameData()
    {
        return gameData != null;
    }
    //**************************
    public Dictionary<string, DatosJuego> GetAllProfilesGameData()
    {
        return dataHandler.LoadAllProfiles();
    }
    //**************************
    private IEnumerator AutoSave()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoSaveTimeSenconds);
            SaveGame();
            Debug.Log("Auto Saved Game");
        }
    }
}

/// <summary> Recursos - Tutorial Carga datos .json
/// Tutorial: https://www.youtube.com/watch?v=aUi9aijvpgs&list=PL3viUl9h9k7-tMGkSApPdu4hlUBagKial&index=1&t=0s
/// </summary>
