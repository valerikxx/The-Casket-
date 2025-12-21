using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public Transform door;          // объект двери или шкафчика
    public float openAngle = 90f;  
    public float speed = 2f;        
    private bool isOpen = false;    
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = door.localRotation;
        openRotation = door.localRotation * Quaternion.Euler(0, openAngle, 0);
    }

    void Update()
    {
        // проверяем нажатие клавиши E
        if (Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen; 
        }

        // вращение
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
