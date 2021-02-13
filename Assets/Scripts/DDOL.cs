using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOL : MonoBehaviour
{
    public static DDOL audiostuff;
    AudioSource audioSource;
    public bool isMuted;

    void Awake()
    {
        isMuted = false;
        audioSource = GetComponent<AudioSource>();
        audiostuff = this;
        DontDestroyOnLoad(gameObject);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void MuteAudio()
    {
        if (isMuted)
        {
            audioSource.volume = 0.05f;
        }
        else
        {
            audioSource.volume = 0;
        }
        isMuted = !isMuted;
    }
}
