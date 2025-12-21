using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Button3D : MonoBehaviour
{
    public int buttonId = 0;
    public SequenceManagerNoTMP manager;
    public Color idleColor = Color.gray;
    public Color hoverColor = Color.white;
    public Color pressedColor = Color.green;

    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend != null) rend.material.color = idleColor;
    }

    void OnMouseEnter()
    {
        if (rend != null) rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        if (rend != null) rend.material.color = idleColor;
    }

    void OnMouseDown()
    {
        if (manager == null) return;
        if (rend != null) rend.material.color = pressedColor;
        manager.On3DButtonPressed(buttonId);
    }
}
