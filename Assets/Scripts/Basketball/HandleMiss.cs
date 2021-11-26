using UnityEngine;
using Scripts.Utilities;

namespace Scripts.Basketball {
    public class HandleMiss : MonoBehaviour {
        public LayerMask ballLayer;
        public BasketballHoop hoop;

        void OnTriggerEnter2D(Collider2D collider) {
            if (collider.gameObject.InLayerMask(ballLayer)) {
                hoop.ResetScore();
                GameObject.Destroy(collider.gameObject);
            }
        }
    }
}
