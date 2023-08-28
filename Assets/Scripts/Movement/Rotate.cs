using UnityEngine;

namespace Scripts.Movement {
    public class Rotate : MonoBehaviour {
        // Optional lock check for rotation, used for portals that rotate when active
        public LockObject lockObject;
        public float speed;

        void FixedUpdate() {
            if (lockObject == null || lockObject.unlocked) {
                transform.RotateAround(transform.position, Vector3.forward, speed);
            }
        }
    }
}
