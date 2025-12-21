using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioLowPassFilter lowPassFilter; // Фильтр для имитации радио
    public AudioDistortionFilter distortionFilter; // Ещё один эффект для радио (пример)

    public AudioClip[] russianDialogueClips;
    public AudioClip[] englishDialogueClips;
    public string[] russianSubtitles;
    public string[] englishSubtitles;

    public bool[] russianUseRadioEffect; // Новые массивы для указания эффекта
    public bool[] englishUseRadioEffect;

    public Text subtitleText;
    private int currentClipIndex = 0;
    private bool isPlaying = false;
    private bool isRussian = true;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Проверим и добавим фильтры если их нет
        if (lowPassFilter == null)
        {
            lowPassFilter = gameObject.AddComponent<AudioLowPassFilter>();
        }
        if (distortionFilter == null)
        {
            distortionFilter = gameObject.AddComponent<AudioDistortionFilter>();
        }

        // Отключим эффекты по умолчанию
        SetRadioEffect(false);
    }

    public void StartDialogue(bool russian)
    {
        if (!isPlaying)
        {
            isRussian = russian;
            currentClipIndex = 0;
            Debug.Log("Запуск диалога на " + (isRussian ? "русском" : "английском"));
            StartCoroutine(PlayDialogue());
        }
        else
        {
            Debug.LogWarning("Диалог уже воспроизводится!");
        }
    }

    private IEnumerator PlayDialogue()
    {
        isPlaying = true;
        AudioClip[] dialogueClips = isRussian ? russianDialogueClips : englishDialogueClips;
        string[] subtitles = isRussian ? russianSubtitles : englishSubtitles;
        bool[] useRadioEffect = isRussian ? russianUseRadioEffect : englishUseRadioEffect;

        if (dialogueClips.Length == 0 || subtitles.Length == 0)
        {
            Debug.LogWarning("Отсутствуют клипы или субтитры!");
            isPlaying = false;
            yield break;
        }

        while (currentClipIndex < dialogueClips.Length)
        {
            AudioClip clip = dialogueClips[currentClipIndex];
            string subtitle = (currentClipIndex < subtitles.Length) ? subtitles[currentClipIndex] : "";
            bool applyRadioEffect = (currentClipIndex < useRadioEffect.Length) ? useRadioEffect[currentClipIndex] : false;

            if (clip != null)
            {
                SetRadioEffect(applyRadioEffect);

                audioSource.clip = clip;
                audioSource.Play();
                if (subtitleText != null)
                {
                    subtitleText.text = subtitle;
                }
                Debug.Log("Играет: " + clip.name + " | Субтитры: " + subtitle + " | Радио эффект: " + applyRadioEffect);
                yield return new WaitForSeconds(clip.length);
            }
            else
            {
                Debug.LogWarning("Отсутствует AudioClip в списке!");
            }
            currentClipIndex++;
        }

        // После окончания диалога выключим радиоэффект
        SetRadioEffect(false);

        isPlaying = false;
        if (subtitleText != null)
        {
            subtitleText.text = "";
        }
        Debug.Log("Диалог завершён.");
    }

    private void SetRadioEffect(bool enable)
    {
        if (lowPassFilter != null)
        {
            lowPassFilter.enabled = enable;
            lowPassFilter.cutoffFrequency = enable ? 4000f : 22000f; // 3000Гц для радио, 22000Гц — стандартный звук
        }
        if (distortionFilter != null)
        {
            distortionFilter.enabled = enable;
            distortionFilter.distortionLevel = enable ? 0.4f : 0f;
        }
    }
}
