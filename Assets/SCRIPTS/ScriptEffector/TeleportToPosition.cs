using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToPosition : MonoBehaviour
{
    public Vector3 targetPosition;
    public GameObject Player;

    public void Teleporting()
    {
        // Телепортируем объект в targetPosition
        Player.transform.position = targetPosition;
    }
}
