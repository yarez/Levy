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
    
    public GameManager GManager;
    public JamesController james;
    public GameObject AD_Container;
    public AudioSource AD_Cntainer_Source;

    public Toggle ADButton;
    public Toggle G_Button;
    public Button CButton;
    public Button Q_Button;

    public GameObject AD_arrow;
    public GameObject GA_arrow;
    public GameObject C_arrow;
    public GameObject Q_arrow;
    bool AD_Enabled = true;
    bool started = false;

    public AudioClip AD_pause;
    public AudioClip AD_adOn;
    public AudioClip AD_gaOn;
    public AudioClip AD_gaOff;
    public AudioClip AD_con;
    public AudioClip AD_quit;
    public AudioClip AD_controlsLong;
    public AudioClip AD_ON;
    public AudioClip AD_OFF;

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Pause"))
        {
            pauseToggle();
        }


        //if(Input.anyKeyDown && controls.activeInHierarchy)
        //{
        //    controlToggle();
        //}

        if(paper.activeInHierarchy && Input.anyKey && (!GManager.paperPlaying || !AD_Enabled))
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

    public void AD_Toggle()
    {
        if (AD_Container.activeInHierarchy)
        {
            AD_Container.SetActive(false);
            AD_Cntainer_Source.clip = AD_OFF;
            AD_Cntainer_Source.Play();
        }else
        {
            AD_Container.SetActive(true);
            AD_Cntainer_Source.clip = AD_ON;
            AD_Cntainer_Source.Play();
        }

    }

    public void pauseToggle()
    {
        if (canvas.gameObject.activeInHierarchy == false)
        {
            AD_Cntainer_Source.Pause();
            Elev.SetActive(false);
            canvas.gameObject.SetActive(true);
            pauseMenu.SetActive(true);
            optionsMenu.SetActive(false);
            paper.SetActive(false);
            controls.SetActive(false);
            UI.SetActive(false);
            StartCoroutine(AD_pauseToggle());
            Time.timeScale = 0;
        }
        else
        {
            started = false;
            Elev.SetActive(true);
            canvas.gameObject.SetActive(false);
            UI.SetActive(true);
            Time.timeScale = 1;
        }
    }

    IEnumerator AD_pauseToggle()
    {
        AD_Cntainer_Source.clip = AD_pause;
        AD_Cntainer_Source.Play();
        while (Input.GetButtonDown("Open") && AD_Cntainer_Source.isPlaying)
        {
            yield return null;
        }
        yield return new WaitForEndOfFrame();
        ADButton.Select();
    }

    public void goToOptions()
    {
        pauseMenu.SetActive(false);
        CButton.Select();
        optionsMenu.SetActive(true);
    }

    public void backFromOptions()
    {
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
            AD_Cntainer_Source.clip = AD_ON;
            AD_Cntainer_Source.Play();
        }
    }

    public void controlToggle()
    {
        StartCoroutine(controlDelay());
    }

    IEnumerator controlDelay()
    {
        yield return new WaitForEndOfFrame();
        if (controls.activeInHierarchy)
        {
            controls.SetActive(false);
            AD_Cntainer_Source.Stop();
        }
        else
        {
            AD_Cntainer_Source.clip = AD_controlsLong;
            AD_Cntainer_Source.Play();
            controls.SetActive(true);
        }
    }

    IEnumerator paperOn()
    {
        yield return new WaitForSeconds(0.001f);
        
    }

    public void toggle_AD_Arrow()
    {
        if (AD_arrow.activeInHierarchy)
        {
            AD_arrow.SetActive(false);
        }
        else
        {
            AD_arrow.SetActive(true);
            if (started)
            {
                AD_Cntainer_Source.Stop();
            }
            if (!AD_Cntainer_Source.isPlaying && started)
            {
                AD_Cntainer_Source.clip = AD_adOn;
                AD_Cntainer_Source.Play();
            }else
            {
                started = true;
            }
        }
    }

    public void toggle_G_Arrow()
    {
        if (GA_arrow.activeInHierarchy)
        {
            GA_arrow.SetActive(false);
        }
        else
        {
            AD_Cntainer_Source.Stop();
            AD_Cntainer_Source.clip = AD_gaOn;
            AD_Cntainer_Source.Play();
            GA_arrow.SetActive(true);
        }
    }

    public void toggle_C_Arrow()
    {
        if (C_arrow.activeInHierarchy)
        {
            C_arrow.SetActive(false);
        }
        else
        {
            AD_Cntainer_Source.Stop();
            AD_Cntainer_Source.clip = AD_con;
            AD_Cntainer_Source.Play();
            C_arrow.SetActive(true);
        }
    }

    public void toggle_Q_Arrow()
    {
        if (Q_arrow.activeInHierarchy)
        {
            Q_arrow.SetActive(false);
        }
        else
        {
            AD_Cntainer_Source.Stop();
            AD_Cntainer_Source.clip = AD_quit;
            AD_Cntainer_Source.Play();
            Q_arrow.SetActive(true);
        }
    }
}
