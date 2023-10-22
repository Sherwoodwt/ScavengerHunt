using System.Collections;
using UnityEngine;

namespace Scripts.Pool {
    public class Gun : MonoBehaviour {
        public PoolGame poolGame;
        public GameObject bullet;
        public Animator explosionAnimator;
        public Transform cannon;
        public float fireRate;
        public bool rotate;

        [SerializeField] Transform target;
        bool shooting;

        void OnTriggerEnter2D(Collider2D collider) {
            if (collider.tag == "Player") {
                target = collider.transform;
            }
        }

        void OnTriggerExit2D(Collider2D collider) {
            if (collider.tag == "Player") {
                target = null;
            }
        }

        public void SetTarget(Transform transform) {
            target = transform;
        }

        void FixedUpdate() {
            if (target != null && cannon != null) {
                if (rotate && cannon != null)
                    cannon.rotation = Quaternion.FromToRotation(Vector3.up, target.position - cannon.position);

                if (!shooting) {
                    StartCoroutine(Shoot());
                }
            }
        }

        IEnumerator Shoot() {
            shooting = true;
            if (explosionAnimator != null)
                explosionAnimator.SetTrigger("Splode");
            var obj = Instantiate(bullet);
            // obj.transform.position = explosionAnimator.transform.position;
            obj.transform.position = cannon.position;
            var cueBall = obj.GetComponent<CueBall>();
            cueBall.Target = target.position;
            var killPlayer = obj.GetComponent<PoolKillPlayer>();
            killPlayer.poolGame = poolGame;
            yield return new WaitForSeconds(fireRate);
            shooting = false;
        }
    }
}
