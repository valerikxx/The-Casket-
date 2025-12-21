using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeCutStarter : MonoBehaviour
{
    public string CutSceneName;
    
    private void Start()
    {
        CutsceneManager.Instance.StartCutscene(CutSceneName);
    }
}
