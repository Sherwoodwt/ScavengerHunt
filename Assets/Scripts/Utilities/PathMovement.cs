using UnityEngine;

namespace Scripts.Utilities {
    public class PathMovement : MonoBehaviour {
        [Range(0, .1f)]
        public float speed;
        public Vector2[] points;
        public bool flippable = false;

        SpriteRenderer spriteRenderer;
        int cur;
        int inc = -1;

        void Start() {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update() {
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
    }
}
