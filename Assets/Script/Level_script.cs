using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level_script : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Pass()
    {
        int current_level = SceneManager.GetActiveScene().buildIndex;
        if(current_level >= PlayerPrefs.GetInt("levelunlocked"))
        {
            PlayerPrefs.SetInt("levelunlocked", current_level +1);
        }
        SceneManager.LoadScene(0);
    }
}
