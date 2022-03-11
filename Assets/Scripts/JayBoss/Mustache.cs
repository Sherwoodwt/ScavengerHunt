using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.JayBoss {
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Collider2D))]
    public class Mustache : MonoBehaviour {
        public GameObject deadPrefab;
        public Vector2[] points;
        [Range(10, 200)]
        public int segmentSize;

        new AudioSource audio;

        void Start() {
            audio = GetComponent<AudioSource>();
            StartCoroutine(Move());
        }

        IEnumerator Move() {
            for (int i = 0; i < points.Length-1; i++) {
                for (int ii = 0; ii < segmentSize; ii++) {
                    transform.position = Vector3.Lerp(points[i], points[i+1], ((float)ii)/segmentSize);
                    yield return new WaitForFixedUpdate();
                }

                yield return new WaitForSeconds(1);
            }

            GameObject.Destroy(this.gameObject, 1);
        }

        IEnumerator Reset() {
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("HackGame", LoadSceneMode.Single);
        }

        void OnTriggerEnter2D(Collider2D collider) {
            if (collider.gameObject.CompareTag("Player")) {
                audio.Play();
                var pos = collider.transform.position;
                GameObject.Destroy(collider.gameObject);
                GameObject.Instantiate(deadPrefab, pos, Quaternion.identity);

                StartCoroutine(Reset());
            }
        }
    }
}
