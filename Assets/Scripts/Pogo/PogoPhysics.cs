using Scripts.Movement;
using UnityEngine;

namespace Scripts.Pogo {
    public class PogoPhysics : BasePhysics {
        [SerializeField] float gravity;
        [SerializeField] LayerMask collisionMask;
        [SerializeField] float bounceSpeed;
        [SerializeField] float maxSpeed;
        Vector2 velocity;

        public void MoveLeft() {
            input.x = -1;
        }

        public void MoveRight() {
            input.x = 1;
        }

        public void MoveStop() {
            input.x = 0;
        }

        void FixedUpdate() {
            var acceleration = new Vector2(input.x * accel * Time.deltaTime, -gravity * Time.deltaTime);
            if (acceleration.x == 0 && velocity.x != 0) {
                // apply drag
                acceleration.x = -Mathf.Sign(velocity.x) * accel/2 * Time.deltaTime;
                if (Mathf.Sign(velocity.x + acceleration.x) != Mathf.Sign(velocity.x)) {
                    acceleration.x = -velocity.x;
                }
            }
            velocity += acceleration;

            velocity.x = Mathf.Max(Mathf.Min(velocity.x, maxSpeed), -maxSpeed);
            velocity.y = Mathf.Max(Mathf.Min(velocity.y, bounceSpeed), -bounceSpeed);

            var prevY = velocity.y;
            velocity = CollisionDetection.CheckCollision(collider, velocity, collisionMask);
            // ground collision
            if (prevY < 0 && velocity.y != prevY) {
                velocity.y = bounceSpeed;
            }

            transform.position += (Vector3)velocity;
        }
    }
}
