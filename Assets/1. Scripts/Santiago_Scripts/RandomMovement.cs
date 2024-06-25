using UnityEngine;
using UnityEngine.AI;

public class RandomMovement : MonoBehaviour
{
    public float minWanderRadius = 5f;
    public float maxWanderRadius = 10f;
    public float wanderTimer = 5f;
    public float stuckThreshold = 3f; // Tiempo en segundos antes de considerar que el agente está atascado
    public float safeDistance = 5f; // Distancia mínima al psicologist para moverse en dirección contraria

    public NavMeshAgent agent;
    public GameObject psicologist; //2506 -Antisocial
    public bool isSociable; // Condición para moverse lejos del psicologist

    private float timer;
    private float stuckTimer;
    private bool isMoving;
    public bool movingAwayFromPsicologist;
    private Vector3 lastPosition;

    [Header("De Accion")]
    public SantiInteractiveObjectController[] santiActions;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        stuckTimer = 0f;
        isMoving = false;
        movingAwayFromPsicologist = false;
        lastPosition = transform.position;
        isSociable = GetComponent<AproachDecreseValueController>().isSociable;
        SetNewDestination();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        bool anyActionInProgress = false;
        foreach (var santiAction in santiActions)
        {
            if (!santiAction.isFree)
            {
                anyActionInProgress = true;
                timer = wanderTimer;
                break;
            }
        }

        if (agent.pathPending)
        {
            stuckTimer = 0f;
        }
        else
        {
            if (agent.remainingDistance > agent.stoppingDistance)
            {
                isMoving = true;
                stuckTimer += Time.deltaTime;

                if (Vector3.Distance(transform.position, lastPosition) > 0.1f)
                {
                    stuckTimer = 0f;
                }

                if (stuckTimer >= stuckThreshold) // Comprobar si el agente esta atascado
                {
                    Debug.Log("Agent is stuck, finding a new path");
                    SetNewDestination();
                    stuckTimer = 0f;
                    timer = wanderTimer;
                }
            }
            else
            {
                if (isMoving)
                {
                    isMoving = false;
                    timer = wanderTimer;

                    // Si estaba alejándose del psicologist y ha terminado, reiniciar el movimiento normal
                    if (movingAwayFromPsicologist)
                    {
                        movingAwayFromPsicologist = false;
                    }
                }
            }
        }

        lastPosition = transform.position;

        if (!anyActionInProgress && !isMoving && timer <= 0f)
        {
            SetNewDestination();
            timer = wanderTimer;
        }

        // Lógica para moverse en dirección contraria al psicologist
        if (!isSociable && !movingAwayFromPsicologist && !anyActionInProgress)
        {
            float distanceToPsicologist = Vector3.Distance(transform.position, psicologist.transform.position);
            if (distanceToPsicologist < safeDistance)
            {
                MoveAwayFromPsicologist();
            }
        }
    }

    public void SetNewDestination()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            Debug.Log("Movimiento NORMAL");
            Vector3 randomPosition = GetRandomNavMeshPositionWithinRange(transform.position, minWanderRadius, maxWanderRadius);
            agent.SetDestination(randomPosition);
            isMoving = true;
            timer = wanderTimer;
        }
    }

    public void MoveAwayFromPsicologist()
    {
        Vector3 directionAwayFromPsicologist = (transform.position - psicologist.transform.position).normalized;
        Vector3 newDestination = GetRandomNavMeshPositionWithinRange(transform.position + directionAwayFromPsicologist * minWanderRadius, minWanderRadius, maxWanderRadius);
        NavMeshHit hit;
        if (NavMesh.SamplePosition(newDestination, out hit, maxWanderRadius, NavMesh.AllAreas))
        {
            Debug.Log("Moviéndose en dirección contraria al psicologist");
            agent.SetDestination(hit.position);
            isMoving = true;
            movingAwayFromPsicologist = true;
            stuckTimer = 0f;
            timer = wanderTimer;
        }
        else
        {
            // Si no se encuentra una posición válida, buscar una nueva dirección
            SetNewDestination();
        }
    }

    Vector3 GetRandomNavMeshPositionWithinRange(Vector3 origin, float minRange, float maxRange)
    {
        Vector3 randomDirection;
        Vector3 finalPosition = origin;
        NavMeshHit hit;
        int maxAttempts = 30;
        int attempts = 0;
        do
        {
            float randomDistance = Random.Range(minRange, maxRange);
            randomDirection = Random.insideUnitSphere * randomDistance;
            randomDirection += origin;
            attempts++;
        } while (!NavMesh.SamplePosition(randomDirection, out hit, maxRange, NavMesh.AllAreas) && attempts < maxAttempts);

        if (attempts < maxAttempts)
        {
            finalPosition = hit.position;
        }

        return finalPosition;
    }

    public void SantiMoveToAction(Vector3 santiActionPosition)
    {
        Debug.Log("MOVIENDOSE");
        agent.SetDestination(santiActionPosition);
        isMoving = true;
        stuckTimer = 0f;
    }

    public void setTimer()
    {
        timer = 0.5f;
    }
}
