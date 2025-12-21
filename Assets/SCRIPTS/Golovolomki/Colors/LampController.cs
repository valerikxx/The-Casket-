using UnityEngine;

public class LampController : MonoBehaviour
{
    public Light lampLight;           // Присвоить вручную или найти автоматически
    public Color offColor = Color.gray;
    public Color currentColor;

    void Awake()
    {
        if (lampLight == null)
            lampLight = GetComponentInChildren<Light>();

        SetColor(offColor);
    }

    public void SetColor(Color color)
    {
        currentColor = color;
        if (lampLight != null)
            lampLight.color = color;
    }

    public void TurnOff()
    {
        SetColor(offColor);
    }
}
