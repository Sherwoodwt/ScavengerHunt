using System.Collections;
using System.Collections.Generic;
using Scripts.Inspectables;
using Scripts.Movement;
using Scripts.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Pool {
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(DisableMovement))]
    public class PoolGame : MonoBehaviour {
        public GameObject ballPrefab, textboxPrefab;
        public Transform target;
        public PoolControls poolControls;
        public float spawnRadius;
        public AudioClip startNoise, endNoise, killSound, doubleTime;
        public InventoryObject inventory;
        public ItemObject prize;
        public Sprite[] ballSprites;
        [TextArea()]
        public string victoryText;

        new AudioSource audio;
        DisableMovement disableMovement;
        [SerializeField] List<GameObject> balls;
        Textbox textbox;

        readonly int WIN_SCORE = 6, ANGLE_COUNT = 9, BALL_COUNT = 30;
        readonly float ANGLE_INC = 360f / 9;
        [SerializeField] int score;
        int curAngleInc, curSprite;
        [SerializeField] bool waiting, running;

        public void AddScore() {
            score++;
            if (score == WIN_SCORE) {
                StartCoroutine(EndPool());
            }
        }

        public void Kill(GameObject obj) {
            StartCoroutine(KillPlayer(obj));
        }

        public void RemoveBall(GameObject ball) {
            if (balls.Contains(ball)) {
                balls.Remove(ball);
            }
        }

        public void ExitPool() {
            if (score == WIN_SCORE) {
                inventory.Add(prize);
            }
        }

        void Start() {
            balls = new List<GameObject>();
            audio = GetComponent<AudioSource>();
            disableMovement = GetComponent<DisableMovement>();
            textbox = TextboxUtils.Init();
        }

        void Update() {
            if (score == WIN_SCORE && !waiting && !running && balls.Count < BALL_COUNT) {
                StartCoroutine(SpawnBall());
            }
        }

        IEnumerator KillPlayer(GameObject obj) {
            obj.SetActive(false);
            audio.clip = killSound;
            audio.Play();

            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }

        IEnumerator SpawnBall(bool intro = false) {
            running = true;
            var pos = new Vector3(target.position.x, target.position.y + spawnRadius);
            var angle = ANGLE_INC * curAngleInc;
            var ball = Instantiate(ballPrefab, pos, Quaternion.identity);
            ball.transform.RotateAround(target.position, Vector3.forward, angle);
            var pb = ball.GetComponent<PoolBall>();
            if (pb != null) {
                pb.Target = target;
                pb.poolGame = this;
                pb.chase = !intro;
            }
            var killPlayer = ball.GetComponent<PoolKillPlayer>();
            killPlayer.poolGame = this;
            var sprite = ball.GetComponent<SpriteRenderer>();
            if (sprite != null) {
                sprite.sprite = ballSprites[curSprite];
            }
            balls.Add(ball);

            curAngleInc = (curAngleInc + 1) % ANGLE_COUNT;
            curSprite = (curSprite + 1) % ballSprites.Length;
            yield return new WaitForSeconds(intro ? .2f : 1);
            running = false;
        }

        IEnumerator SpawnCircle() {
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
        }

        IEnumerator EndPool() {
            waiting = true;
            disableMovement.enabled = true;
            poolControls.disableShoot = true;

            audio.clip = startNoise;
            audio.Play();

            var obj = Instantiate(textboxPrefab);
            textbox.text = victoryText;
            textbox.gameObject.SetActive(true);

            yield return new WaitForSeconds(2);
            audio.clip = doubleTime;
            audio.Play();
            yield return StartCoroutine(SpawnCircle());
            disableMovement.enabled = false;
            poolControls.disableShoot = false;
            waiting = false;
        }
    }
}
