using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
public class PC_Manager : MonoBehaviour
{
    public static PC_Manager Instance;
    public GameObject PC_Canvas;
    public GameObject TabPersonal;
    public GameObject TabObjext;
    public Image InfoImage;
    public Text InfoImageText;
    public Text InfoText;
    public FirstPersonController FPS;
    void Start()
    {
        Instance = this;
    }
    public void SetTab(int tabId)
    {
        if (tabId == 0)
        {
            TabPersonal.SetActive(true);
            TabObjext.SetActive(false);
        }
        else
        {
            TabPersonal.SetActive(false);
            TabObjext.SetActive(true);
        }
    }
    public void SetItem(PC_item item)
    {
        if (InfoText != null)
        {
            InfoText.text = item.infotexts;
        }

        if (InfoImage != null)
        {
            InfoImage.sprite = item.image;
        }

        if (InfoImageText != null)
        {
            InfoImageText.text = item.imageText;
        }

    }
    public void SetPcCanvas(bool toActive)
    {
        PC_Canvas.SetActive(toActive);
        if (toActive)
        {
            FPS.enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        else
        {
            Cursor.visible = false;
            FPS.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
            
    }
}
