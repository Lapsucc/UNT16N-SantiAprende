using UnityEngine;

/// <summary> PROBANDO SAVE & LOAD
/// - POR CAMBIAR. Con este comprobamos que el sistema esta funcionando, para objetos o items que deben ser de alguna forma modificados
/// en sus otros componentes, como estar activo o el color (POR TESTEAR)
/// </summary>

public class AccionGOController : MonoBehaviour, IDataPersistance // ESTE SCRIPT VA A GRABAR SOBRE UN OBJETO. DEBE IR SOBRE CADA GAMEOBJECT A ALMACENAR
{    
    public bool accionBotonUsado = false;

    [SerializeField] private string id;
    [ContextMenu("generade guid for ID")]

    private void GenerateGUID()
    {
        id = System.Guid.NewGuid().ToString();
    }

    // *********** SAVE AND LOAD **********************************************************************************************************************************

    public void LoadData(DatosJuego data)
    {
        data.accion.TryGetValue(id, out accionBotonUsado);

        if (accionBotonUsado)
        {
            this.gameObject.SetActive(false);
        }
    }
    // ************************************************************************************************************************************************************
    public void SaveData(DatosJuego data)
    {
        if (data.accion.ContainsKey(id))
        {
            data.accion.Remove(id);
        }

        data.accion.Add(id, accionBotonUsado);
    }
    // ************************************************************************************************************************************************************
    public void AccionBoton()
    {
        Debug.Log("Click Accion");
        Invoke("DesactivarBoton", 2f);
        accionBotonUsado = true;
    }
    // ************************************************************************************************************************************************************
    public void DesactivarBoton()
    {
        this.gameObject.SetActive(false);
    }    
}
