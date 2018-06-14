using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Char_Movement_Controller[] Chars;
    //public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    public int day = 1;                                  //Current day, expressed in game as "Day 1".
    private int scriptCount = 0;
    private bool dingActive = true;
    private Day_Operation Day = null;
    public GameObject NewsPaper;
    public RawImage Paper_Image;
    public GameObject PauseCanvas;
    public GameObject UI;
    private int movesLeft = 10;
    private int[] deathScript = {0, 3, 1, 6, 0};
    public GameObject[] movesUI;
    public Text dayText;
    private int satisfaction = 5;
    public RectTransform UI_Sat;
    public GameObject[] dayScreens;
    public AudioSource BG_Music;
    public ConversationEngine CEnginge;
    public Texture[] Daily_Papers;
    public GameObject Levy;
    public JamesController james;
    public DeathProps props;


    //Awake is always called before any Start functions
    void Awake()
    {
        //Call the InitGame function to initialize the first level 
        InitGame();
    }

    //Initializes the game for each level.
    void InitGame()
    {
        Debug.Log("Init Game");
        //StartCoroutine(DayEnd());
        DayEnd();
        PauseCanvas.SetActive(false);
        UI.SetActive(false);
        Day = (Day_Operation)this.GetComponent("Day_Operation");
        for (int i = 0; i<Chars.Length; i++)
        {
            if (Chars[i].getIsAlive())
            {
                Chars[i]._isoTransform.Position = Chars[i].HomeBase;
                Chars[i].ar_id = i;
            }else
            {
                Chars[i].gameObject.SetActive(false);
            }
        }
        StartCoroutine(DayStart());
    }



    //Update is called every frame.
    void Update()
    {
        
        if (!dingActive && !CEnginge.convActive && !james.speaking && !NewsPaper.activeInHierarchy)
        {
            dingActive = true;
            int choice = Script_or_Random();
            if (choice == 1)
            {
                Day.Random_Ding(Chars);
            }else if(choice == 0)
            {
                Day.Script(scriptCount, day, Chars);
                scriptCount++;
            }
        }
    }

    private int Script_or_Random()
    {
        int choice;
        movesLeft--;
        Debug.Log("Moves Left: " + movesLeft);
        if (movesLeft == 0)
        {
            if (day == 4)
            {
                SceneManager.LoadScene("ComingSoon", LoadSceneMode.Single);
            }
            else
            {
                newDay();
            }

        }
        else if (scriptCount == 3 || (day == 4 && scriptCount == 2))
        {
            movesUI[movesLeft].SetActive(false);
            movesUI[movesLeft - 1].SetActive(true);
            Debug.Log("Day script finished, random event");
            return 1;
        }
        else if (movesLeft > 3)
        {
            movesUI[movesLeft].SetActive(false);
            movesUI[movesLeft - 1].SetActive(true);
            Random.InitState((int)System.DateTime.Now.Ticks);
            choice = Random.Range(0, 2);
            Debug.Log("Day not finished, chose random: " + choice);
            return choice;
        }
        else
        {
            movesUI[movesLeft].SetActive(false);
            movesUI[movesLeft - 1].SetActive(true);
            Debug.Log("Day not finished, running out of time, scripted event");
            return 0;
        }
        return -1;
    }

    public void Char_Done()
    {
        StartCoroutine(WaitToSelect());
    }

    IEnumerator WaitToSelect()
    {
        yield return new WaitForSeconds(1f);
        dingActive = false;
    }

    IEnumerator DayStart()
    {
        
        for (int i = 0; i < Chars.Length; i++)
        {
            if (Chars[i].getIsAlive())
            {
                Chars[i].StopAllCoroutines();
                Chars[i]._isoTransform.Position = Chars[i].HomeBase;
                Chars[i].script_char = false;
                Chars[i].did_script = false;
            }
            else
            {
                Chars[i].gameObject.SetActive(false);
            }
        }
        while (NewsPaper.activeInHierarchy)
        {
            yield return null;
        }
        dayScreens[day].SetActive(true);
        yield return new WaitForSeconds(3);
        dayScreens[day].SetActive(false);
        Levy.SetActive(true);
        UI.SetActive(true);
        dingActive = false;
    }

    void DayEnd()
    {
        BG_Music.volume = 0.12f;
        Levy.SetActive(false);
        if (day > 1 && !Chars[deathScript[day - 1]].isAlive)
        {
            Paper_Image.texture = Daily_Papers[day + 4];
        }
        else
        {
            Paper_Image.texture = Daily_Papers[day];
        }
        NewsPaper.SetActive(true);
        props.setOutlines(day, Chars);
        
    }

    void newDay()
    {
        dingActive = true;
        day++;
        james.generalIndex = 0;
        if (Chars[deathScript[day - 1]].did_script && Chars[4].did_script)
            Chars[deathScript[day - 1]].kill();
        scriptCount = 0;

        PauseCanvas.SetActive(false);
        UI.SetActive(false);
        movesLeft = 10;
        movesUI[9].SetActive(true);
        movesUI[0].SetActive(false);

        DayEnd();
        StartCoroutine(DayStart());
    }

    public void addSat()
    {
        if (satisfaction < 10)
        {
            Debug.Log("AddSat");
            satisfaction++;
            Vector3 velocity = Vector3.zero;
            UI_Sat.localPosition += new Vector3(0, 46f, 0);
        }
    }

    public void subSat()
    {
        satisfaction--;
        if (satisfaction > 0)
        {
            Debug.Log("SubSat");
            Vector2 velocity = new Vector2(0,0);
            UI_Sat.localPosition -= new Vector3(0, 46f, 0);
        }else
        {
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }
    }

    public int Reaper(int day)
    {
        return deathScript[day];
    }
}
