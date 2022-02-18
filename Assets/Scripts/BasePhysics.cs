using UnityEngine;

namespace Scripts {
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class BasePhysics : MonoBehaviour {
        public float accel;
        protected new BoxCollider2D collider;

        void Start() {
            collider = GetComponent<BoxCollider2D>();
        }
    }
}
