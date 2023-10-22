using UnityEngine;

namespace Scripts.Pool {
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Collider2D))]
    public class Fragment : MonoBehaviour {
        public PoolGame poolGame;

        new AudioSource audio;
        SpriteRenderer spriteRenderer;
        new Collider2D collider;

        void Start() {
            audio = GetComponent<AudioSource>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            collider = GetComponent<Collider2D>();
        }

        void OnTriggerEnter2D(Collider2D target) {
            if (target.gameObject.CompareTag("Player")) {
                audio.Play();
                poolGame.AddScore();
                spriteRenderer.enabled = false;
                collider.enabled = false;
                GameObject.Destroy(this.gameObject, 1);
            }
        }
    }
}
