using UnityEngine;

/// <summary> POR CAMBIAR -
/// CON ESTE SE ESTA asignando el nombre del TMPro de "AddDataMenu" al Slot.
/// </summary>

public class TestNombrePerfil : MonoBehaviour , IDataPersistance // EL Nombre se debe cambiar. 31/05
{       
    public NewGameController NewGC_Name;    
    public GameObject addDataMenu;
    public GameObject saveSlotMenu;
    public CheckFolders checkFolders;

    //public static ProfileData s_currentProfile;

    public void LoadData(DatosJuego data) 
    {        
        NewGC_Name.profileId.text = data.profileId;
    }

    public void SaveData(DatosJuego data)                       // AQUI GUARDO EL NOMBRE que va a mostrarse en el slot
    {
        if (saveSlotMenu.activeSelf && !checkFolders.slotsFull)
        {
            Debug.Log("Salvando el nombre en el .Json de TestNombrePrefil: " + data.profileId);
            data.profileId = NewGC_Name.profileId.text;
        }
    }
}

