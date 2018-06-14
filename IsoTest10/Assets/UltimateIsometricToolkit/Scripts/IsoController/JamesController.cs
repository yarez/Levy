using System.Collections;
using System.Collections.Generic;
using Assets.UltimateIsometricToolkit.Scripts.physics;
using Assets.UltimateIsometricToolkit.Scripts.Pathfinding;
using Assets.UltimateIsometricToolkit.Scripts.Core;
using Assets.UltimateIsometricToolkit.Scripts.Utils;
using UnityEngine.UI;
using UnityEngine;

public class JamesController : MonoBehaviour {

    public Texture[] introSpeech;
    private Texture lineImg;

    public AudioClip[] A_introSpeech;
    private AudioClip lineAud;
    public AudioSource AD_Container;

    public JamesSelector selector;
    public AstarAgent AstarAgent;
    public RawImage D_Text;
    public IsoTransform _isoTransform;
    public SpriteRenderer Char_Floor;
    public Sprite[] Char_Sprites;
    private SpriteRenderer spriteRender = null;
    private float y;
    private bool canAdvance = true;
    private bool onIntro = false;
    private bool onLine = false;
    public bool speaking = false;
    private int index;
    public int generalIndex;

    // Use this for initialization
    void Start () {
        spriteRender = (SpriteRenderer)AstarAgent.GetComponent("SpriteRenderer");
        this._isoTransform.Position = new Vector3(2, -98.9216f, 10);
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

        if (D_Text.texture != null && Input.GetButtonDown("Open") && index < 4 && canAdvance && onIntro)
        {
            StartCoroutine(SwapThroughIntro());
        }else if(onIntro && index >= 4){
            index = 0;
            AstarAgent.Speed = 4;
            onIntro = false;
            D_Text.texture = null;
            D_Text.color = new Color(255, 255, 255, 0);
            AD_Container.Stop();
            AD_Container.clip = null;
            StartCoroutine(moveRandom(y));
        }else if (D_Text.texture != null && Input.GetButtonDown("Open") && onLine)
        {
            onLine = false;
            D_Text.texture = null;
            D_Text.color = new Color(255, 255, 255, 0);
            AD_Container.Stop();
            AD_Container.clip = null;
        }
    }

    IEnumerator moveRandom(float y)
    {
        while (true)
        {
                Vector3 dest = new Vector3(Random.Range(1, 12), y, Random.Range(1, 12));
                setSprite(dest);
                AstarAgent.MoveTo(dest);
            yield return new WaitForSeconds(Random.Range(5, 20));
        }
    }

    private void setSprite(Vector3 dest)
    {
        Vector3 initPos = this._isoTransform.Position;
        Vector3 dir = dest - initPos;

        if (dir.z < 0)
        {
            if (dir.x < 0)
            {
                Char_Floor.sprite = Char_Sprites[0];
            }
            else
            {
                Char_Floor.sprite = Char_Sprites[1];
            }
        }
        else
        {
            if (dir.x < 0)
            {
                Char_Floor.sprite = Char_Sprites[2];
            }
            else
            {
                Char_Floor.sprite = Char_Sprites[3];
            }
        }
    }

    public void runIntro()
    {
        StopAllCoroutines();
        onIntro = true;
        AstarAgent.Speed = 8;
        StartCoroutine(delayIntro());
    }

    public void runLine(int day, bool isAlive)
    {
        AD_Container.Stop();
        lineImg = selector.select(day, isAlive, generalIndex);
        lineAud = selector.selectAudio(day, isAlive, generalIndex++);
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
        yield return new WaitForSeconds(3);
        Vector3 dest = new Vector3(5.375271f, -98.9216f, 1.944325f);
        setSprite(dest);
        AstarAgent.MoveTo(dest);
        yield return new WaitForSeconds(1);
        D_Text.color = new Color(255, 255, 255, 255);
        D_Text.texture = introSpeech[0];
        AD_Container.clip = A_introSpeech[0];
        AD_Container.Play();
    }
}
