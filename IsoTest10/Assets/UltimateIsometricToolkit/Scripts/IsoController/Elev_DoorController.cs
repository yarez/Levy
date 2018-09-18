using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.UltimateIsometricToolkit.Scripts.Core;
using UltimateIsometricToolkit.physics;
using UltimateIsometricToolkit.controller;
using UnityEngine.UI;

public class Elev_DoorController : MonoBehaviour {
    [AddComponentMenu("UIT/CharacterController/Door Controller")]
    bool isOpen = false;
    int floor;
    public GameObject[] floorArrows;
    public AudioSource doorSound;
    public AudioSource AD_Container;
    public AudioClip[] floorClips;
    public ConversationEngine CEngine;
    public JamesController james;
    public float debugInput = 0;
    public FloorDescription AD_Floor;
    public GameManager GManager;
    bool arrestMade = false;

    public AudioSource elevSound;
    public AudioSource elevSoundOnFloor;

    public SimpleIsoObjectController Levy;

    //Floor level constants used for door operation
    private const int floor1Lv = -98;
    private const int floor2Lv = -74;
    private const int floor3Lv = -49;
    private const int floor4Lv = -24;
    private const int floor5Lv = -1;
    private const float doorDelta = 0.9f;

    private float[] lvls = { -98.47604f, -74.49965f, -49.62309f, -24.53732f, -1.224195f };

    private IsoTransform _isoTransform;
    
    void Awake()
    {
        _isoTransform = this.GetOrAddComponent<IsoTransform>(); //avoids polling the IsoTransform component per frame
    }

    // Update is called once per frame
    void Update () {
        debugInput = Input.GetAxis("Vertical");

        int y = (int)_isoTransform.Position.y;
        bool canSnap = setFloor(y);
        //Only allow door operation while on a valid floor
        if (GManager.day != 5 && !CEngine.convActive && (canSnap) && !AD_Floor.playing ) {
            if (Input.GetButtonDown("Open") && !isOpen)
            {
                //StartCoroutine(doorToggle());
                isOpen = !isOpen;
                DoorControl(doorDelta);
                doorSound.Play();
            }
            else if (Input.GetButtonDown("Open") && isOpen)
            {
                //StartCoroutine(doorToggle());
                isOpen = !isOpen;
                DoorControl(-doorDelta);
                doorSound.Play();
            }
        }
        else if ((canSnap) && !AD_Floor.playing && GManager.day == 5)
        {
            if(Input.GetButtonDown("Open") && !isOpen && !james.onLine && !james.onDay5intro && !james.gameOver)
            {
                isOpen = !isOpen;
                DoorControl(doorDelta);
                doorSound.Play();
                james.playArrest();
                arrestMade = true;
            }
        }
        else if(isOpen == true && Mathf.Abs(Input.GetAxis("Vertical")) >0)
        {
            DoorControl(-doorDelta); //Close the door if open when moving away from a floor
            doorSound.Play();
            isOpen = false;
        }else if (james.onDay5Line && !james.gameOver)
        {
            james.clearDia();
        }
	}

    //DoorControl: Moves the door into the correct position and toggles the isOpen variable
    void DoorControl(float delta)
    {
        _isoTransform.Translate(new Vector3(delta, 0, 0));
        
    }

    IEnumerator doorToggle()
    {
        yield return new WaitForSeconds(0.01f);
        isOpen = !isOpen;
    }

    public bool getOpen()
    {
        return isOpen;
    }

    private bool setFloor(int y)
    {

        switch (y)
        {
            case floor1Lv: floor = 1; updateText();  elevSoundOnFloor.volume = 0.5f; return true;
            case floor2Lv: floor = 2; updateText();  elevSoundOnFloor.volume = 0.5f; return true;
            case floor3Lv: floor = 3; updateText();  elevSoundOnFloor.volume = 0.5f; return true;
            case floor4Lv: floor = 4; updateText();  elevSoundOnFloor.volume = 0.5f; return true;
            case floor5Lv: floor = 5; updateText();  elevSoundOnFloor.volume = 0.5f; return true;
            default: floor = 0;  elevSoundOnFloor.volume = 0; return false;
        };

        //for (int i = 0; i < 5; i++)
        //{
        //    if (Mathf.Abs(lvls[i] - y) < 4)
        //    {
        //        floor = i+1;
        //        updateText();
        //        return true;
        //    }
        //}
        //return false;
    }

    public int getFloor()
    {
        return floor;
    }

    public void updateText()
    {
        switch (floor)
        {
            case 1: floorArrows[getFloor()].SetActive(false);
                    floorArrows[getFloor()-1].SetActive(true);break;

            case 5: floorArrows[getFloor()-2].SetActive(false);
                    floorArrows[getFloor()-1].SetActive(true);break;
                    
            default:
                    floorArrows[getFloor()].SetActive(false);
                    floorArrows[getFloor() - 2].SetActive(false);
                    floorArrows[getFloor() - 1].SetActive(true);break;
        }
    }
}
