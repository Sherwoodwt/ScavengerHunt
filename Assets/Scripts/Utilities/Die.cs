using UnityEngine;

namespace Scripts.Utilities {
    public class Die : MonoBehaviour {
        public float time;

        void Start() {
            Destroy(this.gameObject, time);
        }
    }
}