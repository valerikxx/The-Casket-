using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class PC_Add_Btn : MonoBehaviour
{
    public GameObject Tab;
    public PC_Manager PC;
    public Text TabText;
    public string CorrectCode = "231206";
    public UnityEvent unityEvent;
    public void openTab(bool isActive)
    {
        Tab.SetActive(isActive);
    }
    public void CheckPass()
    {
        if (TabText.text != null)
        {
            if (TabText.text == CorrectCode)
            {
                Debug.Log("Correct PAss");
                unityEvent?.Invoke();
                openTab(false);
                PC.SetPcCanvas(false);
            }
            else
            {
                TabText.text = "Not found";
                Debug.LogError("UnCorrect PAss");
            }
                
        }
        else
            Debug.LogError("UnCorrect PAss");
    }
}
