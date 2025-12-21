using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PC_Interact : MonoBehaviour, IInteractable
{
    public PC_Manager pC_Manager;
    
    public void Interact()
    {
        pC_Manager.SetPcCanvas(true);
    }
}
