using System.Collections;
using System.Collections.Generic;
using Assets.UltimateIsometricToolkit.Scripts.physics;
using Assets.UltimateIsometricToolkit.Scripts.Pathfinding;
using Assets.UltimateIsometricToolkit.Scripts.Core;
using Assets.UltimateIsometricToolkit.Scripts.Utils;
using UnityEngine.UI;
using UnityEngine;

public class MinnieController : MonoBehaviour {

    public Texture[] introSpeech;
    private Texture lineImg;

    public AudioClip[] A_introSpeech;
    private AudioClip lineAud;
    public AudioSource AD_Container;

    public AstarAgent AstarAgent;
    public RawImage D_Text;
    public IsoTransform _isoTransform;
    private SpriteRenderer spriteRender = null;
    private float y;
    private bool canAdvance = true;
    private bool onIntro = false;
    private bool onLine = false;
    public bool speaking = false;
    private int index;
    public int generalIndex;
    public Char_Movement_Controller movementController;
    public ConversationEngine CEngine;

    // Use this for initialization
    void Start () {
        spriteRender = (SpriteRenderer)AstarAgent.GetComponent("SpriteRenderer");
        //this._isoTransform.Position = new Vector3(2, -98.9216f, 10);
        y = this._isoTransform.Position.y;
    }

    // Update is called once per frame
    void Update() {
        if(onIntro || onLine)
        {
            speaking = true;
        }else
        {
            speaking = false;
        }

        if (D_Text.texture != null && Input.GetButtonDown("Open") && index < 3 && canAdvance && onIntro)
        {
            StartCoroutine(SwapThroughIntro());
        }else if(onIntro && index >= 3){
            index = 0;
            AstarAgent.Speed = 4;
            
            D_Text.texture = null;
            D_Text.color = new Color(255, 255, 255, 0);
            StartCoroutine(AD_Reminder());
            movementController.startMoving();
        }
    }

    IEnumerator AD_Reminder()
    {
        AD_Container.clip = A_introSpeech[3];
        AD_Container.Play();
        
        while(AD_Container.isPlaying && !Input.GetButtonDown("Open"))
        {
            yield return null;
        }
        AD_Container.Stop();
        AD_Container.clip = null;
        StartCoroutine(disableConv());
        onIntro = false;
        onLine = false;
    }

    IEnumerator disableConv()
    {
        yield return new WaitForSeconds(0.001f);
        CEngine.disable();
    }

    public void runIntro()
    {
        StopAllCoroutines();
        Debug.Log("Minnie Intro");
        onIntro = true;
        AstarAgent.Speed = 8;
        StartCoroutine(delayIntro());
    }

    public void runLine(int day, bool isAlive)
    {
        AD_Container.Stop();
        D_Text.color = new Color(255, 255, 255, 255);
        D_Text.texture = lineImg;
        AD_Container.clip = lineAud;
        AD_Container.Play();
        onLine = true;
    }

    IEnumerator SwapThroughIntro()
    {
        canAdvance = false;
        AD_Container.Stop();
        D_Text.texture = introSpeech[++index];
        AD_Container.clip = A_introSpeech[index];
        AD_Container.Play();
        yield return new WaitForSeconds(0.01f);
        canAdvance = true;
    }
    IEnumerator delayIntro()
    {
        yield return new WaitForSeconds(0.5f);
        Vector3 dest = new Vector3(5.375271f, -98.9216f, 1.944325f);
        movementController.setSprite(dest);
        AstarAgent.MoveTo(dest);
        yield return new WaitForSeconds(1);
        movementController.StopAllCoroutines();
        D_Text.color = new Color(255, 255, 255, 255);
        D_Text.texture = introSpeech[0];
        AD_Container.clip = A_introSpeech[0];
        AD_Container.Play();
    }
}
