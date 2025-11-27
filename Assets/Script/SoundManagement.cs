using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagement : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip ButtonTap;
    public GameObject Menang;
    public GameObject Kalah;
    public AudioClip Keran;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void BunyiKeran()
    {
        audioSource.PlayOneShot(Keran);
    }
    void Update()
    {
        if (Menang.activeInHierarchy || Kalah.activeInHierarchy)
        {
            audioSource.Stop();
        }
    }
    public void ButtonSound()
    {
        audioSource.PlayOneShot(ButtonTap);   
    }
}
