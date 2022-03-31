using System.Collections;
using UnityEngine;

namespace Scripts {
    public class Chase : MonoBehaviour {
        public Transform target;
        public float speed;

        bool flippin;

        public void StartChasin(Transform newTarget) {
            target = newTarget;
        }

        void FixedUpdate() {
            if (target != null) {
                transform.localScale = new Vector3(target.position.x < transform.position.x ? 1 : -1, 1, 1);
                transform.position += (target.position - transform.position).normalized * speed;
            }

            if (target == null && !flippin) {
                StartCoroutine(Flip());
            }
        }

        IEnumerator Flip() {
            flippin = true;
            yield return new WaitForSeconds(3);
            transform.localScale = new Vector3(transform.localScale.x*-1, 1, 1);
            flippin = false;
        }
    }
}
