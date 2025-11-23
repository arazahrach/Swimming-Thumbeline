using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject LevelPanel;

    void Start()
    {
        MenuPanel.SetActive(true);
        LevelPanel.SetActive(false); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (LevelPanel.activeSelf)
            {
                LevelPanel.SetActive(false);
                MenuPanel.SetActive(true);
            }
            else if (MenuPanel.activeSelf)
            {
                Application.Quit();
                Debug.Log("Quit!"); 
            }
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(DelayLoad(sceneName));
    }

    IEnumerator DelayLoad(string sceneName)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
    }

    public void PlayButton()
    {
        MenuPanel.SetActive(false);
        LevelPanel.SetActive(true);
    }
}
