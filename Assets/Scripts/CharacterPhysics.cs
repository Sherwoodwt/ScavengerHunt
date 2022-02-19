using UnityEngine;
using Scripts.Utilities;

namespace Scripts {
    public class CharacterPhysics : BasePhysics {
        public LayerMask collisionMask;
        public Vector2 input;

        void LateUpdate() {
            var velocity = new Vector2(input.x * accel * Time.deltaTime, input.y * accel * Time.deltaTime);

            velocity = CollisionDetection.CheckCollision(collider, velocity, collisionMask);

            transform.position = new Vector3(transform.position.x + velocity.x, transform.position.y + velocity.y, transform.position.z);
        }
    }
}
