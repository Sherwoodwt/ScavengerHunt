using UnityEngine;
using Scripts.Utilities;
using Scripts.Movement;
using System;
using UnityEngine.UIElements;
using UnityEngine.PlayerLoop;
using System.Collections;

namespace Scripts.Basketball {
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(PathMovement))]
    [RequireComponent(typeof(Explode))]
    public class BasketballHoop : MonoBehaviour {
        public LayerMask ballLayer;
        public int goal = 8;
        public ItemObject prize;
        public InventoryObject inventory;
        public AudioClip scoreSound, missSound, prizeSound;
        public PointList[] paths;
        public float[] speeds;

        new AudioSource audio;
        PathMovement pathMovement;
        Explode explode;
        [SerializeField] int score = 0;

        public void ResetScore() {
            audio.PlayOneShot(missSound);
            score = 0;
            pathMovement.Reset();
            UpdatePath();
        }

        void Start() {
            if (paths.Length != speeds.Length)
                throw new ArgumentException("paths and speeds must be same length");

            audio = GetComponent<AudioSource>();
            explode = GetComponent<Explode>();
            pathMovement = GetComponent<PathMovement>();
            UpdatePath();
        }

        void OnTriggerEnter2D(Collider2D collider) {
            if (collider.gameObject.InLayerMask(ballLayer)) {
                score++;
                Destroy(collider.gameObject);

                if (score >= goal && prize != null) {
                    StartCoroutine(EndGame());
                } else {
                    audio.PlayOneShot(scoreSound);
                    UpdatePath();
                }
            }
        }

        void UpdatePath() {
            pathMovement.Reset();
            pathMovement.speed = speeds[score];
            pathMovement.points = paths[score].path;
            transform.position = pathMovement.points[0];
        }

        IEnumerator EndGame() {
            pathMovement.enabled = false;
            explode.TriggerExplosion();

            yield return new WaitForSeconds(1);

            audio.volume = 1;
            audio.PlayOneShot(prizeSound);
            inventory.Add(prize);
        }

        [Serializable]
        public class PointList {
            public Vector2[] path;
        }
    }
}
