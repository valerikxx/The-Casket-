using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public string LoadSceneName;
    public Slider slider;
    public float fakeLoadSpeed = 0.3f; // скорость фейковой загрузки

    private AsyncOperation asyncLoad;
    private float targetProgress = 0;

    void Start()
    {
        Invoke("LoadScene", 1f);
    }

    public void LoadScene()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        asyncLoad = SceneManager.LoadSceneAsync(LoadSceneName);
        asyncLoad.allowSceneActivation = false;

        // Пока сцена реально грузится — крутим наш прогресс
        while (!asyncLoad.isDone)
        {
            // Unity максимум грузит до 0.9f, ждёт allowSceneActivation
            if (asyncLoad.progress < 0.9f)
                targetProgress = asyncLoad.progress;
            else
                targetProgress = 1f; // Когда готово - доводим до конца

            // Плавно увеличиваем значение слайдера
            slider.value = Mathf.MoveTowards(slider.value, targetProgress, fakeLoadSpeed * Time.deltaTime);

            yield return null;

            // Когда слайдер дошёл до конца - активируем сцену
            if (slider.value >= 0.99f && targetProgress == 1f)
            {
                asyncLoad.allowSceneActivation = true;
            }
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
