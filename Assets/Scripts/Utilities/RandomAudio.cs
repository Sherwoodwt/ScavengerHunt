using UnityEngine;

namespace Scripts.Utilities {
    [RequireComponent(typeof(AudioSource))]
    public class RandomAudio : MonoBehaviour {
        public AudioClip[] clips;

        new AudioSource audio;

        void Start() {
            audio = GetComponent<AudioSource>();
        }

        public void PlayAudio() {
            if (audio.isPlaying) {
                audio.Stop();
            }
            var i = Random.Range(0, clips.Length);
            audio.PlayOneShot(clips[i]);
        }
    }
}
