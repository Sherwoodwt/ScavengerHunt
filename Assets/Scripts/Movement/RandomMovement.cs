using System.Collections;
using UnityEngine;

namespace Scripts.Movement {
    [RequireComponent(typeof(NormalPhysics))]
    /// <summary>
    /// Used for characters who are supposed to walk in random directions occassionally, like pokemon/zelda
    /// </summary>
    public class RandomMovement : MonoBehaviour {
        [Range(0, .01f)]
        public float stirCraziness;
        public bool disableX;
        public bool disableY;

        Animator animator;
        NormalPhysics movement;

        void Start() {
            animator = GetComponent<Animator>();
            movement = GetComponent<NormalPhysics>();
        }

        void Update() {
            if (movement.input.magnitude == 0 && Random.value < stirCraziness) {
                StartCoroutine(Move());
            }
        }

        IEnumerator Move() {
            var dir = Random.Range(0, 4);
            if (!disableX) {
                if (dir == 0)
                    movement.input = new Vector2(-1, 0);
                if (dir == 1)
                    movement.input = new Vector2(1, 0);
            }
            if (!disableY) {
                if (dir == 2)
                    movement.input = new Vector2(0, -1);
                if (dir == 3)
                    movement.input = new Vector2(0, 1);
            }
            if (animator != null) {
                animator.SetFloat("YVel", movement.input.y);
                animator.SetFloat("XVel", movement.input.x);
            }

            yield return new WaitForSeconds(Random.Range(.5f, 2));
            movement.input = new Vector2(0, 0);
            if (animator != null) {
                animator.SetFloat("YVel", 0);
                animator.SetFloat("XVel", 0);
            }
        }
    }
}
