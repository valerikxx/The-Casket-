using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIalert : MonoBehaviour
{
     public static UIalert Instance { get; private set; }
     public Image CursoreImage;

    void Awake()
    {
        Instance = this;        
    }
    public void SetNoCursore()
    {
        CursoreImage.color = new Color (1, 1, 1, 0.1f);
    }
    public void SetOnCursore()
    {
        CursoreImage.color = new Color (1, 1, 1,1f);
    }
}
