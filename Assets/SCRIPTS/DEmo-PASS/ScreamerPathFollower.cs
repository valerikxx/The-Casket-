using System.Linq;
using UnityEngine;

public class ScreamerPathFollower : MonoBehaviour
{
    public int pathID = 1;
    public float speed = 5f;
    public float rotationSpeed = 5f; // скорость поворота

    private Transform[] waypoints;
    private int currentIndex = 0;
    private bool isMoving = false;

    void OnEnable()
    {
        InitPath();
        if (waypoints.Length > 0)
        {
            transform.position = waypoints[0].position; // телепорт в старт
            currentIndex = 1; // двигаться начинаем со второй точки
            isMoving = true;
        }
        else
        {
            Debug.LogWarning("Нет точек пути для pathID = " + pathID);
            gameObject.SetActive(false);
        }
    }

    void InitPath()
    {
        var allWaypoints = FindObjectsOfType<ScreamerWaypoint>();

        waypoints = allWaypoints
            .Where(w => w.pathID == pathID)
            .OrderBy(w => w.orderInPath)
            .Select(w => w.transform)
            .ToArray();
    }

    void Update()
    {
        if (!isMoving || currentIndex >= waypoints.Length) return;

        Transform target = waypoints[currentIndex];

        // ---- Движение ----
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // ---- Поворот ----
        Vector3 direction = target.position - transform.position;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(-direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // ---- Проверка достижения точки ----
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentIndex++;

            if (currentIndex >= waypoints.Length)
            {
                isMoving = false;
                gameObject.SetActive(false);
            }
        }
    }
}
