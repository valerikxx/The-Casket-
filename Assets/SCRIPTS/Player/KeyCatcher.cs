using System.Collections;
using System.Collections.Generic;
using Michsky.UI.Dark;
using UnityEngine;

public class KeyCatcher : MonoBehaviour
{
    public ModalWindowManager modalWindowManagerExit;
    public KeyCode Escapes;
    void Update()
    {
        if (Input.GetKeyDown(Escapes))
        {
            CursoreManager.UnLockCursore();
            modalWindowManagerExit.ModalWindowOut();
        }
    }

}
