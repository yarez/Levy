using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDescription : MonoBehaviour {

    public AudioSource[] Floor_Describe_Sources;
    public AudioClip[] Floor_Clips_Day_00;
    public AudioClip Day_01_floor1;
    public AudioClip Day_01_floor3;
    public AudioClip Day_02d_floor3;
    public AudioClip Day_02d_floor4;
    public AudioClip Day_02d_floor5;
    public AudioClip Day_03dd_floor3;
    public AudioClip Day_03da_floor3;
    public AudioClip Day_03ad_floor2;
    public AudioClip Day_03ad_floor3;
    public AudioClip Day_04aa_floor3;
    public AudioClip Day_04ad_floor3;
    public AudioClip Day_04da_floor3;
    public AudioClip Day_04dd_floor3;
    public AudioClip Day_04d_floor4;
    public AudioClip Day_04a_floor4;
    public AudioClip Day_04d_floor5;
    public AudioClip Day_04a_floor5;

    public ConversationEngine CEngine;

    public Elev_DoorController Levy;
    public bool playing;
    public int floor = 0;
	
	// Update is called once per frame
	void Update () {
            if ((Input.GetButtonDown("Open") || Mathf.Abs(Input.GetAxis("Vertical")) > 0) && floor>0 && Floor_Describe_Sources[floor - 1].isPlaying)
            {
                Floor_Describe_Sources[floor - 1].Stop();
                floor = 0;
                StartCoroutine(stopPlaying());
            }
            else if (floor > 1 && !Floor_Describe_Sources[floor - 1].isPlaying)
            {
                StartCoroutine(stopPlaying());
            }
	}

    public void playAD()
    {
        floor = Levy.getFloor();
        if (floor > 0)
        {
            Floor_Describe_Sources[floor - 1].Play();
            playing = true;
        }else
        {
            playing = false;
        }
    }

    IEnumerator stopPlaying()
    {
        yield return new WaitForEndOfFrame();
        playing = false;
        CEngine.disable();

    }

    public void setDescriptions(int day, Char_Movement_Controller[] Chars)
    {
        switch (day)
        {
            case 0:
                setDay0(day, Chars); break;
            case 1:
                setDay1(day, Chars); break;
            case 2:
                setDay2(day, Chars); break;
            case 3:
                setDay3(day, Chars); break;
            case 4:
                setDay4(day, Chars); break;
        }
    }

    private void setDay0(int day, Char_Movement_Controller[] Chars)
    {
        for (int i = 0; i<5; i++)
        {
            Floor_Describe_Sources[i].clip = Floor_Clips_Day_00[i];
        }
    }

    private void setDay1(int day, Char_Movement_Controller[] Chars)
    {
        Floor_Describe_Sources[0].clip = Day_01_floor1;
        Floor_Describe_Sources[2].clip = Day_01_floor3;
    }

    private void setDay2(int day, Char_Movement_Controller[] Chars)
    {
        if (!Chars[3].isAlive)
        {
            Floor_Describe_Sources[2].clip = Day_02d_floor3;
            Floor_Describe_Sources[3].clip = Day_02d_floor4;
            Floor_Describe_Sources[4].clip = Day_02d_floor5;
        }
    }

    private void setDay3(int day, Char_Movement_Controller[] Chars)
    {
        if (!Chars[3].isAlive)
        {
            if (!Chars[1].isAlive)
            {
                Floor_Describe_Sources[1].clip = Day_03ad_floor2;
                Floor_Describe_Sources[2].clip = Day_03dd_floor3;
            }else
            {
                Floor_Describe_Sources[2].clip = Day_03da_floor3;
            }
        }else
        {
            if (!Chars[1].isAlive)
            {
                Floor_Describe_Sources[1].clip = Day_03ad_floor2;
                Floor_Describe_Sources[2].clip = Day_03ad_floor3;
            }
        }
    }

    private void setDay4(int day, Char_Movement_Controller[] Chars)
    {
        if (!Chars[6].isAlive)
        {
            if (!Chars[1].isAlive)
            {
                Floor_Describe_Sources[3].clip = Day_04d_floor4;
                Floor_Describe_Sources[4].clip = Day_04d_floor5;
                if (!Chars[3].isAlive)
                {
                    Floor_Describe_Sources[2].clip = Day_04dd_floor3;
                }else
                {
                    Floor_Describe_Sources[2].clip = Day_04ad_floor3;
                }

            }else
            {
                Floor_Describe_Sources[3].clip = Day_04a_floor4;
                Floor_Describe_Sources[4].clip = Day_04a_floor5;
                if (!Chars[3].isAlive)
                {
                    Floor_Describe_Sources[2].clip = Day_04da_floor3;
                }
                else
                {
                    Floor_Describe_Sources[2].clip = Day_04aa_floor3;
                }
            }
        }
    }
}
