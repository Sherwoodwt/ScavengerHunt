using UnityEngine;

namespace Scripts {
    public class Rotate : MonoBehaviour {
        public float speed;

        void FixedUpdate() {
            transform.RotateAround(transform.position, Vector3.forward, speed);
        }
    }
}
