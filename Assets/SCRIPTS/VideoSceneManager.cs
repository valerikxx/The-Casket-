using UnityEngine;
using UnityEngine.SceneManagement;
public class VideoSceneManager : MonoBehaviour
{
    public float nextScene;
    public int IdScene;
    void Start()
    {
        ScreenFader.Instance.FadeFromBlack();
        Invoke("toBlack",nextScene-3);
        Invoke("LoadScene",nextScene);
    }
    public void LoadScene(){
        SceneManager.LoadScene(IdScene);
    }
    void toBlack(){
        ScreenFader.Instance.FadeToBlack(); 
    }
    void fromBlack(){
        ScreenFader.Instance.FadeToBlack(); 
    }
    
}
