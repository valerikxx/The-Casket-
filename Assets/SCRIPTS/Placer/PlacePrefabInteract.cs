using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePrefabInteract : MonoBehaviour, IInteractable
{
    public PrefabSpawner prefabSpawner;

    public void Interact(){
        Debug.LogError("PlacePrefabInteract: PREFAB CREATED!!!");
        prefabSpawner.Spawn();
    }
}
