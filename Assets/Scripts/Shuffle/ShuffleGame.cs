using System.Collections;
using Scripts.Utilities;
using UnityEngine;

namespace Scripts.Shuffle {
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(DisableMovement))]
    public class ShuffleGame : MonoBehaviour {
        public ItemObject prizeItem;
        public SpriteRenderer prizeRenderer;
        public Transform[] jars;
        public int numberOfShuffles = 50;
        public GameObject handPrefab;
        public float minWaitTime;
        

        Collider2D startCollider;
        DisableMovement disableMovement;


        void Start() {
            startCollider = GetComponent<Collider2D>();
            disableMovement = GetComponent<DisableMovement>();
            disableMovement.enabled = false;
            prizeRenderer.sprite = prizeItem.sprite;
        }

        void OnTriggerEnter2D(Collider2D collider) {
            if (collider.tag == "Player") {
                // Debug.Log("START");
                startCollider.enabled = false;
                disableMovement.enabled = true;
                StartCoroutine(ShuffleAnimation());
            }
        }

        IEnumerator ShuffleAnimation() {
            // Debug.Log("Animation Start");
            var hand = GameObject.Instantiate(handPrefab, jars[1].position, Quaternion.identity);
            var path = hand.GetComponent<PathMovement>();
            if (path != null) {
                path.points = new Vector2[2] {
                    jars[1].position,
                    prizeRenderer.transform.position,
                };
            }
            var handCollider = hand.GetComponent<Collider2D>();
            if (handCollider != null) {
                handCollider.enabled = false;
            }

            yield return new WaitForSeconds(.2f);
            GameObject.DestroyImmediate(prizeRenderer.gameObject);

            yield return new WaitForSeconds(1);
            float waitTime = .05f;
            for (int i = 0; i < numberOfShuffles; i++) {
                int j1 = Random.Range(0, jars.Length);
                int j2 = (j1 + 1) % jars.Length;
                var center = (jars[j1].transform.position + jars[j2].transform.position) / 2;
                yield return SwitchPlaces(jars[j1], jars[j2], center, waitTime);

                var temp = jars[j1];
                jars[j1] = jars[j2];
                jars[j2] = temp;

                waitTime = Mathf.Max(waitTime/2, minWaitTime);
            }
            disableMovement.enabled = false;
        }

        IEnumerator SwitchPlaces(Transform jar1, Transform jar2, Vector2 center, float waitTime) {
            // Debug.Log("SWITCHING");
            float numSegments = 64;
            var angle = 180 / numSegments * (jar1.position.x < jar2.position.x ? 1 : -1);
            for (int i = 0; i < numSegments; i++) {
                jar1.RotateAround(center, Vector3.back, angle);
                jar1.rotation = Quaternion.identity;
                jar2.RotateAround(center, Vector3.back, -angle);
                jar2.rotation = Quaternion.identity;
                yield return new WaitForSeconds(waitTime);
            }
        }
    }
}
