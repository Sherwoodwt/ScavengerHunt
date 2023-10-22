using Scripts.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts {
    // Component used to invoke an event upon collision, used for detection boxes (ChasePlayer)
    [RequireComponent(typeof(Collider2D))]
    public class Detect : MonoBehaviour {
        [System.Serializable]
        public class OnDetect : UnityEvent<Transform> {};
        public OnDetect onDetect;
        public string collisionTag;

        Collider2D view;

        void OnTriggerEnter2D(Collider2D collider) {
            if (string.IsNullOrEmpty(collisionTag) || collider.tag == collisionTag) {
                Debug.Log(collider.transform);
                onDetect.Invoke(collider.transform);
            }
        }

        void OnTriggerExit2D(Collider2D collider) {
            onDetect.Invoke(null);
        }
    }
}
