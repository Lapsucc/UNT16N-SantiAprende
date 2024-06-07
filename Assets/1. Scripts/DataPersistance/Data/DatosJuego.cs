using UnityEngine;
/// <summary> Comentarios - 
/// Esta clase, es utilizada para asignar las variables que van a estar en el DataPersistance.
/// Mejor dicho, estas variables seran las que se guardaran en el .Json y seran cargadas en cada escena segun el perfil.
/// </summary>

public class DatosJuego
{
    // Fecha
    public long lastUpdated;
    // Profile name
    public string profileId;

    public int numeroEjemplo;    

    public Vector3 camPos; //posicion de objetos

    public SerializableDictionary<string, bool> accion; // Sera un boton en el canvas/panel
    
    public DatosJuego()
    {
        profileId = "";
        this.numeroEjemplo= 0;
        camPos = new Vector3(-100,1,100); // ubicacion inicial o de NEW GAME
        accion = new SerializableDictionary<string, bool>();
    }
}

/// <summary> Recursos - Tutorial Carga datos .json
/// Tutorial: https://www.youtube.com/watch?v=aUi9aijvpgs&list=PL3viUl9h9k7-tMGkSApPdu4hlUBagKial&index=1&t=0s
/// </summary>


