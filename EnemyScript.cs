using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public Transform[] pathPoints; // Points the enemy will follow
    public float detectionRadius = 10f; // Radius within which the player is detected
    public float chaseSpeed = 6f; // Speed when chasing the player
    public float normalSpeed = 3.5f; // Normal speed when following the path
    public Transform player; // Reference to the player

    private NavMeshAgent navMeshAgent;
    private int currentPathIndex = 0;
    private bool isChasing = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = normalSpeed;
        MoveToNextPathPoint();
    }

    void Update()
    {
        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            PatrolPath();
            CheckForPlayer();
        }
    }

    void PatrolPath()
    {
        if (navMeshAgent.remainingDistance < 0.5f && !navMeshAgent.pathPending)
        {
            currentPathIndex = (currentPathIndex + 1) % pathPoints.Length;
            MoveToNextPathPoint();
        }
    }

    void MoveToNextPathPoint()
    {
        if (pathPoints.Length > 0)
        {
            navMeshAgent.destination = pathPoints[currentPathIndex].position;
        }
    }

    void CheckForPlayer()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        if (distanceToPlayer < detectionRadius)
        {
            isChasing = true;
            navMeshAgent.speed = chaseSpeed;
        }
    }

    void ChasePlayer()
    {
        navMeshAgent.destination = player.position;
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        if (distanceToPlayer > detectionRadius)
        {
            isChasing = false;
            navMeshAgent.speed = normalSpeed;
            MoveToNextPathPoint();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
