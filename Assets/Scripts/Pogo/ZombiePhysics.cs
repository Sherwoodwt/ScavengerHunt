using UnityEngine;
using Scripts.Movement;

namespace Scripts.Pogo {
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class ZombiePhysics : MonoBehaviour {
        public float speed;
        public float gravity;
        public LayerMask collisionMask;

        int dir = 1;
        SpriteRenderer spriteRenderer;
        new BoxCollider2D collider;

        void Start() {
            spriteRenderer = GetComponent<SpriteRenderer>();
            collider = GetComponent<BoxCollider2D>();
        }

        void FixedUpdate() {
            var velocity = new Vector2(speed*dir * Time.deltaTime, -gravity * Time.deltaTime);
            var collideVelocity = CollisionDetection.CheckCollision(collider, velocity, collisionMask);
            if (collideVelocity.x != velocity.x) {
                Flip();
            }
            transform.position += (Vector3)collideVelocity;
        }

        void Flip() {
            dir = -dir;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }
}
