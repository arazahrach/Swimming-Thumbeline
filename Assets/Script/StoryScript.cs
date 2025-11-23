using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryScript1 : MonoBehaviour
{
    public string nextScene = "L1Scene";

    void Start()
    {
        Invoke("GoNextScene", 38.5f);
    }

    void GoNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
