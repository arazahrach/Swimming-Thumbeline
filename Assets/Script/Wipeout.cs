using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wipeout : MonoBehaviour
{
    public AnimationClip WipeOutClip;
    int indexScene;

    void Start()
    {
    }

    void Update()
    {
    }

    public void WipeOutLoadScene(int SceneNumber)
    {
        gameObject.SetActive(true);
        
        indexScene = SceneNumber;

        Invoke("WipeOutCheck", WipeOutClip.length);
    }

    void WipeOutCheck()
    {
        SceneManager.LoadScene(indexScene);
    }
}