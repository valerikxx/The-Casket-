using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursoreManager : MonoBehaviour
{
    void Start()
    {
        LockCursore();
    }
    public static void LockCursore()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public static void UnLockCursore()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
