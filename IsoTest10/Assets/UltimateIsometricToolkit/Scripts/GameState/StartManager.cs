using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartManager : MonoBehaviour {

	public void startGame()
    {

        SceneManager.LoadScene("DemoIntro", LoadSceneMode.Single);
    }
}
