using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartManager : MonoBehaviour {

    public AudioSource AD_Container;
    public AudioSource BG_Music;


    void Start()
    {
        StartCoroutine(playAD());
    }

    public void startGame()
    {
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
}
