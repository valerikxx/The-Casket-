using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SpawnedObjectEvent : UnityEvent<GameObject> { }

public class OneTimeSpawner : MonoBehaviour
{
    [Header("Настройки спавна")]
    public GameObject prefabToSpawn;
    public Transform spawnPoint;

    [Header("Событие после спавна")]
    public SpawnedObjectEvent onSpawned;

    private bool hasSpawned = false;

    public void Spawn()
    {
        if (hasSpawned) return; // Спавним только один раз

        if (prefabToSpawn == null || spawnPoint == null)
        {
            Debug.LogWarning("Не указан префаб или точка спавна!");
            return;
        }

        GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);

        onSpawned?.Invoke(spawnedObject);

        hasSpawned = true;
    }
}
