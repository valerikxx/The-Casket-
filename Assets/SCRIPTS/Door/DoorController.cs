using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{
     [Header("Открытие двери")]
    public bool canOpen = false;                          // Разрешение на взаимодействие
    public bool isOpen = false;                           // Состояние двери
    public float openSpeed = 2f;                          // Скорость анимации
    public Vector3 openRotation = new Vector3(0, 90, 0);  // Поворот при открытии

    [Header("Звуки")]
    public AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip lockedSound;

    private Quaternion closedRotation;
    private Quaternion openedRotation;

    void Start()
    {
        closedRotation = transform.rotation;
        openedRotation = Quaternion.Euler(transform.eulerAngles + openRotation);
    }
    public void setCanOpen(bool canOpen)
    {
        this.canOpen = canOpen;
    }
    public void Interact()
    {
        if (!canOpen)
        {
            if (audioSource && lockedSound)
                audioSource.PlayOneShot(lockedSound);
            return;
        }

        StopAllCoroutines();

        if (isOpen)
        {
            isOpen = false;
            StartCoroutine(RotateDoor(closedRotation));
        }
        else
        {
            isOpen = true;
            if (audioSource && openSound)
                audioSource.PlayOneShot(openSound);
            StartCoroutine(RotateDoor(openedRotation));
        }
    }

    private IEnumerator RotateDoor(Quaternion targetRot)
    {
        while (Quaternion.Angle(transform.rotation, targetRot) > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * openSpeed);
            yield return null;
        }
        transform.rotation = targetRot;
    }
}
