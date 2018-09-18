using UnityEngine;
using Assets.UltimateIsometricToolkit.Scripts.Core;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UltimateIsometricToolkit.physics;

namespace UltimateIsometricToolkit.controller { 
	[AddComponentMenu("UIT/CharacterController/Simple Controller")]
	public class SimpleIsoObjectController : MonoBehaviour {

		public float Speed = 20;
        public bool credits = false;
        public GameObject UICanvas;
		private IsoTransform _isoTransform;
        public AudioSource elevSound;
        private bool soundPlayed;
        public ConversationEngine CEngine;
        public JamesController james;
        public Elev_DoorController doorController;

        public AudioSource BG_Music;
        public AudioClip Credits_Music;

        public AudioSource elevSoundFloor;

        private int[] lvls = { -98, -74, -49, -24, -1 };


        void Awake() {
			_isoTransform = this.GetOrAddComponent<IsoTransform>();            
        }

        void Update() {
            //translate on isotransform
            if(!credits && james.onDay5Line && ((_isoTransform.Position.y >= -98.47604f && Input.GetAxis("Vertical") < 0) || (_isoTransform.Position.y <= -1.243564f && Input.GetAxis("Vertical") > 0) ))
            {
                if (Input.GetButtonDown("Sprint"))
                {
                    Speed = 25;
                }
                else
                {
                    Speed = selectSpeed();
                }
                _isoTransform.Translate(new Vector3(0, Input.GetAxis("Vertical"), 0) * Time.deltaTime * Speed);
                if (!soundPlayed)
                {
                    elevSoundFloor.Play();
                    elevSound.Play();
                    soundPlayed = true;
                }
            } else if (!credits && ((_isoTransform.Position.y >= -98.47604f && Input.GetAxis("Vertical") < 0) || (_isoTransform.Position.y <= -1.243564f && Input.GetAxis("Vertical") > 0) && !(credits)) && !CEngine.convActive && !james.speaking) {
                if (Input.GetButtonDown("Sprint"))
                {
                    Speed = 25;
                }else
                {
                    Speed = selectSpeed();
                }
                _isoTransform.Translate(new Vector3(0, Input.GetAxis("Vertical"), 0) * Time.deltaTime * Speed);
                if (!soundPlayed)
                {
                    elevSoundFloor.Play();
                    elevSound.Play();
                    soundPlayed = true;
                }
            } else if (credits && _isoTransform.Position.y >= -98.47604f)
            {
                _isoTransform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * (Speed * 3f));
            } else if (credits && _isoTransform.Position.y < -98.47604f && _isoTransform.Position.y >= -266)
            {
                _isoTransform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * (Speed * 0.45f));
            } else if (!credits && _isoTransform.Position.y <= -99f)
            {
                _isoTransform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * (Speed * 5f));
            } else if (credits && (_isoTransform.Position.y < -266 && _isoTransform.Position.y > -270)) {
                _isoTransform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * (Speed * 0.1f));
            }
            else if(credits)
            {
                StartCoroutine(delayEnd());
            }

            if(Input.GetAxis("Vertical") == 0)
            {
                elevSoundFloor.Stop();
                elevSound.Stop();
                soundPlayed = false;
            }
        }

        IEnumerator delayEnd()
        {
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }

        public void Credit_Sequence()
        {
            Debug.Log("Credits");
            credits = true;
            BG_Music.Stop();
            BG_Music.clip = Credits_Music;
            BG_Music.volume = 1;
            BG_Music.Play();
            UICanvas.SetActive(false);
        }

        private int selectSpeed()
        {
            for (int i = 0; i < 5; i++)
            {
                if (Mathf.Abs(_isoTransform.Position.y - lvls[i]) < 1)
                {
                    return 4;
                }else if (Mathf.Abs(_isoTransform.Position.y - lvls[i]) < 2)
                {
                    return 5;
                }else if (Mathf.Abs(_isoTransform.Position.y - lvls[i]) < 3)
                {
                    return 7;
                }else if (Mathf.Abs(_isoTransform.Position.y - lvls[i]) < 4)
                {
                    return 10;
                }
                else if (Mathf.Abs(_isoTransform.Position.y - lvls[i]) < 5)
                {
                    return 16;
                }
            }
            return 20;
        }
	}
}
