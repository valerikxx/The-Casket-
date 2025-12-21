using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptEffector : MonoBehaviour
{
    [Header("Аудио")]
    public AudioSource audioSource;

    [Header("Партиклы")]
    public ParticleSystem[] particleSystems;

    [Header("Объекты")]
    public GameObject[] objectsToActivate;
    public GameObject[] objectsToDeactivate;

    // ===== Методы для использования в UnityEvents =====

    public void PlayAudio()
    {
        if (audioSource != null)
            audioSource.Play();
    }

    public void PlayAudioClip(AudioClip clip)
    {
        if (audioSource != null && clip != null)
            audioSource.PlayOneShot(clip);
    }

    public void PlayParticles()
    {
        foreach (var ps in particleSystems)
        {
            if (ps != null)
                ps.Play();
        }
    }

    public void ActivateObjects(GameObject gameObject)
    {
        gameObject.SetActive(true);
       /* foreach (var obj in objectsToActivate)
         {
             if (obj != null)
                 obj.SetActive(true);
         }*/
    }

    public void DeactivateObjects()
    {
        foreach (var obj in objectsToDeactivate)
        {
            if (obj != null)
                obj.SetActive(false);
        }
    }

    public void ToggleObject(GameObject target)
    {
        if (target != null)
            target.SetActive(!target.activeSelf);
    }

    public void DebugMessage(string message)
    {
        Debug.Log(message);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void DestroyObject(GameObject target)
    {
        if (target != null)
            Destroy(target);
    }
}
