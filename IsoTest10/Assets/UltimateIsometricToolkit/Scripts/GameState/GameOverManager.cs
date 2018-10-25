using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{

    public AudioSource AD_Container;
    public AudioSource BG_Music;
    public AudioClip AD_ChicagoTribune;
    public AudioClip AD_PressToSkip;
    public AudioClip[] paperClips;

    public GameObject paper;
    public GameObject GO_Screen;


    void Start()
    {
        StartCoroutine(paperAudio());
    }

    public void startGame()
    {
        BG_Music.Stop();
        SceneManager.LoadScene("DemoIntro", LoadSceneMode.Single);
    }

    IEnumerator playAD()
    {
        while (true)
        {
            BG_Music.volume = 0.1f;
            AD_Container.Play();
            while (AD_Container.isPlaying)
                yield return null;
            BG_Music.volume = 0.24f;
            yield return new WaitForSeconds(4);
        }
    }

    IEnumerator paperAudio()
    {
        int i = 0;
        AD_Container.Stop();
        AD_Container.clip = AD_ChicagoTribune;
        AD_Container.Play();
        while (!Input.GetButtonDown("Open") && AD_Container.isPlaying)
        {
            yield return null;
        }

        while (i < 3)
        {
            AD_Container.Stop();
            AD_Container.clip = paperClips[i];
            AD_Container.Play();

            while (!Input.GetButtonDown("Open") && AD_Container.isPlaying)
            {
                yield return null;
            }
            yield return new WaitForEndOfFrame();
            i++;
        }
        yield return new WaitForEndOfFrame();
        
        AD_Container.Stop();
        AD_Container.clip = null;
        paper.SetActive(false);
        GO_Screen.SetActive(true);
        BG_Music.volume = 1;
    }
}
