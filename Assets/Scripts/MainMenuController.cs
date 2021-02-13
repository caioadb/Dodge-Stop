using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    GameObject creditsUI = null;
    [SerializeField]
    Button muteButton = null;    

    private void Start()
    {
        creditsUI.SetActive(false);
    }

    public void StartGameButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
    public void CreditsButton()
    {
        creditsUI.SetActive(true);
    }
    public void CloseCreditsButton()
    {
        creditsUI.SetActive(false);
    }

    public void MuteAudioButton()
    {
        DDOL.audiostuff.MuteAudio();
        if (DDOL.audiostuff.isMuted)
        {
            muteButton.image.color = Color.grey;
        }
        else
        {
            muteButton.image.color =  Color.white;
        }
    }

}
