using UnityEngine;

namespace Scripts.Movement {
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class BasePhysics : MonoBehaviour {
        public float accel;
        protected new BoxCollider2D collider;
        public Vector2 input;

        void Start() {
            collider = GetComponent<BoxCollider2D>();
        }
    }
}
