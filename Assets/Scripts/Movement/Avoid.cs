using UnityEngine;

namespace Scripts.Movement {
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(NormalPhysics))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Avoid : MonoBehaviour {
        public Transform target;
        public float speed, distance;

        Animator animator;
        NormalPhysics physics;
        SpriteRenderer sprite;

        void Start() {
            physics = GetComponent<NormalPhysics>();
            sprite = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            animator.enabled = false;
        }

        void Update() {
            var dist = transform.position - target.position;
            if (dist.magnitude < distance) {
                animator.enabled = true;
                physics.input = dist.normalized * speed;
                sprite.flipX = physics.input.x > 0;
            } else {
                physics.input = Vector2.zero;
                animator.enabled = false;
            }
        }
    }
}
