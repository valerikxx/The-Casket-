using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader Instance;  // Singleton

    public Image blackScreen;            // Image на весь экран
    public float fadeDuration = 1f;      // Скорость затухания

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

    public void FadeToBlack()
    {
        StartCoroutine(Fade(0, 1));
    }

    public void FadeFromBlack()
    {
        StartCoroutine(Fade(1, 0));
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float time = 0f;
        Color color = blackScreen.color;

        while (time < fadeDuration)
        {
            color.a = Mathf.Lerp(startAlpha, endAlpha, time / fadeDuration);
            blackScreen.color = color;
            time += Time.deltaTime;
            yield return null;
        }

        color.a = endAlpha;
        blackScreen.color = color;
    }
}
