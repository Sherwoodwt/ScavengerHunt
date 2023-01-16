using UnityEngine;

namespace Scripts.Movement {
    public class Rotate : MonoBehaviour {
        public float speed;

        void FixedUpdate() {
            transform.RotateAround(transform.position, Vector3.forward, speed);
        }
    }
}
