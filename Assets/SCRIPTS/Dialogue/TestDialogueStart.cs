using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialogueStart : MonoBehaviour
{
    public DialoguePlayer Dp;
    private bool isRussian;
    int landuageID;
    
    private void Start(){
        
        if(PlayerPrefs.HasKey("HorizontalSelectorLANGUAGE_h1184513884") == false)
        {
            Debug.LogError("WE DO NOT FIND LANGUAGE ID");
            landuageID = 1;
        }
        else{
            landuageID = PlayerPrefs.GetInt("HorizontalSelectorLANGUAGE_h1184513884");// 0-eng 1-ru
        }
        if (landuageID == 0) 
        {   
            isRussian = false;
        }
        else
        {
            isRussian = true;
        }

    }
    public void StartDialogue()
    {
        Dp.StartDialogue(isRussian);
    }
}
