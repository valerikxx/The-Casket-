using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_item : MonoBehaviour
{
    public string imageText;
    [TextArea(5, 10)]
    public string infotexts;
    public Sprite image;
    public void OnClick()
    {
        PC_Manager.Instance.SetItem(this);
    }
}
