using Scripts.Utilities;
using UnityEngine;

namespace Scripts {
    [RequireComponent(typeof(BoxCollider2D))]
    public class FoosballPhysics : MonoBehaviour {
        public LayerMask collisionMask;
        public Transform sprite;
        public float speed;

        new BoxCollider2D collider;
        Vector2 velocity;

        void Start() {
            collider = GetComponent<BoxCollider2D>();
            velocity = new Vector2(-speed, speed);
        }

        void FixedUpdate() {
            var collideVelocity = CollisionDetection.CheckCollision(collider, velocity, collisionMask);
            if (collideVelocity.y != velocity.y) {
                velocity.y = -velocity.y;
            }
            if (collideVelocity.x != velocity.x) {
                velocity.x = -velocity.x;
            }
            transform.position = transform.position + (Vector3)velocity;
            sprite.RotateAround(
                sprite.transform.position,
                velocity.y > 0 ? Vector3.forward : Vector3.back,
                5);
        }
    }
}
