using UnityEngine;

public class StereoController : MonoBehaviour
{
    public InterestBarValueController accionMusica;
    public float value;
    public float duration; // Duracion en Segundos.

    void Start()
    {
        // Aqu� puedes inicializar cualquier cosa que necesites en el Start
    }

    void Update()
    {
        // Aqu� puedes poner cualquier l�gica que necesites en el Update
    }

    // M�todo que se llama cuando se hace clic en el objeto
    void OnMouseDown()
    {
        Debug.Log("Dando clic sobre " + gameObject.name);
        accionMusica.positiveBarValue(value, duration);
    }
}

