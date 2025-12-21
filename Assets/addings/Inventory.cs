using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // список предметов
    public List<string> items = new List<string>();

    // добавить предмет
    public void AddItem(string itemName)
    {
        items.Add(itemName);
        Debug.Log("Добавлен предмет: " + itemName);
    }

    // проверить наличие
    public bool HasItem(string itemName)
    {
        return items.Contains(itemName);
    }

    // вывести список
    public void PrintInventory()
    {
        Debug.Log("Инвентарь содержит:");
        foreach (string item in items)
        {
            Debug.Log("- " + item);
        }
    }
}
