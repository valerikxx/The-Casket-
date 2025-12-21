using UnityEngine;

public class CarEnter : MonoBehaviour
{
    public GameObject player;       // объект игрока
    public GameObject car;          // объект машины
    public Transform seatPoint;     // точка, куда "садится" игрок
    public KeyCode enterKey = KeyCode.E;

    private bool isInCar = false;

    void Update()
    {
        if (Input.GetKeyDown(enterKey))
        {
            if (!isInCar)
            {
                EnterCar();
            }
            else
            {
                ExitCar();
            }
        }
    }

    void EnterCar()
    {
        // перемещаем игрока в точку сиденья
        player.transform.position = seatPoint.position;
        player.transform.rotation = seatPoint.rotation;

        // отключаем управление игроком
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<PlayerController>().enabled = false; // твой скрипт ходьбы

        // включаем управление машиной
        car.GetComponent<CarController>().enabled = true;

        isInCar = true;
        Debug.Log("Игрок сел в машину");
    }

    void ExitCar()
    {
        // возвращаем управление игроку
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<PlayerController>().enabled = true;

        // отключаем управление машиной
        car.GetComponent<CarController>().enabled = false;

        // ставим игрока рядом с машиной
        player.transform.position = car.transform.position + car.transform.right * 2f;

        isInCar = false;
        Debug.Log("Игрок вышел из машины");
    }
}
