using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamerStarter : MonoBehaviour
{
    public GameObject Screamer;
    ScreamerPathFollower screamerPathFollower;
    // Start is called before the first frame update
    void Start()
    {
        Screamer.SetActive(false);
        screamerPathFollower = Screamer.GetComponent<ScreamerPathFollower>();
    }
    public void StartFollow(int idPath)
    {
        screamerPathFollower.pathID = idPath;
         Screamer.SetActive(true);
    }
}
