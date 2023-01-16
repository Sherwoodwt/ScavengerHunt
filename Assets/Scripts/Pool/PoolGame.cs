using System.Collections;
using System.Collections.Generic;
using Scripts.Inspectables;
using Scripts.Movement;
using UnityEngine;

namespace Scripts.Pool {
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(DisableMovement))]
    public class PoolGame : MonoBehaviour {
        public GameObject ballPrefab, textboxPrefab;
        public Transform target;
        public PoolControls poolControls;
        public float spawnRadius;
        public AudioClip startNoise, endNoise;
        public InventoryObject inventory;
        public ItemObject prize;
        public Sprite[] ballSprites;
        [TextArea()]
        public string victoryText;

        new AudioSource audio;
        DisableMovement disableMovement;
        [SerializeField] List<GameObject> balls;

        readonly int WIN_SCORE = 6, ANGLE_COUNT = 9, BALL_COUNT = 30;
        readonly float ANGLE_INC = 360f / 9;
        int score, curAngleInc, curSprite;
        bool spawning;

        public void AddScore() {
            score++;
            if (score == WIN_SCORE) {
                StartCoroutine(EndPool());
            }
        }

        public void RemoveBall(GameObject ball) {
            if (balls.Contains(ball)) {
                balls.Remove(ball);
            }
        }

        void Start() {
            balls = new List<GameObject>();
            audio = GetComponent<AudioSource>();
            disableMovement = GetComponent<DisableMovement>();
            spawning = true;

            StartCoroutine(StartPool());
        }

        void Update() {
            if (!spawning && balls.Count < BALL_COUNT) {
                StartCoroutine(SpawnBall());
            }
        }

        IEnumerator SpawnBall(bool intro = false) {
            if (!intro) {
                spawning = true;
            }
            var pos = new Vector3(target.position.x, target.position.y + spawnRadius);
            var angle = ANGLE_INC * curAngleInc;
            var ball = GameObject.Instantiate(ballPrefab, pos, Quaternion.identity);
            ball.transform.RotateAround(target.position, Vector3.forward, angle);
            var pb = ball.GetComponent<PoolBall>();
            if (pb != null) {
                pb.Target = target;
                pb.poolGame = this;
                pb.chase = !intro;
            }
            var sprite = ball.GetComponent<SpriteRenderer>();
            if (sprite != null) {
                sprite.sprite = ballSprites[curSprite];
            }
            balls.Add(ball);

            curAngleInc = (curAngleInc + 1) % ANGLE_COUNT;
            curSprite = (curSprite + 1) % ballSprites.Length;
            yield return new WaitForSeconds(intro ? .2f : 1);
            if (!intro) {
                spawning = false;
            }
        }

        IEnumerator StartPool() {
            disableMovement.enabled = true;
            poolControls.disableShoot = true;
            audio.clip = startNoise;
            audio.Play();
            yield return new WaitForSeconds(2);

            // Create balls in a circle
            for (int i = 0; i < ANGLE_COUNT; i++) {
                yield return StartCoroutine(SpawnBall(true));
            }

            yield return new WaitForSeconds(1);
            foreach(var ball in balls) {
                var pb = ball.GetComponent<PoolBall>();
                if (pb != null) {
                    pb.chase = true;
                }
            }

            disableMovement.enabled = false;
            poolControls.disableShoot = false;
            spawning = false;
        }

        IEnumerator EndPool() {
            spawning = true;
            disableMovement.enabled = true;

            audio.clip = endNoise;
            audio.Play();
            inventory.Add(prize);
            yield return new WaitForSeconds(1);

            foreach (var ball in balls) {
                GameObject.Destroy(ball.gameObject, .2f);
            }

            var obj = GameObject.Instantiate(textboxPrefab);
            var textbox = obj.GetComponent<Textbox>();
            textbox.text = victoryText;

            disableMovement.enabled = false;
        }
    }
}
