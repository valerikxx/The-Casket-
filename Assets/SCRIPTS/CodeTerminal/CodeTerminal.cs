using UnityEngine;
using UnityEngine.UI; // или using TMPro;
using UnityEngine.Audio;
using UnityEngine.Events;

public class CodeTerminal : MonoBehaviour, IInteractable
{
    public Text codeDisplay; // UI-текст (можно заменить на TMP_Text, если используешь TextMeshPro)
    public string correctCode = "1234";
    public UnityEvent onCorrectCodeEntered;
    public UnityEvent onUnCorrectCodeEntered;
    public AudioSource audioSource;
    public AudioClip successSound;
    public AudioClip failSound;
    public bool needICheck = false;
    public GameObject UICodePanel;

    private string currentInput = "";

    void Update()
    {

        if (needICheck)
        {
            CheckInput();
            if (Input.GetKeyDown(KeyCode.Q))
            {
                needICheck = false;
            }
        }
        else
        {
            UICodePanel.SetActive(false);
            needICheck = false;
        }
    }
    public void Interact()
    {
        UICodePanel.SetActive(true);
        needICheck = true;
    }
    public void CheckInput()
    {
        // Проверка ввода цифр с клавиатуры
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key) && currentInput.Length < 4)
            {
                string digit = "";

                // С клавиатуры сверху
                if (key >= KeyCode.Alpha0 && key <= KeyCode.Alpha9)
                    digit = key.ToString().Replace("Alpha", "");

                // С numpad-а
                else if (key >= KeyCode.Keypad0 && key <= KeyCode.Keypad9)
                    digit = ((int)key - (int)KeyCode.Keypad0).ToString();

                if (digit != "")
                {
                    currentInput += digit;
                    codeDisplay.text = currentInput;

                    // Если набрано 4 цифры — проверяем
                    if (currentInput.Length == 4)
                    {
                        CheckCode();
                    }
                    break;
                }
            }
        }
    }

    void CheckCode()
    {
        if (currentInput == correctCode)
        {
            audioSource.PlayOneShot(successSound);
            Debug.Log("Код правильный!");
            AfterCorrect();
            needICheck = false;
            // Действие: открыть дверь и т.д.
        }
        else
        {
            audioSource.PlayOneShot(failSound);
            Debug.Log("Код неверный!");
            onUnCorrectCodeEntered?.Invoke();
        }

        // Сброс ввода
        currentInput = "";
        codeDisplay.text = "";
    }
    public void AfterCorrect()
    {
        onCorrectCodeEntered?.Invoke();
        Debug.Log("Чтото делаем");
    }
}
