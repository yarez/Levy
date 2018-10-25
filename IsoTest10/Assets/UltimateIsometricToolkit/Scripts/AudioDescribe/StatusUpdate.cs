using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusUpdate : MonoBehaviour {


    public bool playing;
    public GameManager Gmanager;
    public Elev_DoorController Levy;
    public AudioSource AD_Container;
    public AudioClip[] satClips;
    public AudioClip[] energyClips;
    public AudioClip[] floorClips;

    public AudioClip[] charNames;
    private int floor;

    private bool Jodee = false;
    private bool Akira = false;
    private bool Leon = false;
    private bool William = false;
    private bool Minnie = false;
    private bool Zahra = false;

    public bool[] charBools = new bool[] {false, false, false, false, false, false, false, false};


    // Update is called once per frame
    void Update () {
		
	}

    public void playAD(int sat, int energy)
    {
        floor = Levy.getFloor();
        findChars();
        StartCoroutine(play(sat, energy));
    }

    IEnumerator play(int sat, int energy)
    {
        while (AD_Container.isPlaying)
        {
            yield return null;
        }
        AD_Container.clip = satClips[sat-1];
        AD_Container.Play();
        while(!Input.GetButtonDown("Open") && AD_Container.isPlaying)
        {
            yield return null;
        }
        AD_Container.clip = energyClips[energy];
        AD_Container.Play();
        while (!Input.GetButtonDown("Open") && AD_Container.isPlaying)
        {
            yield return null;
        }
        if (floor > 0)
        {
            AD_Container.clip = floorClips[floor-1];
            AD_Container.Play();
            while (!Input.GetButtonDown("Open") && AD_Container.isPlaying)
            {
                yield return null;
            }
            for(int i = 0; i<7; i++)
            {
                if (charBools[i])
                {
                    AD_Container.clip = charNames[i];
                    AD_Container.Play();
                    while (!Input.GetButtonDown("Open") && AD_Container.isPlaying)
                    {
                        yield return null;
                    }
                }
                if (Gmanager.day > 0 && Gmanager.day < 5 && floor == 1)
                {
                    AD_Container.clip = charNames[7];
                    AD_Container.Play();
                }
            }
        }
        while (!Input.GetButtonDown("Open") && AD_Container.isPlaying)
        {
            yield return null;
        }
        AD_Container.clip = null;
    }

    void findChars()
    {
        for(int i = 0; i<7; i++)
        {
            if (Gmanager.Chars[i].isAlive && Gmanager.Chars[i].getFloor() == floor)
            {
                charBools[i] = true;
            }else
            {
                charBools[i] = false;
            }
        }
    }
}
