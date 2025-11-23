using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenScript : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        StartCoroutine(LoadMainScreen());
    }

    // Update is called once per frame
    void Update() { }

    IEnumerator LoadMainScreen()
    {
        yield return new WaitForSeconds(9f);

        SceneManager.LoadScene("MainMenu");
    }
}