using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PauseGame : MonoBehaviour {

    public Transform canvas;
    public GameObject UI;
    public GameObject optionsMenu;
    public GameObject pauseMenu;
    public GameObject paper;
    public GameObject controls;
    public GameObject Elev;
    public Button News;
    public Button CButton;
    public GameManager GManager;
    public JamesController james;

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Pause"))
        {
            pauseToggle();
        }

        if(paper.activeInHierarchy && Input.anyKey)
        {
            if (GManager.day == 1)
            {
                james.runIntro();
            }
            paper.SetActive(false);
            //if (!pauseMenu.activeInHierarchy)
            //{
            //    UI.SetActive(true);
            //    Elev.SetActive(true);
            //}  
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (controls.activeInHierarchy)
            {
                controls.SetActive(false);
            }
            else if(optionsMenu.activeInHierarchy)
            {
                optionsMenu.SetActive(false);
                pauseMenu.SetActive(true);
            }
            else
            {
                pauseToggle();
            }
        }
	}

    public void pauseToggle()
    {
        if (canvas.gameObject.activeInHierarchy == false)
        {
            Elev.SetActive(false);
            canvas.gameObject.SetActive(true);
            pauseMenu.SetActive(true);
            optionsMenu.SetActive(false);
            paper.SetActive(false);
            controls.SetActive(false);
            UI.SetActive(false);
            News.Select();
            Time.timeScale = 0;
        }
        else
        {
            Elev.SetActive(true);
            canvas.gameObject.SetActive(false);
            UI.SetActive(true);
            Time.timeScale = 1;
        }
    }

    public void goToOptions()
    {
        pauseMenu.SetActive(false);
        CButton.Select();
        optionsMenu.SetActive(true);
    }

    public void backFromOptions()
    {
        News.Select();
        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void showPaper()
    {
        Elev.SetActive(false);
        paper.SetActive(true);
        StartCoroutine(paperOn());
        UI.SetActive(false);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void audioToggle()
    {
        Debug.Log(AudioListener.volume);
        if (AudioListener.volume > 0)
        {
            AudioListener.volume = 0;
        }else
        {
            AudioListener.volume = 1;
        }
    }

    public void controlToggle()
    {
        if (controls.activeInHierarchy)
        {
            controls.SetActive(false);
        }else
        {
            controls.SetActive(true);
        }
    }


    IEnumerator paperOn()
    {
        yield return new WaitForSeconds(0.001f);
        
    }
}
