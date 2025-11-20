using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Levelscript : MonoBehaviour
{
    int levelunlocked;
    public Button[] buttons;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelunlocked = PlayerPrefs.GetInt("levelunlocked",1);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < levelunlocked; i++)
        {
            buttons[i].interactable = true;
        }
    }

    public void LoadLevel(int levelindex)
    {
        SceneManager.LoadScene(levelindex); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
