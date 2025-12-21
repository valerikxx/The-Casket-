using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;
public class NoteReader : MonoBehaviour, IInteractable
{
    [Header("Метод, который вызовется")]
    public UnityEvent onPlayerInteract;
    public bool NeedIvent;
    [Header("Settings")]
    public CanvasGroup canvasGroup;
    public float fadeDuration = 0.5f;

    public GameObject notePanel;       // Панель с запиской (включает фон, текст и т.д.)
    public Text noteText;              // UI Text (или TextMeshProUGUI, если используешь TMP)

    [TextArea(5, 10)]
    public string TextRU;
    [TextArea(5, 10)]
    public string TextEng;

    public int languageID = 0;         // 0 - Русский, 1 - English

    private bool isVisible = false;
    private bool noteOpen = false;

    void Start()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        notePanel.SetActive(false);
    }

    public void Interact()
    {
        if (!noteOpen)
            OpenNote();
    }

    void Update()
    {
        if (noteOpen && Input.GetKeyDown(KeyCode.Q))
        {
            CloseNote();
        }
    }

    public void OpenNote()
    {
        if (NeedIvent)
        {
            onPlayerInteract?.Invoke();
        }
        noteOpen = true;
        notePanel.SetActive(true);
        StartCoroutine(FadeIn());

        switch (languageID)
        {
            case 0:
                noteText.text = TextRU;
                break;
            case 1:
                noteText.text = TextEng;
                break;
            default:
                noteText.text = TextEng;
                break;
        }

        Time.timeScale = 0f;
    }

    public void CloseNote()
    {
        noteOpen = false;
        StartCoroutine(FadeOut());

        Time.timeScale = 1f;
    }

    IEnumerator FadeIn()
    {
        isVisible = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        float timer = 0f;
        while (timer < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, timer / fadeDuration);
            timer += Time.unscaledDeltaTime; // Работает при Time.timeScale = 0
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }

    IEnumerator FadeOut()
    {
        isVisible = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;

        float timer = 0f;
        while (timer < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, timer / fadeDuration);
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f;
        notePanel.SetActive(false);
    }
}
