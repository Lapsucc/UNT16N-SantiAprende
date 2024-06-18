using UnityEngine;

public class deteccion : MonoBehaviour
{
    public GameObject menuClic;
    public Transform actionLocation;
    public ClickToMove movePsic;
    public bool isClose = false;

    public void Update()
    {
        float distance = Vector3.Distance(actionLocation.position, movePsic.gameObject.transform.position);

        if (distance < 1 && !isClose)
        {
            Debug.Log("Estoy cerca de caja reforzadores");
            isClose = true;
            menuClic.SetActive(true);
        }

        if (distance > 1)
        {
            isClose = false;
            menuClic.SetActive(false);
        }
    }

    private void OnMouseDown()
    {                
        movePsic.MoveToActionPosition(actionLocation.position);
    }
}
