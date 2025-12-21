using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    // ������� ��� �������� ��������� ���������: ID => ������ (true/false)
    public Dictionary<string, bool> _items = new Dictionary<string, bool>();
    [ContextMenu("WIPE ALL DATA")]
    private void WipeAllData()
    {
        ClearData();
    }
    private void Awake()
    {
        WipeAllData();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ������ �� ��������� ����� �������
            LoadData(); // ��������� ������ ��� ������
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ���������� ��� ������� ��������
    public void SetItemCollected(string itemID)
    {
        Debug.Log(itemID + " Collected");
        if (!_items.ContainsKey(itemID))
        {
            _items.Add(itemID, true); // ��������� ������� � �������� ��� ���������
        }
        else
        {
            _items[itemID] = true; // ��������� ������, ���� ��� ���� � �������
        }
        SaveData();
    }

    // ���������, ������ �� �������
    public bool IsItemCollected(string itemID)
    {
        return _items.TryGetValue(itemID, out bool collected) && collected;
    }

    // ���������� ������
    private void SaveData()
    {
        // ������������ ������� � ������ ��� ������������
        List<ItemData> dataList = new List<ItemData>();
        foreach (var item in _items)
        {
            dataList.Add(new ItemData { id = item.Key, collected = item.Value });
        }

        string json = JsonUtility.ToJson(new SerializableList(dataList));
        PlayerPrefs.SetString("Inventory", json);
        PlayerPrefs.Save();
    }

    // �������� ������
    private void LoadData()
    {
        if (PlayerPrefs.HasKey("Inventory"))
        {
            string json = PlayerPrefs.GetString("Inventory");
            SerializableList loadedData = JsonUtility.FromJson<SerializableList>(json);

            _items.Clear();
            foreach (ItemData item in loadedData.items)
            {
                _items.Add(item.id, item.collected);
            }
        }
    }
    private void ClearData()
    {
        _items.Clear();
        PlayerPrefs.DeleteKey("Inventory");
        PlayerPrefs.Save();
        Debug.Log("��������� ������");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            AlertManager.Alert("Clear All Data!");
            ClearData();
        }
    }
    // ����� ��� �������� ������ ������ ��������
    [System.Serializable]
    private class ItemData
    {
        public string id;
        public bool collected;
    }

    // �����-������ ��� ������������ ������
    [System.Serializable]
    private class SerializableList
    {
        public List<ItemData> items;

        public SerializableList(List<ItemData> data)
        {
            items = data;
        }
    }
}
