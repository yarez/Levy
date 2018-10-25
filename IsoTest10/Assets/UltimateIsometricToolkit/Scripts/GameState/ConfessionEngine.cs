using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UltimateIsometricToolkit.controller;
using Assets.UltimateIsometricToolkit.Scripts.Core;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ConfessionEngine : MonoBehaviour {
    public GameManager GManager;

    public RawImage Text_Box;
    public AudioSource AD_Container;

    public Texture arrest_img;
    public AudioClip arrest_aud;

    public Texture[] Leon;
    public Texture[] Jodee;
    public Texture[] Akira;
    public Texture[] WilliamBase;
    public Texture WilliamLeonDead;
    public Texture[] WilliamAkiraDead;
    public Texture[] WilliamZahraDead;
    public Texture[] Zahra;

    public AudioClip[] A_Leon;
    public AudioClip[] A_Jodee;
    public AudioClip[] A_Akira;
    public AudioClip[] A_WilliamBase;
    public AudioClip A_WilliamLeonDead;
    public AudioClip[] A_WilliamAkiraDead;
    public AudioClip[] A_WilliamZahraDead;
    public AudioClip[] A_Zahra;

    public IsoTransform james_floor;
    public SpriteRenderer james_floor_sprite;
    public SpriteRenderer james_elev;
    public SpriteRenderer william_floor;
    public SpriteRenderer william_elev;

    public Sprite williamSprite;

    public SimpleIsoObjectController Levy;

    private bool choseRight = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void selectConfession(int floor)
    {
        int charID;
        switch (floor)
        {
            case 1: charID = 3; break;
            case 2: charID = 0; break;
            case 3: charID = 1; break;
            case 4: charID = 4; break;
            case 5: charID = 6; break;
            default: charID = 2; break;
        }
        runConfession(charID);
    }

    private void runConfession(int charID)
    {
        Texture[] lines = null;
        AudioClip[] A_lines = null;
        switch (charID)
        {
            case 0: lines = Jodee; A_lines = A_Jodee; break;
            case 1: lines = Akira; A_lines = A_Akira; break;
            case 3: lines = Leon; A_lines = A_Leon; break;
            case 4: lines = chooseWillSet(); A_lines = A_chooseWillSet(); choseRight = true; break;
            case 6: lines = Zahra; A_lines = A_Zahra; break;
            default: lines = null; A_lines = null; break;
        }
        StartCoroutine(startConfession(lines, A_lines));
    }

    private Texture[] chooseWillSet()
    {
        Texture[] setBuilt = new Texture[14];
        int i = 0;
        while (i < 5)
        {        
            setBuilt[i] = WilliamBase[i];
            i++;
        }

        if (!GManager.Chars[3].isAlive)
        {
            setBuilt[i] = WilliamLeonDead;
            i++;
        }
        if (!GManager.Chars[1].isAlive)
        {
            setBuilt[i] = WilliamAkiraDead[0];
            i++;
            setBuilt[i] = WilliamAkiraDead[1];
            i++;
        }
        if (!GManager.Chars[6].isAlive)
        {
            setBuilt[i] = WilliamZahraDead[0];
            i++;
            setBuilt[i] = WilliamZahraDead[1];
            i++;
            setBuilt[i] = WilliamZahraDead[2];
            i++;
            setBuilt[i] = WilliamZahraDead[3];
            i++;
        }

        setBuilt[i] = WilliamBase[5];
        i++;
        setBuilt[i] = WilliamBase[6];

        return setBuilt;
    }

    private AudioClip[] A_chooseWillSet()
    {
        AudioClip[] setBuilt = new AudioClip[14];
        int i = 0;
        while (i < 5)
        {
            setBuilt[i] = A_WilliamBase[i];
            i++;
        }

        if (!GManager.Chars[3].isAlive)
        {
            setBuilt[i] = A_WilliamLeonDead;
            i++;
        }
        if (!GManager.Chars[1].isAlive)
        {
            setBuilt[i] = A_WilliamAkiraDead[0];
            i++;
            setBuilt[i] = A_WilliamAkiraDead[1];
            i++;
        }
        if (!GManager.Chars[6].isAlive)
        {
            setBuilt[i] = A_WilliamZahraDead[0];
            i++;
            setBuilt[i] = A_WilliamZahraDead[1];
            i++;
            setBuilt[i] = A_WilliamZahraDead[2];
            i++;
            setBuilt[i] = A_WilliamZahraDead[3];
            i++;
        }

        setBuilt[i] = A_WilliamBase[5];
        i++;
        setBuilt[i] = A_WilliamBase[6];

        return setBuilt;
    }

    IEnumerator startConfession(Texture[] lines, AudioClip[] A_lines)
    {
        //james_floor.Position = new Vector3(5, james_elev.Position.y, 1);
        //james_floor_sprite.sprite = james_elev.sprite;
        //james_elev.sprite = null;
        Text_Box.color = new Color(255, 255, 255, 255);
        Text_Box.texture = arrest_img;
        AD_Container.clip = arrest_aud;
        AD_Container.Play();
        Debug.Log("Start confession");
        yield return new WaitForEndOfFrame();
        while (!Input.GetButtonDown("Open") && AD_Container.isPlaying)
        {
            yield return null;
        }
        yield return new WaitForEndOfFrame();
        Debug.Log("Start confession 2");
        int i = 0;
        while (i < lines.Length && lines[i] != null)
        {
            yield return new WaitForEndOfFrame();
            Text_Box.color = new Color(255, 255, 255, 255);
            Text_Box.texture = lines[i];
            AD_Container.clip = A_lines[i];
            AD_Container.Play();

            while (!Input.GetButtonDown("Open") && AD_Container.isPlaying)
            {
                yield return null;
            }
            yield return new WaitForEndOfFrame();
            i++;
        }
        Text_Box.color = new Color(255, 255, 255, 0);
        Text_Box.texture = null;
        AD_Container.Stop();
        yield return new WaitForEndOfFrame();
        if (!choseRight)
        {
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }else
        {
            
            yield return new WaitForSeconds(0.1f);
            Levy.Credit_Sequence();
        }
    }
}
