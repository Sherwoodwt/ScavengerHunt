using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.JayBoss {
    [RequireComponent(typeof(AudioSource))]
    public class HackGame : MonoBehaviour {
        public AudioClip countSound, startSound;
        public GameObject enemyPrefab1, enemyPrefab2, prizePrefab;
        public GermControls controls;
        public InventoryObject inventory;
        public ItemObject prizeItem;
        public Transform[] spawnPoints;
        public BoxCollider2D outline;
        public int enemyCount;

        GameObject lastEnemy, prize;
        new AudioSource audio;
        bool done;

        void Start() {
            audio = GetComponent<AudioSource>();

            StartCoroutine(PlayGame());
        }

        void Update() {
            if (done && lastEnemy == null && prize == null && !inventory.Contains(prizeItem)) {
                audio.Play();
                prize = Instantiate(prizePrefab);
            } else if (inventory.Contains(prizeItem) && prize == null) {
                SceneManager.LoadScene("LivingRoom");
            }
        }

        IEnumerator PlayGame() {
            yield return StartCoroutine(StartGame());
            yield return StartCoroutine(SpawnEnemies());
        }

        IEnumerator SpawnEnemies() {
            for (int i = 0; i < enemyCount; i++) {
                var obj = GameObject.Instantiate(enemyPrefab1, transform.position, Quaternion.identity);
                var mustache = obj.GetComponent<Mustache>();
                mustache.points = GeneratePoints(Random.Range(4, 8));

                yield return new WaitForSeconds(2f);
            }

            yield return new WaitForSeconds(10);
            for (int i = 0; i < enemyCount * 1.5f; i++) {
                var prefab = i % 2 == 0 ? enemyPrefab1 : enemyPrefab2;
                var obj = GameObject.Instantiate(prefab, transform.position, Quaternion.identity);
                var mustache = obj.GetComponent<Mustache>();
                mustache.points = GeneratePoints(Random.Range(4, 8));

                yield return new WaitForSeconds(1.5f);
            }

            yield return new WaitForSeconds(8);
            var o = GameObject.Instantiate(enemyPrefab1, transform.position, Quaternion.identity);
            var stash = o.GetComponent<Mustache>();
            stash.points = GeneratePoints(3);
            lastEnemy = o;

            this.done = true;
        }

        Vector2[] GeneratePoints(int pointCount) {
            var points = new Vector2[pointCount];

            var outline = this.outline.bounds.extents;
            for (int i = 0; i < points.Length; i++) {
                if (i == 0 || i == points.Length - 1) {
                    points[i] = spawnPoints[Random.Range(0,spawnPoints.Length)].position;
                } else {
                    points[i] = new Vector2(
                        Random.Range(-outline.x, outline.x),
                        Random.Range(-outline.y, outline.y));
                }
            }
            return points;
        }

        IEnumerator StartGame() {
            controls.enabled = false;

            audio.clip = countSound;
            yield return new WaitForSeconds(1);
            audio.Play();
            yield return new WaitForSeconds(1);
            audio.Play();
            yield return new WaitForSeconds(1);
            audio.Play();
            yield return new WaitForSeconds(1);

            audio.clip = startSound;
            audio.Play();

            controls.enabled = true;
        }
    }
}
