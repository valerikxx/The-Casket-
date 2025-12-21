using UnityEngine;
using UnityEngine.Events;
public class TriggerEvent : MonoBehaviour
{
    [Header("Метод, который вызовется при входе игрока в триггер")]
    public UnityEvent onPlayerEnter;

    [Header("Задержка перед выполнением (в секундах)")]
    public float delay = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke(nameof(ExecuteEvent), delay);
        }
    }

    private void ExecuteEvent()
    {
        onPlayerEnter?.Invoke();
    }
}
