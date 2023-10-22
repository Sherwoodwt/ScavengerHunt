using Scripts.Movement;
using UnityEngine;

namespace Scripts.Utilities {
    [RequireComponent(typeof(Animator))]
    public class Explode : MonoBehaviour {
        new AudioSource audio;
        Animator animator;

        public AudioClip dieSound;
        public Animation explosion;
        public float destroyTime = .75f;

        void Start() {
            audio = GetComponent<AudioSource>();
            animator = GetComponent<Animator>();
        }

        public void TriggerExplosion() {
            if (audio != null) {
                audio.clip = dieSound;
                audio.Play();
            }
            animator.enabled = true;
            animator.SetTrigger("Splode");
            Destroy(this.gameObject, destroyTime);
        }
    }
}
