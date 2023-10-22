using System.Collections;
using Scripts.Utilities;
using UnityEngine;

namespace Scripts.Pool {
    public class CueBall : MonoBehaviour {
        public float speed;
        public LayerMask walls;

        public Vector2 Target { get; set; }

        
       Vector2 direction;

        void Start() {
            direction = (Target - (Vector2)transform.position).normalized;
            StartCoroutine(Expire());
        }

        void FixedUpdate() {
            transform.position += (Vector3)direction * speed;
        }

        void OnTriggerEnter2D(Collider2D collider) {
            if (collider.gameObject.InLayerMask(walls)) {
                GameObject.Destroy(this.gameObject);
            }
        }

        IEnumerator Expire() {
            yield return new WaitForSeconds(1);
            GameObject.Destroy(this.gameObject);
        }
    }
}
