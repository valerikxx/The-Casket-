using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMainMAnager : MonoBehaviour
{
    // Текущее асинхронное действие
    private AsyncOperation asyncOperation;
    public string sceneName;
    // Флаг, что загрузка завершена и готова к активации
    private bool isReadyToActivate = false;

    /// <summary>
    /// Метод 1: Начинает асинхронную загрузку сцены, но не активирует её при завершении.
    /// </summary>
    /// <param name="sceneName">Имя сцены для загрузки</param>
    public void StartLoad()
    {
        if (asyncOperation == null)
        {
            StartCoroutine(LoadSceneAsync());
        }
    }

    // Корутина загрузки
    private IEnumerator LoadSceneAsync()
    {
        asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        // Запретить автоматическую активацию сцены
        asyncOperation.allowSceneActivation = false;

        // Ждать пока сцена загрузится до 90% (progress == 0.9)
        while (asyncOperation.progress < 0.9f)
        {
            // Здесь можно использовать asyncOperation.progress для обновления UI
            yield return null;
        }

        // Загрузка завершена (без активации)
        isReadyToActivate = true;
    }

    /// <summary>
    /// Метод 2: Если сцена загружена, активирует её.
    /// </summary>
    public void ActivateIfReady()
    {
        if (asyncOperation != null && isReadyToActivate)
        {
            // Разрешить активацию сцены
            asyncOperation.allowSceneActivation = true;
            // Сбросим флаги на случай повторного использования
            isReadyToActivate = false;
            asyncOperation = null;
        }
    }
}
