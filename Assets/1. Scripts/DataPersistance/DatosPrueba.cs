using UnityEngine;

/// <summary>
///   PRUEBA DE FUNCIONALIDAD DEL DATA PERSISTANCE. PRUEBA CON UN VALOR ENTERO, SER CARGADO Y SALVADO. con la TECLA X se aumenta el valor.
/// </summary>

public class DatosPrueba : MonoBehaviour, IDataPersistance
{
    public int numeroEjemplo;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            numeroEjemplo += 1;        
        }
    }
    public void LoadData(DatosJuego data)
    {        
        this.numeroEjemplo = data.numeroEjemplo;
        Debug.Log("Cargando Numero Ejemplo");
    }
    public void SaveData(DatosJuego data)
    {
        data.numeroEjemplo = this.numeroEjemplo;
        Debug.Log("Salvando Numero Ejemplo");
    }
}
