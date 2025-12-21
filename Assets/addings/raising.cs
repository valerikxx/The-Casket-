using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public string itemName;   // имя предмета
    public AudioClip pickupSound; // звук при сборе (опционально)

    private void OnTriggerEnter(Collider other)
    {
        // проверяем, что столкнулся игрок
        if (other.CompareTag("Player"))
        {
            // можно добавить предмет в инвентарь
            Debug.Log("Игрок собрал: " + itemName);

            // проигрываем звук
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }

            // уничтожаем объект после сбора
            Destroy(gameObject);
        }
    }
}
