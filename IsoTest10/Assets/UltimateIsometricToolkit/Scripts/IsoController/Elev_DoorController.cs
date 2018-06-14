using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.UltimateIsometricToolkit.Scripts.Core;
using UltimateIsometricToolkit.physics;
using UnityEngine.UI;

public class Elev_DoorController : MonoBehaviour {
    [AddComponentMenu("UIT/CharacterController/Door Controller")]
    bool isOpen = false;
    bool canInput = true;
    int floor;
    public GameObject[] floorArrows;
    public AudioSource doorSound;
    public ConversationEngine CEngine;
    public JamesController james;

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
        int y = (int)_isoTransform.Position.y;
        bool canSnap = setFloor(y);
        //Only allow door operation while on a valid floor
        if ((canSnap) && !CEngine.convActive && !james.speaking) {
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
        }else if(isOpen == true)
        {
            DoorControl(-doorDelta); //Close the door if open when moving away from a floor
            doorSound.Play();
            isOpen = false;
        }
	}

    //DoorControl: Moves the door into the correct position and toggles the isOpen variable
    void DoorControl(float delta)
    {
        canInput = false;
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
            case floor1Lv: floor = 1; updateText(); return true;
            case floor2Lv: floor = 2; updateText(); return true;
            case floor3Lv: floor = 3; updateText(); return true;
            case floor4Lv: floor = 4; updateText(); return true;
            case floor5Lv: floor = 5; updateText(); return true;
            default: floor = 0; return false;
        };

       /* for (int i = 0; i < 5; i++)
        {
            if (Mathf.Abs(lvls[i] - y) < 1.5)
            {
                floor = i+1;
                return true;
            }
        }
        return false;*/
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
                    floorArrows[getFloor()-1].SetActive(true); break;
            case 5: floorArrows[getFloor()-2].SetActive(false);
                    floorArrows[getFloor()-1].SetActive(true); break;
            default:
                    floorArrows[getFloor()].SetActive(false);
                    floorArrows[getFloor() - 2].SetActive(false);
                    floorArrows[getFloor() - 1].SetActive(true); break;
        }
    }
}
