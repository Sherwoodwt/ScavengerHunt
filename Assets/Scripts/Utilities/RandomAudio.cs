using System.Linq;
using UnityEngine;

namespace Scripts.Utilities {
    [RequireComponent(typeof(AudioSource))]
    public class RandomAudio : MonoBehaviour {
        public AudioClip[] clips;

        public void PlayAudio() {
            var audio = GetComponent<AudioSource>();
            if (audio.isPlaying) {
                audio.Stop();
            }
            var i = Random.Range(0, clips.Length);
            audio.PlayOneShot(clips[i]);
        }
    }
}
