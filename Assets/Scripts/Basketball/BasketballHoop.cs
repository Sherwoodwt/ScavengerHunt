using UnityEngine;
using Scripts.Utilities;

namespace Scripts.Basketball {
    [RequireComponent(typeof(AudioSource))]
    public class BasketballHoop : MonoBehaviour {
        [Range(.01f, .3f)]
        public float speed;
        public LayerMask ballLayer;
        public int goal = 5;
        public ItemObject prize;
        public InventoryObject inventory;
        public AudioClip scoreSound;
        public AudioClip missSound;
        public AudioClip prizeSound;

        new AudioSource audio;
        float baseSpeed;

        public void ResetScore() {
            audio.PlayOneShot(missSound);
            speed = baseSpeed;
            score = 0;
        }

        [SerializeField] int score = 0;
        float direction = 1;

        void Start() {
            baseSpeed = speed;
            audio = GetComponent<AudioSource>();
        }

        void FixedUpdate() {
            transform.position = new Vector3(transform.position.x, transform.position.y + (direction * speed));
        }

        void OnTriggerEnter2D(Collider2D collider) {
            direction = -direction;
            if (collider.gameObject.InLayerMask(ballLayer)) {
                score++;
                GameObject.Destroy(collider.gameObject);
                if (score >= goal && prize != null) {
                    audio.PlayOneShot(prizeSound);
                    inventory.Add(prize);
                } else {
                    audio.PlayOneShot(scoreSound);
                    speed *= 1.5f;
                }
            }
        }
    }
}
