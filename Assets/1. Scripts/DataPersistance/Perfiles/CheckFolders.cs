using UnityEngine;
using System.IO;
/// <summary> Comentarios -
/// Este Script pretende revisar si los slots estan siendo usados (Cuando se usa un slot, se crea una carpeta).
/// Esta comprobacion que se hace revisando si existe el directorio 0, 1, 2
/// si los 3 existen al mismo tiempo, significa que los slots estan siendo usados.
/// 
/// Se plantea usar el slotsFull para condicionar otros scripts.
/// </summary>
public class CheckFolders : MonoBehaviour
{
    public bool slotsFull;
    public void CheckForFolders()
    {        
        if (Directory.Exists(Path.Combine(Path.Combine(Application.dataPath, "DatosDeUsuario", "Slots"), "0")) &&
            Directory.Exists(Path.Combine(Path.Combine(Application.dataPath, "DatosDeUsuario", "Slots"), "1")) &&
            Directory.Exists(Path.Combine(Path.Combine(Application.dataPath, "DatosDeUsuario", "Slots"), "2")))
        {     
            Debug.Log("CheckFolders.cs en Account Manager. ¡Las tres carpetas existen!");
            slotsFull = true;
        }
        else
        {         
            Debug.Log("CheckFolders.cs en Account Manager. No se encontraron todas las carpetas.");
            slotsFull = false;
        }
    }
}
