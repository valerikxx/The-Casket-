using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInteract : MonoBehaviour
{
    public Camera Camera;
    public float ViewDistance;
    public LayerMask layerMask;

    public AudioSource audioSource;
    void Start()
    {
        UiSearch();
    }
    private void Update()
    {
        
        UiSearch();
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hitInfo;
            if (RaycastFromCenter(out hitInfo))
            {
                HandleHit(hitInfo);
            }
        }
    }
    public void UiSearch(){
        RaycastHit hitInfo;
        if (RaycastFromCenter(out hitInfo))
        {
            if (hitInfo.collider.TryGetComponent(out IInteractable interactableObj))
            {
                UIalert.Instance.SetOnCursore();
            }
            else
            {
                UIalert.Instance.SetNoCursore();
            }
        }
        else{
            UIalert.Instance.SetNoCursore();
        }
    }
    // Метод, который просто испускает луч и возвращает hitInfo
    private bool RaycastFromCenter(out RaycastHit hitInfo)
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = Camera.ScreenPointToRay(screenCenter);
        return Physics.Raycast(ray, out hitInfo, ViewDistance, layerMask);
    }

    // Метод который решает что делать с объектом в который попал луч
    private void HandleHit(RaycastHit hitInfo)
    {
        if (hitInfo.collider.TryGetComponent(out IInteractable interactableObj))
        {
            InteractWith(interactableObj);
        }
        else
        {
            Debug.Log("Это не интерактивный объект.");
        }
    }

    // Метод взаимодействия с объектом
    private void InteractWith(IInteractable interactable)
    {
        audioSource.Play();
        interactable.Interact();
    }
}
