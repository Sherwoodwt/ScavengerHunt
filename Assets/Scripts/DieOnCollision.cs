using System.Collections;
using Scripts.Movement;
using Scripts.Pool;
using UnityEngine;

namespace Scripts {
    public class DieOnCollision : MonoBehaviour {
        public Sprite deadSprite;

        SpriteRenderer spriteRenderer;
        new AudioSource audio;

        void Start() {
            spriteRenderer = GetComponent<SpriteRenderer>();
            audio = GetComponent<AudioSource>();
        }

        void OnTriggerEnter2D(Collider2D collider) {
            if (audio != null) {
                audio.Play();
            }

            if (spriteRenderer != null) {
                spriteRenderer.sprite = deadSprite;
                var animator = GetComponent<Animator>();
                animator.enabled = false;
            }

            var gun = GetComponentInParent<Gun>();
            if (gun != null)
                gun.enabled = false;
            var move = GetComponent<PathMovement>();
            if (move != null)
                move.enabled = false;

            GameObject.Destroy(this.gameObject, 1);
        }
    }
}
