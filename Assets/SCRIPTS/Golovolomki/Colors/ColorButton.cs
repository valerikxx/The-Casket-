using UnityEngine;
using UnityEngine.UI; // или using TMPro;
using UnityEngine.Audio;
using UnityEngine.Events;

public class ColorButton : MonoBehaviour, IInteractable
{
    public string colorName;
    [HideInInspector]
    public ColorPuzzleController controller;

    public void Interact()
    {
        controller.OnButtonPressed(colorName);
    }
}
