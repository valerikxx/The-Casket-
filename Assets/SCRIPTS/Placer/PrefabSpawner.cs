using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor; // Нужно для кнопки в инспекторе
#endif

public class PrefabSpawner : MonoBehaviour
{
    [Header("Настройки")]
    public GameObject prefab;         // Что спавнить
    public Transform spawnPoint;      // Где спавнить
    public UnityEvent onSpawn;        // Что вызвать после спавна

    //[ContextMenu("Spawn")] // Правая кнопка на скрипте -> Spawn
    public void Spawn()
    {
        if (prefab == null || spawnPoint == null)
        {
            Debug.LogWarning("Не назначен Prefab или SpawnPoint!" + gameObject.name);
            return;
        }
        Debug.LogWarning("СОЗДАНИЕ ОБЬЕКТА   " + gameObject.name);
        Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        
        onSpawn?.Invoke(); // Вызов событий после спавна
    }
}

#if UNITY_EDITOR
// Часть для кнопки прямо в инспекторе
[CustomEditor(typeof(PrefabSpawner))]
public class PrefabSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PrefabSpawner spawner = (PrefabSpawner)target;

        if (GUILayout.Button("Spawn"))
        {
            spawner.Spawn();
        }
    }
}
#endif
