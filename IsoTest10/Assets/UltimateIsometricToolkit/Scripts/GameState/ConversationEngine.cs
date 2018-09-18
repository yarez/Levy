using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ConversationEngine : MonoBehaviour {

    public ConvSetSelector ConvSelector;

    private int[,] convScript1 = { {1, 1, 1, 1 }, { 3, 5, 4, 6}, { 1, 3, 6, 10 }, { 1, 4, 6, 10 }, { 3, 4, 6, 10 } };
    private int[,] convScript2 = { { 1, 1, 1, 1 }, { 1, 0, 0, 1 }, { 5, 4, 0, 10 }, { 3, 0, 5, 10 }, { 5, 0, 1, 10 } };

    private int[,] convScript1Dead = { { 1, 1, 1, 1 }, { 3, 5, 4, 6 }, { 1, 4, 6, 10 }, { 3, 4, 6, 10 }, { 3, 4, 10, 10 } };
    private int[,] convScript2Dead = { { 1, 1, 1, 1 }, { 1, 0, 0, 1 }, { 5, 1, 0, 10 }, {0, 0, 5, 10 }, { 1, 0, 10, 10 } };
    public GameManager Gmanager;
    public RawImage ConvText;
    private Texture[] convSet;
    private AudioClip[] A_convSet;
    public AudioSource A_Container;
    public JamesController james;
    public MinnieController minnie;
    private int index = 0;
    public bool convActive = false;
    public bool canAdvance = true;
    public int LocIndex;
    public FloorDescription AD_Floor;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if(james.speaking || minnie.speaking || AD_Floor.playing)
        {
            convActive = true;
        }

		if (convSet != null && convActive && ConvText.texture != null && Input.GetButtonDown("Open") && index < 4 && canAdvance)
        {
            StartCoroutine(SwapThrough());    
        }else if (convSet != null && index >= convSet.Length && !james.speaking)
        {
            index = 0;
            convSet = null;
            A_Container.Stop();
            A_Container.clip = null;
            ConvText.color = new Color(255, 255, 255, 0);
            ConvText.texture = null;
            StartCoroutine(deactivateConv());
        }
	}

    public Vector3 conversationTree(int ar_id, int floor)
    {
        int day = Gmanager.day;
        int partner = findConvPartner(ar_id, day, floor);
        bool isOnFloor = checkFloor(partner, floor);
        bool isAlive = Gmanager.Chars[Gmanager.Reaper(day - 1)].isAlive;
        ConvText.texture = null;

        if (!isOnFloor && floor == 1)
        {
            james.runLine(Gmanager.day, isAlive);
        }
        if (isOnFloor && isAlive)
        {

            Gmanager.Chars[partner].stopMoving();
            runConversation(day, ar_id, partner);
            return Gmanager.Chars[partner]._isoTransform.Position;
            
        }

        return new Vector3(-1, -1, -1);

    }

    private int findConvPartner(int ar_id, int day, int floor)
    {
        for (int i = 0; i < 4; i++)
        {
            if (Gmanager.Chars[Gmanager.Reaper(day-1)].isAlive)
            {
                if (ar_id == convScript1[day, i])
                {
                    if (checkFloor(convScript2[day, i], floor))
                    {
                        convScript1[day, i] = 10;
                        return convScript2[day, i];
                    }else
                    {
                        return findConvPartner2(ar_id, day, floor, i + 1);
                    }

                }
                else if (ar_id == convScript2[day, i])
                {
                    if (checkFloor(convScript1[day, i], floor))
                    {
                        convScript2[day, i] = 10;
                        return convScript1[day, i];
                    }else
                    {
                        return findConvPartner2(ar_id, day, floor, i + 1);
                    }
                }
            }else
            {
                if (ar_id == convScript1Dead[day, i])
                {
                    if (checkFloor(convScript2Dead[day, i], floor))
                    {
                        convScript1Dead[day, i] = 10;
                        return convScript2Dead[day, i];
                    }else
                    {
                        return findConvPartner2(ar_id, day, floor, i + 1);
                    }
                }
                else if (ar_id == convScript2Dead[day, i])
                {
                    if (checkFloor(convScript1Dead[day, i], floor))
                    {
                        convScript2Dead[day, i] = 10;
                        return convScript1Dead[day, i];
                    }else
                    {
                        return findConvPartner2(ar_id, day, floor, i + 1);
                    }
                }
            }
        }
        return 10;
    }

    private int findConvPartner2(int ar_id, int day, int floor, int ind)
    {
        for (int i = 0; i < 4; i++)
        {
            if (Gmanager.Chars[Gmanager.Reaper(day - 1)].isAlive)
            {
                if (ar_id == convScript1[day, i])
                {
                    if (checkFloor(convScript2[day, i], floor))
                    {
                        convScript1[day, i] = 10;
                        return convScript2[day, i];
                    }

                }
                else if (ar_id == convScript2[day, i])
                {
                    if (checkFloor(convScript1[day, i], floor))
                    {
                        convScript2[day, i] = 10;
                        return convScript1[day, i];
                    }
                }
            }
            else
            {
                if (ar_id == convScript1Dead[day, i])
                {
                    if (checkFloor(convScript2Dead[day, i], floor))
                    {
                        convScript1Dead[day, i] = 10;
                        return convScript2Dead[day, i];
                    }
                }
                else if (ar_id == convScript2Dead[day, i])
                {
                    if (checkFloor(convScript1Dead[day, i], floor))
                    {
                        convScript2Dead[day, i] = 10;
                        return convScript1Dead[day, i];
                    }
                }
            }
        }
        return 10;
    }

    private bool checkFloor(int target, int floor)
    {
        if (target < 10)
        {
            Char_Movement_Controller targetChar = Gmanager.Chars[target];
            if (targetChar.getFloor() == floor && targetChar.isAlive)
            {
                return true;
            }else
            {
                return false;
            }
        }
        return false;
    }

    public void runConversation(int day, int char1, int char2)
    {
        convSet = ConvSelector.Select(day, char1, char2);
        A_convSet = ConvSelector.SelectAudio(day, char1, char2);
        if(convSet != null) {
            ConvText.texture = (Texture)convSet[0];
            A_Container.clip = A_convSet[0];
            A_Container.Play();
            ConvText.color = new Color(255, 255, 255, 255);
            Debug.Log("Hi COnv");
            convActive = true;
        }else
        {
            Debug.LogError("Coversation Error, null conversation");
        }
    }

    IEnumerator SwapThrough()
    {
        A_Container.Stop();
        canAdvance = false;
        ConvText.texture = convSet[++index];
        A_Container.clip = A_convSet[index];
        A_Container.Play();
        yield return new WaitForSeconds(0.01f);
        canAdvance = true;
    }

    IEnumerator deactivateConv()
    {
        yield return new WaitForSeconds(0.5f);
        convActive = false;
        canAdvance = true;
    }

    public void disable()
    {
        convActive = false;
        Debug.Log("Disable");
    }

    public void clearText()
    {
        ConvText.color = new Color(255, 255, 255, 0);
        ConvText.texture = null;
    }
}
