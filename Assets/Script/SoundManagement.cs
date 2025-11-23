using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagement : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip ButtonTap;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void ButtonSound()
    {
        audioSource.PlayOneShot(ButtonTap);   
    }
}
