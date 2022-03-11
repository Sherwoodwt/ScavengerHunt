using Scripts.Utilities;
using UnityEngine;

namespace Scripts.JayBoss {
    [RequireComponent(typeof(BoxCollider2D))]
    public class GermControls : MonoBehaviour {
        public LayerMask wallMask;
        [Range(.01f, 1)]
        public float speed;

        new BoxCollider2D collider;
        Vector2 velocity;

        void Start() {
            collider = GetComponent<BoxCollider2D>();
        }

        void Update() {
            if (Input.GetKey(KeyCode.A)) {
                velocity.x = -speed;
            } else if (Input.GetKey(KeyCode.D)) {
                velocity.x = speed;
            } else {
                velocity.x = 0;
            }
            if (Input.GetKey(KeyCode.S)) {
                velocity.y = -speed;
            } else if (Input.GetKey(KeyCode.W)) {
                velocity.y = speed;
            } else {
                velocity.y = 0;
            }
        }

        void FixedUpdate() {
            if (velocity != Vector2.zero) {
                velocity = CollisionDetection.CheckCollision(collider, velocity, wallMask);
                transform.position += (Vector3)velocity;
            }
        }
    }
}
