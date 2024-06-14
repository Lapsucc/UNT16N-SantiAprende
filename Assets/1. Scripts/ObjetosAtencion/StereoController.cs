using UnityEngine;

public class StereoController : MonoBehaviour
{
    public InterestBarValueController accionMusica;
    public Transform actionLocation;
    public ClickToMove movePsic;
    public float value;
    public float duration;

    void OnMouseDown()
    {
        Debug.Log("Dando clic sobre " + gameObject.name);
        accionMusica.PositiveBarValue(value, duration);
        movePsic.MoveToActionPosition(actionLocation.position);
    }
}

