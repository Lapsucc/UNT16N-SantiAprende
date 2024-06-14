using UnityEngine;

public class VentanaController : MonoBehaviour
{
    public InterestBarValueController accionVentana;
    public Transform actionLocation;
    public ClickToMove movePsic;


    public float value;
    public float duration; // Duracion en Segundos.
    

    void OnMouseDown()
    {
        Debug.Log("Dando clic sobre " + gameObject.name);
        accionVentana.PositiveBarValue(value, duration);
        movePsic.MoveToActionPosition(actionLocation.position);
    }
}

