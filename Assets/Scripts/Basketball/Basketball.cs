using UnityEngine;

namespace Scripts.Basketball {
    public class Basketball : MonoBehaviour {
        [Range(.01f, .3f)]
        public float speed;

        void FixedUpdate() {
            transform.position = transform.position + (Vector3.right * speed);
            // TODO: Add rotation
        }
    }
}
