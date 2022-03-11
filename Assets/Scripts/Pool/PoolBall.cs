using System.Linq;
using Scripts.Utilities;
using UnityEngine;

namespace Scripts.Pool {
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class PoolBall : MonoBehaviour {
        public float acceleration;
        public float maxSpeed;
        [Range(0, 10)]
        public float rotationSpeed;
        public bool chase;

        public Transform Target { set { target = value; } }

        Transform target;
        Vector3 velocity;
        new AudioSource audio;
        new CircleCollider2D collider;

        void Start() {
            audio = GetComponent<AudioSource>();
            collider = GetComponent<CircleCollider2D>();
        }

        void FixedUpdate() {
            if (chase) {
                var direction = (target.position - transform.position).normalized;
                var accel = direction * acceleration;

                velocity += accel;

                if (velocity.magnitude > maxSpeed) {
                    velocity = velocity.normalized * maxSpeed;
                }
            }
        }

        void Update() {
            if (rotationSpeed > 0) {
                transform.RotateAround(transform.position, Vector3.forward, rotationSpeed);
            }
        }

        void OnTriggerEnter2D(Collider2D collider) {
            if (collider.gameObject.tag == "Projectile") {
                audio.Play();
                GameObject.Destroy(this.gameObject);
            }
        }
    }
}
