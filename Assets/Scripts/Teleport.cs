using UnityEngine;

namespace Scripts {
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Collider2D))]
    public class Teleport : MonoBehaviour {
        public Vector2 destination;
        
        new AudioSource audio;

        void Start() {
            audio = GetComponent<AudioSource>();
        }

        void OnTriggerEnter2D(Collider2D collider) {
            if (collider.tag == "Player") {
                audio.Play();
                collider.transform.position = destination;
            }
        }
    }
}