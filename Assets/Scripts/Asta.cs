using System.Collections;
using Scripts.Inspectables;
using Scripts.Movement;
using UnityEngine;

namespace Scripts {
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Chase))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Talkable))]
    public class Asta : MonoBehaviour {
        public float distance;
        public Sprite sleepingSprite;
        public ItemObject key;
        public InventoryObject inventory;
        public string borkTrigger = "Bork";

        Animator animator;
        new AudioSource audio;
        new BoxCollider2D collider;
        SpriteRenderer sprite;
        Chase chase;
        Talkable talkable;
        

        void Start() {
            animator = GetComponent<Animator>();
            audio = GetComponent<AudioSource>();
            collider = GetComponent<BoxCollider2D>();
            chase = GetComponent<Chase>();
            sprite = GetComponent<SpriteRenderer>();
            talkable = GetComponent<Talkable>();

            if (!inventory.Contains(key)) {
                sprite.enabled = false;
                chase.enabled = false;
            } else {
                StartCoroutine(Greet());
            }
        }

        void FixedUpdate() {
            if (!inventory.Contains(key)) {
                return;
            }

            if (chase.enabled) {
                var distance = chase.target.transform.position - transform.position;
                if (distance.magnitude < .5f) {
                    talkable.Inspect();
                    chase.enabled = false;
                }
            }
        }

        IEnumerator Greet() {
            animator.SetTrigger(borkTrigger);
            audio.Play();
            yield return new WaitForSeconds(2);

            animator.ResetTrigger(borkTrigger);
            chase.enabled = true;
        }
    }
}
