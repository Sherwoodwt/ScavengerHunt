using System.Collections;
using UnityEngine;

namespace Scripts.Pool {
    public class CueBall : MonoBehaviour {
        public float speed;

        public Vector3 Target { get; set; }

        
       Vector3 direction;

        void Start() {
            direction = (Target - transform.position).normalized;
            StartCoroutine(Expire());
        }

        void FixedUpdate() {
            transform.position += direction * speed;
        }

        IEnumerator Expire() {
            yield return new WaitForSeconds(2);
            GameObject.DestroyImmediate(this.gameObject);
        }
    }
}
