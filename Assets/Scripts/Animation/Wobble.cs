using UnityEngine;

namespace Scripts.Animation {
    public class Wobble : MonoBehaviour {
        public float speed;
        public float maxIntensity = 30;
        public bool randomStart;

        [SerializeField] bool backswing;

        void Start() {
            if (randomStart) {
                transform.RotateAround(transform.position, Vector3.forward, Random.Range(-maxIntensity, maxIntensity));
            }
        }

        void FixedUpdate() {
            var cur = transform.rotation.eulerAngles.z;
            if (cur > maxIntensity && cur < 360 - maxIntensity) {
                backswing = !backswing;
            }
            var amount = backswing ? -speed : speed;
            transform.RotateAround(transform.position, Vector3.forward, amount);
        }
    }
}
