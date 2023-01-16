using System.Collections;
using Scripts.Movement;
using UnityEngine;

namespace Scripts {
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Chase))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Asta : MonoBehaviour {
        public float distance;
        public Transform spawnpoint;
        public Sprite sleepingSprite;
        public string borkTrigger;
        public LayerMask collisionMask;

        Animator animator;
        new AudioSource audio;
        new BoxCollider2D collider;
        SpriteRenderer sprite;
        Chase chase;
        

        void Start() {
            animator = GetComponent<Animator>();
            audio = GetComponent<AudioSource>();
            collider = GetComponent<BoxCollider2D>();
            chase = GetComponent<Chase>();
            sprite = GetComponent<SpriteRenderer>();
            transform.position = spawnpoint.position;

            StartCoroutine(Greet());
        }

        void Update() {
            if (chase.target != null) {
                var dist = (transform.position - chase.target.position).magnitude;
                if (dist < distance) {
                    if (chase.target == spawnpoint) {
                        animator.enabled = false;
                        sprite.sprite = sleepingSprite;
                    } else {
                        StartCoroutine(Return());
                    }
                }
            }
        }

        IEnumerator Greet() {
            animator.SetTrigger(borkTrigger);
            yield return new WaitForSeconds(.8f);
            audio.Play();
            yield return new WaitForSeconds(2);

            animator.ResetTrigger(borkTrigger);
            chase.enabled = true;
        }

        IEnumerator Return() {
            chase.target = null;
            animator.SetTrigger(borkTrigger);
            yield return new WaitForSeconds(.8f);
            audio.Play();
            yield return new WaitForSeconds(2);

            animator.ResetTrigger(borkTrigger);
            chase.target = spawnpoint;
            chase.speed = chase.speed / 2f;
        }
    }
}
