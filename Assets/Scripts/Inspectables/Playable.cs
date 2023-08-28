using UnityEngine;

namespace Scripts.Inspectables {
    [RequireComponent(typeof(AudioSource))]
    public class Playable : Inspectable {        
        public AudioClip usual, success, fail;

        new AudioSource audio;

        void Start() {
            audio = GetComponent<AudioSource>();
        }

        public override void NoItemResponse() {
            if (usual != null) {
                audio.clip = usual;
                audio.Play();
            } // else nothing, used for success/fail only situations
        }

        public override void CorrectResponse() {
            if (success != null) {
                audio.clip = success;
                audio.Play();
            } else {
                base.CorrectResponse();
            }
        }

        public override void IncorrectResponse() {
            if (fail != null) {
                audio.clip = fail;
                audio.Play();
            } else {
                base.IncorrectResponse();
            }
        }
    }
}
