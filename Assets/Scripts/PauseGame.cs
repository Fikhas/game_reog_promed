using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    private bool isCanPause;

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (isCanPause)
            {
                if (!pausePanel.activeInHierarchy)
                {
                    pausePanel.SetActive(true);
                    Time.timeScale = 0;
                }
                else
                {
                    pausePanel.SetActive(false);
                    Time.timeScale = 1;
                }
            }
        }
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void CanPause()
    {
        isCanPause = true;
    }

    public void CannotPause()
    {
        isCanPause = false;
    }
}
