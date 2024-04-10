using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public GameObject pauseUI;
    public GameObject opitionUI;
    // Start is called before the first frame update
    void Start()
    {
        pauseUI.SetActive(false);
        opitionUI.SetActive(false);
    }

    public void PauseButtonUI()
    {
        Time.timeScale = 0.0f;
        pauseUI.SetActive(true);
    }

    public void ResumeButtonUI()
    {
        Time.timeScale = 1.0f;
        pauseUI.SetActive(false);
    }

    public void OptionsUI()
    {
        pauseUI.SetActive(false);
        opitionUI.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
