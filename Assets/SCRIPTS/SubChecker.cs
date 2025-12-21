using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("SUBTITLES" + "DarkUISwitch") == "false")
        {
            this.gameObject.SetActive(false);
        }
        else{
            this.gameObject.SetActive(true);
        }
    }

}
