using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject NewBackground;
    public GameObject Background;
    public GameObject MenuPanel;
    public GameObject LevelPanel;

    void Start()
    {
        if(PlayerPrefs.GetInt("OpenLevelPanel") == 1)
        {
            LevelPanel.SetActive(true);
            MenuPanel.SetActive(false);
            Background.SetActive(false);
            NewBackground.SetActive(true);
            PlayerPrefs.SetInt("OpenLevelPanel", 0); 
        }
        else
        {
            Background.SetActive(true);
            NewBackground.SetActive(false);
            MenuPanel.SetActive(true);
            LevelPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (LevelPanel.activeSelf)
            {
                Background.SetActive(false);
                NewBackground.SetActive(true);
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
        Background.SetActive(false);
        NewBackground.SetActive(true);
        MenuPanel.SetActive(false);
        LevelPanel.SetActive(true);
    }
}
