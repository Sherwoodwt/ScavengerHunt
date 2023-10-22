﻿using UnityEngine;

namespace Scripts.Movement {
    public class NormalPhysics : BasePhysics {
        public LayerMask collisionMask;

        void LateUpdate() {
            var velocity = new Vector2(input.x * accel * Time.deltaTime, input.y * accel * Time.deltaTime);

            velocity = CollisionDetection.CheckCollision(collider, velocity, collisionMask);

            transform.position = new Vector3(transform.position.x + velocity.x, transform.position.y + velocity.y, transform.position.z);
        }
    }
}
