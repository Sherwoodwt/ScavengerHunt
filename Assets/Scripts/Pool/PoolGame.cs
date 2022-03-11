using System.Collections;
using UnityEngine;

namespace Scripts.Pool {
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(DisableMovement))]
    public class PoolGame : MonoBehaviour {
        public GameObject ballPrefab;
        public Transform target;
        public Texture2D cursorSprite;
        public Sprite[] ballSprites;
        public AudioClip startNoise;
        public float spawnRadius;

        new AudioSource audio;
        DisableMovement disableMovement;
        [SerializeField] GameObject[] balls;
        int score;
        readonly int VICTORY_SCORE = 6;

        public void AddScore() {
            score++;
        }

        void Start() {
            Cursor.SetCursor(cursorSprite, Vector2.zero, CursorMode.Auto);
            audio = GetComponent<AudioSource>();
            disableMovement = GetComponent<DisableMovement>();

            score = 0;
            StartCoroutine(PlayPool());
        }

        IEnumerator PlayPool() {
            yield return StartCoroutine(StartPool());

        }

        IEnumerator StartPool() {
            disableMovement.enabled = true;
            audio.clip = startNoise;
            audio.Play();
            yield return new WaitForSeconds(2);

            // Create balls in a circle
            balls = new GameObject[ballSprites.Length];
            float angleInc = 360f / ballSprites.Length;
            for (int i = 0; i < balls.Length; i++) {
                var pos = new Vector3(target.position.x, target.position.y + spawnRadius);
                var angle = angleInc * i;
                balls[i] = GameObject.Instantiate(ballPrefab, pos, Quaternion.identity);
                balls[i].transform.RotateAround(target.position, Vector3.forward, angle);
                var pb = balls[i].GetComponent<PoolBall>();
                if (pb != null) {
                    pb.Target = target;
                }
                var sprite = balls[i].GetComponent<SpriteRenderer>();
                if (sprite != null) {
                    sprite.sprite = ballSprites[i%ballSprites.Length];
                }
                yield return new WaitForSeconds(.2f);
            }

            yield return new WaitForSeconds(1);
            foreach(var ball in balls) {
                var pb = ball.GetComponent<PoolBall>();
                if (pb != null) {
                    pb.chase = true;
                }
            }

            disableMovement.enabled = false;
        }
    }
}
