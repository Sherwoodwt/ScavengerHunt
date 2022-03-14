using UnityEngine;

namespace Scripts.Pool {
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Collider2D))]
    public class Fragment : MonoBehaviour {
        public PoolGame poolGame;

        new AudioSource audio;

        void Start() {
            audio = GetComponent<AudioSource>();
        }

        void OnTriggerEnter2D(Collider2D collider) {
            if (collider.gameObject.CompareTag("Player")) {
                audio.Play();
                poolGame.AddScore();
                GameObject.Destroy(this.gameObject, .1f);
            }
        }
    }
}
