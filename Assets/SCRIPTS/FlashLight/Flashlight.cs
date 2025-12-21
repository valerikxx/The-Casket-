using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Light flashlight; // Ссылка на источник света
    public AudioSource clickSound; // Звук включения/выключения
    public float batteryLife = 100f; // Время работы фонарика (в секундах)
    public float batteryDrainRate = 1f; // Скорость разряда (единиц в секунду)
    public bool hasBattery = true; // Флаг использования батарейки

    private bool isOn = false;
    private float currentBattery;

    void Start()
    {
        if (flashlight == null)
            flashlight = GetComponentInChildren<Light>();

        if (flashlight != null)
            flashlight.enabled = false;

        currentBattery = batteryLife;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFlashlight();
        }

        if (isOn && hasBattery)
        {
            DrainBattery();
        }
    }

    void ToggleFlashlight()
    {
        if (hasBattery && currentBattery <= 0)
        {
            flashlight.enabled = false;
            isOn = false;
            return;
        }

        isOn = !isOn;
        flashlight.enabled = isOn;
        if (clickSound != null)
            clickSound.Play();
    }

    void DrainBattery()
    {
        if (currentBattery > 0)
        {
            currentBattery -= batteryDrainRate * Time.deltaTime;
        }
        else
        {
            flashlight.enabled = false;
            isOn = false;
        }
    }

    public void RechargeBattery(float amount)
    {
        currentBattery = Mathf.Clamp(currentBattery + amount, 0, batteryLife);
    }
}
