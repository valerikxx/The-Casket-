using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI; // если используешь UI Text

[System.Serializable]
public class IntEvent : UnityEvent<int> {}

public class SequenceManagerNoTMP : MonoBehaviour
{
    [Header("Display (без TextMeshPro)")]
    public Text monitorText;             // optional: World Space Canvas Text
    public Renderer monitorRenderer;     // optional: менять цвет/материал монитора

    [Header("Buttons")]
    public List<GameObject> buttons3D;   // необязательно

    [Header("Settings")]
    public int stepsCount = 4;
    public int minValue = 0;
    public int maxValueInclusive = 3;
    public float showDuration = 0.9f;
    public float gapBetween = 0.25f;

    [Header("Auto behaviour")]
    [Tooltip("Если нет назначенных (persistent) слушателей — автоматически продолжать")]
    public bool autoProceedIfNoListener = true;
    [Tooltip("Задержка перед автоматическим продолжением (если нет слушателей)")]
    public float autoProceedDelay = 0.25f;

    [Header("Events")]
    public IntEvent OnCorrectPressed;    // сюда привязываешь свой внешний метод

    // runtime
    private List<int> sequence = new List<int>();
    private int currentStep = 0;
    private int currentShown = -1;
    private bool waitingForPlayer = false;
    private bool waitingForExternalAction = false;
    private bool inputEnabled = false;
    private bool autoProceedPending = false;

    void Start()
    {
        StartSequence();
    }

    public void StartSequence()
    {
        StopAllCoroutines();
        sequence.Clear();
        for (int i = 0; i < stepsCount; i++)
            sequence.Add(Random.Range(minValue, maxValueInclusive + 1));

        Debug.Log("Sequence generated: " + string.Join(",", sequence));
        currentStep = 0;
        StartCoroutine(RunSequence());
    }

    IEnumerator RunSequence()
    {
        while (currentStep < sequence.Count)
        {
            // показать цифру
            currentShown = sequence[currentStep];
            ShowOnMonitor(currentShown);
            yield return new WaitForSeconds(showDuration);
            ClearMonitor();
            yield return new WaitForSeconds(gapBetween);

            // ждать нажатия игрока
            inputEnabled = true;
            waitingForPlayer = true;
            waitingForExternalAction = false;
            autoProceedPending = false;

            while (waitingForPlayer)
                yield return null;

            // Если ожидается внешнее действие — ждать его завершения.
            if (waitingForExternalAction)
            {
                while (waitingForExternalAction)
                    yield return null;
            }
            else if (autoProceedPending)
            {
                // если включён autoProceed, ждём пока истечёт задержка
                while (autoProceedPending)
                    yield return null;
            }

            // небольшой промежуток перед следующим шагом
            currentStep++;
            yield return new WaitForSeconds(0.15f);
        }

        Debug.Log("All steps completed.");
        // Можно здесь вызвать событие завершения, перезапустить и т.д.
    }

    void ShowOnMonitor(int value)
    {
        if (monitorText != null) monitorText.text = value.ToString();
        if (monitorRenderer != null)
        {
            if (monitorRenderer.material != null)
                monitorRenderer.material.color = Color.cyan;
        }
    }

    void ClearMonitor()
    {
        if (monitorText != null) monitorText.text = "";
        if (monitorRenderer != null)
        {
            if (monitorRenderer.material != null)
                monitorRenderer.material.color = Color.black;
        }
    }

    // Вызывают 3D-кнопки
    public void On3DButtonPressed(int buttonId)
    {
        if (!inputEnabled) return;

        Debug.Log($"Player pressed {buttonId}, expected {currentShown}");
        inputEnabled = false;
        waitingForPlayer = false;

        // Проверяем: есть ли персистентные слушатели, назначенные в инспекторе?
        bool hasPersistentListeners = (OnCorrectPressed != null && OnCorrectPressed.GetPersistentEventCount() > 0);

        // Вызов любого назначенного события (если есть)
        OnCorrectPressed?.Invoke(buttonId);

        if (hasPersistentListeners)
        {
            // Ожидаем внешнюю работу — зовущая логика должна вызвать NotifyActionComplete()
            waitingForExternalAction = true;
            Debug.Log("Waiting for external action to complete (NotifyActionComplete required).");
        }
        else
        {
            // Если слушателей нет — либо авто-продолжение (с задержкой), либо сразу продолжить
            if (autoProceedIfNoListener)
            {
                autoProceedPending = true;
                StartCoroutine(AutoProceedCoroutine(autoProceedDelay));
                Debug.Log("No listener assigned — auto-proceed in " + autoProceedDelay + "s");
            }
            else
            {
                // ничего — просто продолжим немедленно (следующий цикл сделает шаг)
                Debug.Log("No listener and autoProceedIfNoListener == false — proceeding immediately.");
            }
        }
    }

    IEnumerator AutoProceedCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        autoProceedPending = false;
    }

    // Метод, который внешний код должен вызвать, когда его действие закончено:
    // FindObjectOfType<SequenceManagerNoTMP>().NotifyActionComplete();
    public void NotifyActionComplete()
    {
        Debug.Log("External action completed. Resuming sequence.");
        waitingForExternalAction = false;
    }

    // Вспомогательные методы для удобной отладки (можно привязать в инспекторе)
    public void DebugPressButton0() { On3DButtonPressed(0); }
    public void DebugPressButton1() { On3DButtonPressed(1); }
    public void DebugPressButton2() { On3DButtonPressed(2); }
    public void DebugPressButton3() { On3DButtonPressed(3); }
}
