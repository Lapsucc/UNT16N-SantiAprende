using UnityEngine;

public class StereoController : MonoBehaviour
{
    public InterestBarValueController accionMusica;
    public float value;
    public float duration; // Duracion en Segundos.

    void Start()
    {
        // Aquí puedes inicializar cualquier cosa que necesites en el Start
    }

    void Update()
    {
        // Aquí puedes poner cualquier lógica que necesites en el Update
    }

    // Método que se llama cuando se hace clic en el objeto
    void OnMouseDown()
    {
        Debug.Log("Dando clic sobre " + gameObject.name);
        accionMusica.positiveBarValue(value, duration);
    }
}

