using UnityEngine;

public class PlayerRun : MonoBehaviour
{
    public float runSpeed = 10f;   // скорость бега
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // проверяем, удерживает ли игрок клавишу Shift
        if (Input.GetKey(KeyCode.LeftShift))
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * runSpeed * Time.deltaTime);
        }
    }
}

