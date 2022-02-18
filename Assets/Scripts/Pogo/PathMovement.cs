using UnityEngine;

namespace Scripts.Pogo {
    public class PathMovement : MonoBehaviour {
        [Range(0, .1f)]
        public float speed;
        public Vector2[] points;

        int cur;
        int inc = -1;

        void Update() {
            var dir = ((Vector3)points[cur] - transform.position).normalized;
            var velocity = speed * dir;
            transform.position += velocity;

            if (((Vector2)transform.position - points[cur]).magnitude < speed) {
                if (cur == 0 || cur == points.Length - 1)
                    inc = -inc;
                cur += inc;
            }
        }
    }
}
