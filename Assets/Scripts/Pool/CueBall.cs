using System.Collections;
using UnityEngine;

namespace Scripts.Pool {
    public class CueBall : MonoBehaviour {
        public float speed;

        public Vector2 Target { get; set; }

        
       Vector2 direction;

        void Start() {
            direction = (Target - (Vector2)transform.position).normalized;
            StartCoroutine(Expire());
        }

        void FixedUpdate() {
            transform.position += ((Vector3)direction * speed);
        }

        IEnumerator Expire() {
            yield return new WaitForSeconds(2);
            GameObject.DestroyImmediate(this.gameObject);
        }
    }
}
