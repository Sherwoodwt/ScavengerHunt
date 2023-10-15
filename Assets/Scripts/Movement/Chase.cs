using System.Collections;
using Scripts.Utilities;
using UnityEngine;

namespace Scripts.Movement {
    public class Chase : MonoBehaviour {
        public Transform target;
        public float speed;

        bool flippin;
        PathMovement movement;
        SpriteRenderer spriteRenderer;

        void Start() {
            movement = GetComponent<PathMovement>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void StartChasin(Transform newTarget) {
            if (movement != null) {
                // if pathmovement, disable when chasin, enable when not
                movement.enabled = newTarget == null;
            }
            target = newTarget;
        }

        void FixedUpdate() {
            if (target != null) {
                if (spriteRenderer != null) {
                    spriteRenderer.flipX = target.position.x < transform.position.x ? true : false;
                }
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
