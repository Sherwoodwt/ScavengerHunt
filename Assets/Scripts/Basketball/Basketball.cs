using UnityEngine;

namespace Scripts.Basketball {
    public class Basketball : MonoBehaviour {
        [Range(.01f, .3f)]
        public float speed;
        public Transform sprite;

        void FixedUpdate() {
            transform.position = transform.position + (Vector3.right * speed);
            sprite.RotateAround(transform.position, Vector3.forward, 5);
        }
    }
}
