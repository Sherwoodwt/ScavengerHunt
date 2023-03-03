using UnityEngine;

namespace Scripts.Movement {
    /// <summary>
    /// Used for things that move along a list of points
    /// </summary>
    public class PathMovement : MonoBehaviour, Influencable {
        public float speed;
        public Vector2[] points;
        public bool flippable = false;

        // used to store speed for disabling when focus set
        float cachedSpeed;

        SpriteRenderer spriteRenderer;
        int cur;
        int inc = -1;

        void Start() {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void FixedUpdate() {
            var dir = ((Vector3)points[cur] - transform.position).normalized;
            var velocity = speed * dir;
            transform.position += velocity;

            if (((Vector2)transform.position - points[cur]).magnitude < speed) {
                if (cur == 0 || cur == points.Length - 1)
                    inc = -inc;
                cur += inc;
            }

            if (flippable && spriteRenderer != null) {
                spriteRenderer.flipX = points[cur].x - transform.position.x > 0;
            }
        }

        public void SetFocus(Vector3 playerPos) {
            cachedSpeed = speed;
            speed = 0;
        }

        public void DisableFocus() {
            speed = cachedSpeed;
        }
    }
}
