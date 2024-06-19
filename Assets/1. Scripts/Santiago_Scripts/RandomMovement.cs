using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Cambios 1906 - Modificado para realizar movimiento hacia Accion.
/// </summary>
public class RandomMovement : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float wanderTimer = 2f;

    private Transform target;
    private NavMeshAgent agent;
    public float timer;

    [Header("De Accion")]
    public SantiInteractiveObjectController[] santiActions; // Array de objetos de acción

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        SetNewDestination();
    }
    void Update()
    {
        timer -= Time.deltaTime;

        // Verificar el estado de cada objeto de acción
        foreach (var santiAction in santiActions)
        {
            if (!santiAction.isSantiClose && !santiAction.isInAction && santiAction.isFree)
            {
                if (Vector3.Distance(transform.position, santiAction.santiAction.position) < santiAction.actionDistance)
                {
                    santiAction.isSantiClose = true;
                    santiAction.isInAction = true;
                    santiAction.isFree = false;
                    SantiMoveToAction(santiAction.santiAction.position);
                    return;
                }
            }
        }

        // Movimiento aleatorio si no se está interactuando con ningún objeto
        if (timer <= 0f && agent.remainingDistance <= agent.stoppingDistance)
        {
            SetNewDestination();
            timer = wanderTimer;
        }
    }

    void SetNewDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1);
        Vector3 finalPosition = hit.position;
        agent.SetDestination(finalPosition);
    }

    public void SantiMoveToAction(Vector3 santiActionPosition)
    {
        agent.SetDestination(santiActionPosition);
    }
}
