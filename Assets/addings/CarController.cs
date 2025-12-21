using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    public float speed = 10f;        // скорость движения
    public float turnSpeed = 50f;    // скорость поворота
    public float brakeForce = 20f;   // сила торможения

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // движение вперёд/назад (W/S)
        float move = Input.GetAxis("Vertical") * speed;
        Vector3 forwardMove = transform.forward * move;
        rb.AddForce(forwardMove);

        // поворот (A/D)
        float turn = Input.GetAxis("Horizontal") * turnSpeed * Time.fixedDeltaTime;
        transform.Rotate(0, turn, 0);

        // тормоз (пробел)
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(-rb.velocity * brakeForce);
        }
    }
}
