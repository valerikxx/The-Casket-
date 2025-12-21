using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class ColorPuzzleController : MonoBehaviour
{
    [Header("Настройки")]
    public List<string> availableColors = new List<string> { "Red", "Green", "Blue", "Yellow" };
    public int[] stageLengths = new int[] { 3, 5, 7 };

    [Header("Объекты")]
    public List<ColorButton> buttons;
    public List<LampController> lamps; // Должны быть в том же порядке, что и кнопки
    public UnityEvent onPuzzleCompleted;

    private List<string> currentSequence = new List<string>();
    private List<string> playerInput = new List<string>();
    private int currentStage = 0;
    private bool isShowingSequence = false;
    public AudioSource audioSource;
    public AudioClip Correct;
    public AudioClip UnCorrect;

    private Dictionary<string, Color> colorMap = new Dictionary<string, Color>();

    private void Start()
    {
        // Присваиваем цвета
        colorMap["Red"] = Color.red;
        colorMap["Green"] = Color.green;
        colorMap["Blue"] = Color.blue;
        colorMap["Yellow"] = Color.yellow;

        foreach (var btn in buttons)
            btn.controller = this;

        StartStage();
    }

    private void StartStage()
    {
        playerInput.Clear();
        currentSequence = GenerateRandomSequence(stageLengths[currentStage]);
        Debug.Log($"Этап {currentStage + 1}: {string.Join(", ", currentSequence)}");
        StartCoroutine(ShowSequence());
    }

    private List<string> GenerateRandomSequence(int length)
    {
        List<string> result = new List<string>();
        for (int i = 0; i < length; i++)
        {
            int rnd = Random.Range(0, availableColors.Count);
            result.Add(availableColors[rnd]);
        }
        return result;
    }

    IEnumerator ShowSequence()
    {
        isShowingSequence = true;

        foreach (var lamp in lamps)
            lamp.TurnOff();

        yield return new WaitForSeconds(0.5f);

        foreach (var color in currentSequence)
        {
            LampController lamp = GetLampByColor(color);
            if (lamp != null)
                lamp.SetColor(colorMap[color]);

            yield return new WaitForSeconds(0.6f);

            if (lamp != null)
                lamp.TurnOff();

            yield return new WaitForSeconds(0.2f);
        }

        isShowingSequence = false;
    }

    public void OnButtonPressed(string color)
    {
        if (isShowingSequence) return;

        playerInput.Add(color);
        int index = playerInput.Count - 1;

        if (color != currentSequence[index])
        {
            Debug.Log("Ошибка! Повторите этап.");
            audioSource.PlayOneShot(UnCorrect);
            StartCoroutine(ShowResult(Color.red, false));
            return;
        }

        if (playerInput.Count == currentSequence.Count)
        {
            Debug.Log("Этап пройден!");
            audioSource.PlayOneShot(Correct);
            currentStage++;

            if (currentStage >= stageLengths.Length)
            {
                Debug.Log("Головоломка решена полностью!");
                StartCoroutine(ShowResult(Color.green, true, true));
            }
            else
            {
                StartCoroutine(ShowResult(Color.green, true));
            }
        }
    }

    IEnumerator ShowResult(Color resultColor, bool success, bool completed = false)
    {
        isShowingSequence = true;

        foreach (var lamp in lamps)
            lamp.SetColor(resultColor);

        yield return new WaitForSeconds(1.5f);

        foreach (var lamp in lamps)
            lamp.TurnOff();

        isShowingSequence = false;

        if (completed)
        {
            onPuzzleCompleted?.Invoke();
        }
        else if (success)
        {
            StartStage();
        }
        else
        {
            playerInput.Clear();
            StartCoroutine(ShowSequence());
        }
    }

    private LampController GetLampByColor(string colorName)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i].colorName == colorName)
                return lamps[i];
        }
        return null;
    }
}
