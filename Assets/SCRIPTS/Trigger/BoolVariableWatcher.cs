using UnityEngine;
using UnityEngine.Events;

public class InventoryBoolWatcher : MonoBehaviour
{
    [Header("ID предмета из InventoryManager")]
    public string itemID;

    [Header("Событие если собрал предмет")]
    public UnityEvent onCollected;

    [Header("Событие если НЕ собрал предмет")]
    public UnityEvent onNotCollected;

    private bool lastValue;

    private void Start()
    {
        if (InventoryManager.Instance == null)
        {
            Debug.LogError("InventoryManager не найден на сцене!");
            enabled = false;
            return;
        }

        lastValue = InventoryManager.Instance.IsItemCollected(itemID);
        CheckValue();
    }

    private void Update()
    {
        bool currentValue = InventoryManager.Instance.IsItemCollected(itemID);

        if (currentValue != lastValue)
        {
            CheckValue();
            lastValue = currentValue;
        }
    }

    private void CheckValue()
    {
        if (InventoryManager.Instance.IsItemCollected(itemID))
        {
            onCollected?.Invoke();
        }
        else
        {
            onNotCollected?.Invoke();
        }
    }
}
