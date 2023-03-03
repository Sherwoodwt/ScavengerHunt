using System.Collections;
using UnityEngine;

namespace Scripts.Movement {
    [RequireComponent(typeof(NormalPhysics))]
    /// <summary>
    /// Used for characters who are supposed to walk in random directions occassionally, like pokemon/zelda
    /// </summary>
    public class RandomMovement : MonoBehaviour, Influencable {
        [Range(0, .01f)]
        public float stirCraziness;
        public bool disableX;
        public bool disableY;

        Animator animator;
        NormalPhysics physics;

        void Start() {
            animator = GetComponent<Animator>();
            physics = GetComponent<NormalPhysics>();
        }

        void Update() {
            if (physics.enabled &&
                physics.input.magnitude == 0 &&
                Random.value < stirCraziness
            ) {
                StartCoroutine(Move());
            }
        }

        IEnumerator Move() {
            var dir = Random.Range(0, 4);
            if (!disableX) {
                if (dir == 0)
                    physics.input = new Vector2(-1, 0);
                if (dir == 1)
                    physics.input = new Vector2(1, 0);
            }
            if (!disableY) {
                if (dir == 2)
                    physics.input = new Vector2(0, -1);
                if (dir == 3)
                    physics.input = new Vector2(0, 1);
            }
            if (animator != null) {
                animator.SetFloat("YVel", physics.input.y);
                animator.SetFloat("XVel", physics.input.x);
            }

            yield return new WaitForSeconds(Random.Range(.5f, 2));
            physics.input = new Vector2(0, 0);
            if (animator != null) {
                animator.SetFloat("YVel", 0);
                animator.SetFloat("XVel", 0);
            }
        }

        public void SetFocus(Vector3 playerPos) {
            physics.enabled = false;
            if (animator != null) {
                var dif = playerPos - transform.position;
                var xVal = Mathf.Clamp(dif.x, -1, 1);
                var yVal = Mathf.Clamp(dif.y, -1, 1);
                if (Mathf.Abs(xVal) >= Mathf.Abs(yVal)) {
                    animator.SetFloat("XVel", Mathf.FloorToInt(xVal));
                } else {
                    animator.SetFloat("YVel", Mathf.FloorToInt(yVal));
                }
            }
        }

        public void DisableFocus() {
            physics.enabled = true;
        }
    }
}
