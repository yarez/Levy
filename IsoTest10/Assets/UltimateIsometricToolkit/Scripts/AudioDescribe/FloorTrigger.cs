using System.Collections;
using System.Collections.Generic;
using Assets.UltimateIsometricToolkit.Scripts.Core;
using Assets.UltimateIsometricToolkit.Scripts.Utils;
using UnityEngine;

public class FloorTrigger : MonoBehaviour {

    public IsoTransform Levy;
    public int floor;
    public bool active = false;
    public AudioSource AD_Container;
    public AudioClip[] floorClips;
    public bool upTrigger;
    private IsoTransform tform;
    private bool triggerDistance;

    // Use this for initialization
    void Start () {
        tform = (IsoTransform)this.GetComponent("IsoTransform");
	}
	
	// Update is called once per frame
	void Update () {
        if(Mathf.Abs(Levy.Position.y - tform.Position.y) < 1)
        {
            triggerDistance = true;
        }else
        {
            triggerDistance = false;
        }

        if (upTrigger)
        {
            if (!active && triggerDistance && Input.GetAxis("Vertical") > 0)
            {
                AD_Container.clip = floorClips[floor - 1];
                AD_Container.Play();
            }
        }else
        {
            if (!active && triggerDistance && Input.GetAxis("Vertical") < 0)
            {
                AD_Container.clip = floorClips[floor - 1];
                AD_Container.Play();
            }
        }
	}
}
