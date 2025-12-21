using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float sensitivity = 2.0f;   // чувствительность мыши
    public float clampAngle = 80.0f;   // ограничение угла по вертикали

    private float rotY = 0.0f; // угол по вертикали
    private float rotX = 0.0f; // угол по горизонтали

    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        Cursor.lockState = CursorLockMode.Locked; // фиксируем курсор
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotY += mouseX * sensitivity;
        rotX -= mouseY * sensitivity;

        // ограничиваем вертикальный угол
        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;
    }
}
