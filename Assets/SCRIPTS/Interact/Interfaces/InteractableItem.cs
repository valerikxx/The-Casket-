using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour, IInteractable
{
    public string itemID; // ���������� ID �������� (������� � ����������)

    private void Start()
    {
        // ���� ������� ��� ������, �������� ���
        if (InventoryManager.Instance.IsItemCollected(itemID))
        {
            gameObject.SetActive(false);
        }
    }
    public void Interact()
    {
        AlertManager.Alert("Collected: " + itemID);
        InventoryManager.Instance.SetItemCollected(itemID);
        gameObject.SetActive(true); // �������� ��� ���������� �������
        UIalert.Instance.SetOnCursore();
        if (this.gameObject.CompareTag("Placer"))
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        else
            Destroy(this.gameObject);
    }
}
