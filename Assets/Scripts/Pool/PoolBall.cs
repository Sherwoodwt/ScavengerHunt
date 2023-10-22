using Scripts.Utilities;
using UnityEngine;

namespace Scripts.Pool {
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(Explode))]
    public class PoolBall : MonoBehaviour {
        public float acceleration;
        public float maxSpeed;
        [Range(0, 10)]
        public float rotationSpeed;
        public bool chase;
        public AudioClip spawnSound, dieSound;
        public PoolGame poolGame;

        public Transform Target { set { target = value; } }

        [SerializeField] Transform target;
        Vector3 velocity;
        Animator animator;
        new AudioSource audio;
        new CircleCollider2D collider;
        Explode explode;

        void Start() {
            animator = GetComponent<Animator>();
            audio = GetComponent<AudioSource>();
            collider = GetComponent<CircleCollider2D>();
            explode = GetComponent<Explode>();
            explode.dieSound = dieSound;

            audio.clip = spawnSound;
        }

        void FixedUpdate() {
            if (rotationSpeed > 0) {
                transform.RotateAround(transform.position, Vector3.forward, rotationSpeed);
            }

            if (chase) {
                var direction = (target.position - transform.position).normalized;
                var accel = direction * acceleration;

                velocity += accel;

                if (velocity.magnitude > maxSpeed) {
                    velocity = velocity.normalized * maxSpeed;
                }

                transform.position += velocity;
            }
        }

        void OnTriggerEnter2D(Collider2D collider) {
            if (collider.gameObject.CompareTag("Projectile")) {
                GameObject.Destroy(collider.gameObject);
                this.collider.enabled = false;
                chase = false;
                rotationSpeed = 0;
                poolGame.RemoveBall(this.gameObject);
                explode.TriggerExplosion();
            }
        }
    }
}
