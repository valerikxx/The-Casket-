using UnityEngine;
using UnityEngine.AI; 

public class NPCController : MonoBehaviour
{
    public Transform[] waypoints;   // точки патруля
    public float detectionRange = 5f; // радиус обнаружения игрока
    public Transform player;        // ссылка на игрока

    private NavMeshAgent agent;
    private int currentWaypoint = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GoToNextWaypoint();
    }

    void Update()
    {
        // проверяем расстояние до игрока
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            // если игрок рядом — идём к нему
            agent.SetDestination(player.position);
        }
        else
        {
            // если игрок далеко — патрулируем
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                GoToNextWaypoint();
            }
        }
    }

    void GoToNextWaypoint()
    {
        if (waypoints.Length == 0) return;

        agent.destination = waypoints[currentWaypoint].position;
        currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
    }
}
