using UnityEngine;
using Assets.UltimateIsometricToolkit.Scripts.Core;
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

        private int[] lvls = { -98, -74, -49, -24, -1 };


        void Awake() {
			_isoTransform = this.GetOrAddComponent<IsoTransform>();
                                                   
        }

		void Update() {
            //translate on isotransform
            if (((_isoTransform.Position.y >= -98.47604f && Input.GetAxis("Vertical") < 0) || (_isoTransform.Position.y <= -1.243564f && Input.GetAxis("Vertical") > 0) && !(credits)) && !CEngine.convActive && !james.speaking) {
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
                    elevSound.Play();
                    soundPlayed = true;
                }
            } else if (credits && _isoTransform.Position.y >= -98.47604f)
            {
                _isoTransform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * (Speed * 1.2f));
            } else if (credits && _isoTransform.Position.y < -98.47604f && _isoTransform.Position.y >= -237)
            {
                _isoTransform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * (Speed * 0.27f));
            } else if (!credits && _isoTransform.Position.y <= -99f)
            {
                _isoTransform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * (Speed * 5f));
            } else if (credits && (_isoTransform.Position.y < -237 && _isoTransform.Position.y > -241)) {
                _isoTransform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * (Speed * 0.1f));
            }
            if ((Input.anyKey && credits) || _isoTransform.Position.y <= -250f)
            {
                credits = false;
                UICanvas.SetActive(true);
            }

            if(Input.GetAxis("Vertical") == 0)
            {
                elevSound.Stop();
                soundPlayed = false;
            }
        }

        public void Credit_Sequence()
        {
            credits = true;
            UICanvas.SetActive(false);
        }

        private int selectSpeed()
        {
            for (int i = 0; i < 5; i++)
            {
                if (Mathf.Abs(_isoTransform.Position.y - lvls[i]) < 3)
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
