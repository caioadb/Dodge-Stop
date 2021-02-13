using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    [SerializeField]
    HPValue hp = null;
    [SerializeField]
    GameObject gameUI = null;
    [SerializeField]
    GameObject tutorialUI = null;
    [SerializeField]
    GameObject gameoverUI = null;
    [SerializeField]
    GameObject player = null;
    [SerializeField]
    GameObject mousePos = null;
    [SerializeField]
    AudioSource audioSource = null;
    [SerializeField]
    AudioSource deathSource = null;

    private void Start()
    {
        audioSource.volume = DDOL.audiostuff.isMuted ? 0f : 0.5f;
        deathSource.volume = DDOL.audiostuff.isMuted ? 0f : 0.5f;
        Time.timeScale = 0;
        hp.OnVariableChange += VariableChangeHandler;
        player.SetActive(false);
        mousePos.SetActive(false);
        gameUI.SetActive(false);
        gameoverUI.SetActive(false);
        tutorialUI.SetActive(true);

    }

    private void VariableChangeHandler(int hpVal)
    {
        if (hpVal == 0) // if player is dead
        {
            //play death  sound
            deathSource.Play();
            Time.timeScale = 0f; // stop time
            Cursor.visible = true;
            //activate game over ui image/buttons        
            tutorialUI.SetActive(false);
            gameUI.SetActive(false);
            gameoverUI.SetActive(true);
        }
        else
        {
            //play hit sound
            audioSource.Play();
        }
    }

    public void TutorialOK()
    {
        player.SetActive(true);
        mousePos.SetActive(true);
        gameoverUI.SetActive(false);
        tutorialUI.SetActive(false);
        gameUI.SetActive(true);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        hp._RuntimeValue = 3;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

}
