using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveItem : MonoBehaviour
{
    public GameObject item;
    public void SetActive(bool isActive){
        item.SetActive(isActive);
    }
}
