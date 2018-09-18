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
    public RawImage Text_Image;
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
    public FloorDescription AD_Floor;
    public StatusUpdate AD_Status;
    public AudioSource AD_Container;
    public AudioClip[][] paperClips;
    public bool paperPlaying;
    public AudioClip[] dayScreenAudio;
    public Sprite JamesSprite;
    public SpriteRenderer Elev_Agent;

    public AudioClip AD_ChicagoTribune;
    public AudioClip AD_PressToSkip;

    //Paper AD arrays
    public AudioClip[] AD_Paper_Day00;
    public AudioClip[] AD_Paper_Day01;
    public AudioClip[] AD_Paper_Day02_A;
    public AudioClip[] AD_Paper_Day02_D;
    public AudioClip[] AD_Paper_Day03_A;
    public AudioClip[] AD_Paper_Day03_D;
    public AudioClip[] AD_Paper_Day04_A;
    public AudioClip[] AD_Paper_Day04_D;
    public AudioClip[] AD_Paper_Day05_A;
    public AudioClip[] AD_Paper_Day05_D;

    public Char_Movement_Controller[] day5Chars;



    //Awake is always called before any Start functions
    void Awake()
    {
        //Call the InitGame function to initialize the first level 
        InitGame();
    }

    //Initializes the game for each level.
    void InitGame()
    {
        initAudio();
        Debug.Log("Init Game");
        //StartCoroutine(DayEnd());
        DayEnd();
        PauseCanvas.SetActive(false);
        UI.SetActive(false);
        Day = (Day_Operation)this.GetComponent("Day_Operation");
        AD_Floor.setDescriptions(day, Chars);
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

    void initAudio()
    {
        paperClips = new AudioClip[10][];

        paperClips[0] = AD_Paper_Day00;
        paperClips[1] = AD_Paper_Day01;
        paperClips[2] = AD_Paper_Day02_A;
        paperClips[3] = AD_Paper_Day03_A;
        paperClips[4] = AD_Paper_Day04_A;
        paperClips[5] = AD_Paper_Day05_A;
        paperClips[6] = AD_Paper_Day02_D;
        paperClips[7] = AD_Paper_Day03_D;
        paperClips[8] = AD_Paper_Day04_D;
        paperClips[9] = AD_Paper_Day05_D;
    }


    //Update is called every frame.
    void Update()
    {
        
        if (!dingActive && !CEnginge.convActive && !james.speaking && !NewsPaper.activeInHierarchy && day < 5)
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

        if (!CEnginge.convActive && Input.GetButtonDown("Floor") && !AD_Container.isPlaying)
        {
            AD_Floor.playAD();
        }

        if (!CEnginge.convActive && Input.GetButtonDown("Status") && !AD_Container.isPlaying)
        {
            AD_Status.playAD(satisfaction, movesLeft);
        }


    }

    private int Script_or_Random()
    {
        int choice;
        movesLeft--;
        Debug.Log("Moves Left: " + movesLeft);
        if (movesLeft == 0 && !CEnginge.convActive)
        {
            if (day == 4)
            {
                day5();
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
        
        while (NewsPaper.activeInHierarchy)
        {
            yield return null;
        }
        dayScreens[day].SetActive(true);
        AD_Container.clip = dayScreenAudio[day-1];
        AD_Container.Play();
        yield return new WaitForSeconds(2);
        
        yield return new WaitForSeconds(0.5f);
        if (day < 5)
        {
            for (int i = 0; i < Chars.Length; i++)
            {
                if (Chars[i].getIsAlive())
                {
                    Chars[i].StopAllCoroutines();
                    Chars[i].script_char = false;
                    Chars[i].did_script = false;
                    Chars[i]._isoTransform.Position = Chars[i].HomeBase;
                    
                }
                else
                {
                    Chars[i].gameObject.SetActive(false);
                }
            }
        }
        
        AD_Container.clip = null;
        dayScreens[day].SetActive(false);
        Levy.SetActive(true);
        UI.SetActive(true);
        dingActive = false;
        if (day == 5)
        {
            james.runDay5();
            positionChars();
        }
    }

    void DayEnd()
    {
        Levy.SetActive(false);
        if (day > 1 && !Chars[deathScript[day - 1]].isAlive)
        {
            Paper_Image.texture = Daily_Papers[day + 4];
            StartCoroutine(paperAudio(day + 4, true));
        }
        else
        {
            Paper_Image.texture = Daily_Papers[day];
            StartCoroutine(paperAudio(day, false));
        }
        NewsPaper.SetActive(true);
        props.setOutlines(day, Chars);

    }

    IEnumerator paperAudio(int dayIndex, bool death)
    {
        int i = 0;
        paperPlaying = true;
        AD_Container.Stop();
        AD_Container.clip = AD_ChicagoTribune;
        AD_Container.Play();
        while (!Input.GetButtonDown("Open") && AD_Container.isPlaying)
        {
            yield return null;
        }
        
        while (i < 2)
        {
            AD_Container.Stop();
            AD_Container.clip = paperClips[dayIndex][i];
            AD_Container.Play();
            
            while (!Input.GetButtonDown("Open") && AD_Container.isPlaying)
            {
                yield return null;
            }
            yield return new WaitForEndOfFrame();
            i++;
        }
        AD_Container.Stop();
        AD_Container.clip = AD_PressToSkip;
        AD_Container.Play();
        while (!Input.GetButtonDown("Open") && AD_Container.isPlaying)
        {
            yield return null;
        }
        yield return new WaitForEndOfFrame();
        while (i < 5)
        {
            AD_Container.Stop();
            AD_Container.clip = paperClips[dayIndex][i];
            AD_Container.Play();
            paperPlaying = true;
            while (!Input.GetButtonDown("Open") && AD_Container.isPlaying)
            {
                yield return null;
            }
            yield return new WaitForEndOfFrame();
            i++;
        }
        AD_Container.Stop();
        AD_Container.clip = null;
        paperPlaying = false;
    }

    void newDay()
    {
        dingActive = true;
        day++;
        
        james.generalIndex = 0;
        if (Chars[deathScript[day - 1]].did_script && Chars[4].did_script)
        {
            Chars[deathScript[day - 1]].kill();
            if(day == 4)
            {
                Chars[5].kill();
            }
        }
        AD_Floor.setDescriptions(day, Chars);
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

    void day5()
    {
        day++;
        Debug.Log("Day 5");

        if (Chars[deathScript[day - 1]].did_script && Chars[4].did_script)
            Chars[deathScript[day - 1]].kill();
        AD_Floor.setDescriptions(day, Chars);
        PauseCanvas.SetActive(false);
        UI.SetActive(false);
        movesLeft = 10;
        movesUI[9].SetActive(true);
        movesUI[0].SetActive(false);


        DayEnd();
        StartCoroutine(DayStart());


    }

    void positionChars()
    {
        Debug.Log("Position Chars");
        Chars[3].StopAllCoroutines();
        Chars[3].script_char = false;
        Chars[3].did_script = false;
        Chars[3]._isoTransform.Position = new Vector3(5, -98.71528f, 4);
        Chars[3].manSetSprite(1);
        Chars[0].StopAllCoroutines();
        Chars[0].script_char = false;
        Chars[0].did_script = false;
        Chars[0]._isoTransform.Position = new Vector3(5, -74.46848f, 4);
        Chars[0].manSetSprite(1);
        Chars[1].StopAllCoroutines();
        Chars[1].script_char = false;
        Chars[1].did_script = false;
        Chars[1]._isoTransform.Position = new Vector3(5, -49.6328f, 4);
        Chars[1].manSetSprite(1);
        Chars[4].StopAllCoroutines();
        Chars[4].script_char = false;
        Chars[4].did_script = false;
        Chars[4]._isoTransform.Position = new Vector3(5, -24.40814f, 4);
        Chars[4].manSetSprite(1);
        Chars[6].StopAllCoroutines();
        Chars[6].script_char = false;
        Chars[6].did_script = false;
        Chars[6]._isoTransform.Position = new Vector3(5, -1.243204f,1);
        Chars[6].manSetSprite(1);

        Elev_Agent.sprite = JamesSprite;
        SpriteRenderer floor_james = (SpriteRenderer)james.GetComponent<SpriteRenderer>();
        floor_james.sprite = null;
    }
}
