using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance;

    [Header("UI References")]
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private Image fadeImage;

    [Header("Settings")]
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private Color defaultFadeColor = Color.black;

    [Header("Audio (Optional)")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip fadeSound;

    public void LoadFade(string Name)
    {
        TransitionManager.Instance.LoadScene(Name);
    }
    private void Awake()
    {
        // Singleton (один на игру)
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }

        // На старте делаем прозрачный фейд
        fadeCanvasGroup.alpha = 0;
        fadeImage.color = defaultFadeColor;
    }

    /// <summary>
    /// Запустить fade in/out без смены сцены
    /// </summary>
    public void FadeOnly(float targetAlpha, Color? color = null)
    {
        StartCoroutine(FadeRoutine(targetAlpha, color ?? defaultFadeColor));
    }

    /// <summary>
    /// Загрузить сцену с фейдом
    /// </summary>
    public void LoadScene(string sceneName, Color? color = null)
    {
        StartCoroutine(LoadSceneRoutine(sceneName, color ?? defaultFadeColor));
    }

    private IEnumerator LoadSceneRoutine(string sceneName, Color color)
    {
        // Fade to black (или выбранный цвет)
        yield return StartCoroutine(FadeRoutine(1, color));

        // Воспроизводим звук (если есть)
        if (fadeSound && audioSource)
        {
            audioSource.PlayOneShot(fadeSound);
        }

        // Загружаем сцену
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Fade back
        yield return StartCoroutine(FadeRoutine(0, color));
    }

    private IEnumerator FadeRoutine(float targetAlpha, Color color)
    {
        fadeImage.color = color;

        float startAlpha = fadeCanvasGroup.alpha;
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            yield return null;
        }

        fadeCanvasGroup.alpha = targetAlpha;
    }
}
