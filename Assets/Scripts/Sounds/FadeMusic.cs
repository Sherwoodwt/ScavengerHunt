using UnityEngine;

namespace Scripts.Sounds {
    public class FadeMusic : MonoBehaviour {
        public float minVol, maxVol, factor;

        new AudioSource audio;
        AudioSource mainAudio;
        new CircleCollider2D collider;

        float startVolume;

        void Start() {
            audio = GetComponent<AudioSource>();
            collider = GetComponent<CircleCollider2D>();
            mainAudio = GameObject.FindWithTag("MusicPlayer").GetComponent<AudioSource>();
            startVolume = mainAudio.volume;
        }

        void OnTriggerEnter2D(Collider2D other) {
            audio.Play();
        }

        void OnTriggerExit2D(Collider2D other) {
            audio.Stop();
        }

        void OnTriggerStay2D(Collider2D other) {
            var distance = (other.transform.position - transform.position).magnitude;
            audio.volume = ((collider.radius - distance) / collider.radius) * (maxVol - minVol);
            mainAudio.volume = (distance / collider.radius) * (startVolume);
        }
    }
}
