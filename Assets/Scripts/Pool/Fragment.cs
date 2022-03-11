using UnityEngine;

namespace Scripts.Pool {
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Collider2D))]
    public class Fragment : MonoBehaviour {
        public PoolGame poolGame;

        new AudioSource audio;
        new Collider2D collider;

        void Start() {
            audio = GetComponent<AudioSource>();
            collider = GetComponent<Collider2D>();
        }

        void OnTriggerEnter2D(Collider2D collider) {
            if (collider.gameObject.tag == "Player") {
                poolGame.AddScore();
                audio.Play();
                GameObject.Destroy(this.gameObject);
            }
        }
    }
}
