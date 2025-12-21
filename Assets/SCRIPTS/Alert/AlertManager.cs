using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertManager : MonoBehaviour
{
    public static AlertManager Instance;
    public GameObject alertPrefab; // Префаб уведомления
    public Transform alertContainer; // Контейнер для уведомлений
    public float alertDuration = 3f; // Время отображения

    private Queue<GameObject> activeAlerts = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void Alert(string message)
    {
        if (Instance != null)
        {
            Instance.ShowAlert(message);
        }
    }

    private void ShowAlert(string message)
    {
        GameObject newAlert = Instantiate(alertPrefab, alertContainer);
        newAlert.GetComponentInChildren<Text>().text = message;
        activeAlerts.Enqueue(newAlert);
        StartCoroutine(FadeAndDestroy(newAlert));
    }

    private IEnumerator FadeAndDestroy(GameObject alert)
    {
        CanvasGroup canvasGroup = alert.GetComponent<CanvasGroup>();
        yield return new WaitForSeconds(alertDuration);

        float fadeDuration = 1f;
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            canvasGroup.alpha = 1 - (t / fadeDuration);
            yield return null;
        }

        Destroy(alert);
        activeAlerts.Dequeue();
    }
}
