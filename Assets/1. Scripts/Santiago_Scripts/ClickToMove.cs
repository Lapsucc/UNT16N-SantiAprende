using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ClickToMove : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public LayerMask clickableLayer;
    public LayerMask sueloLayer;
    public string objectTag; // Tag de los objetos interactivos

    public int moveCounter;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
        {
            Debug.Log("Moviendome");

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity, clickableLayer | sueloLayer);

            bool interactivoEncontrado = false;

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.CompareTag(objectTag))
                {
                    Debug.Log("Haciendo clic sobre " + hit.collider.gameObject.name);
                    interactivoEncontrado = true;
                    // Comprobar el Onmouse BORRAR DESPUES - Depuracion
                    hit.collider.gameObject.SendMessage("OnMouseDown", SendMessageOptions.DontRequireReceiver);
                    break;
                }
            }

            if (!interactivoEncontrado)
            {
                if (Physics.Raycast(ray, out RaycastHit sueloHit, Mathf.Infinity, sueloLayer))
                {
                    navMeshAgent.SetDestination(sueloHit.point);
                }
                else
                {
                    Debug.Log("Raycast no hit");
                }
            }
        }
    }

    bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current)
        {
            position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
        };
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public void MoveToActionPosition(Vector3 actionPosition)
    {
        navMeshAgent.SetDestination(actionPosition);
    }

    public bool IsMoving()
    {
        return navMeshAgent.velocity.sqrMagnitude > 0.01f;
    }
}
