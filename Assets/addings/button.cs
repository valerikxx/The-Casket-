using UnityEngine;
using UnityEngine.UI;

public class DoorButton : MonoBehaviour
{
    public Button openButton;       // ссылка на UI-кнопку
    public Transform door;          // объект двери
    public float openAngle = 90f;   // угол открытия
    public float speed = 2f;        // скорость анимации

    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        // сохраняем исходное положение двери
        closedRotation = door.localRotation;
        openRotation = door.localRotation * Quaternion.Euler(0, openAngle, 0);

        // подписываемся на событие нажатия кнопки
        openButton.onClick.AddListener(ToggleDoor);
    }

    void ToggleDoor()
    {
        isOpen = !isOpen; // переключаем состояние
    }

    void Update()
    {
        if (isOpen)
        {
            door.localRotation = Quaternion.Slerp(door.localRotation, openRotation, Time.deltaTime * speed);
        }
        else
        {
            door.localRotation = Quaternion.Slerp(door.localRotation, closedRotation, Time.deltaTime * speed);
        }
    }
}
