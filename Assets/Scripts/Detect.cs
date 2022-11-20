using Scripts.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts {
    [RequireComponent(typeof(Collider2D))]
    public class Detect : MonoBehaviour {
        [System.Serializable]
        public class OnDetect : UnityEvent<Transform> {};
        public OnDetect onDetect;
        public string collisionTag;

        Collider2D view;

        void OnTriggerEnter2D(Collider2D collider) {
            if (string.IsNullOrEmpty(collisionTag) || collider.tag == collisionTag) {
                onDetect.Invoke(collider.transform);
            }
        }

        void OnTriggerExit2D(Collider2D collider) {
            onDetect.Invoke(null);
        }
    }
}
