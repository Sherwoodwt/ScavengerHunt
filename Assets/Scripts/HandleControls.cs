using Scripts.Inspectables;
using UnityEngine;

namespace Scripts {
    [RequireComponent(typeof(CharacterMovement))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class HandleControls : MonoBehaviour {
        public GameObject pauseMenuPrefab;
        public Animator animator;
        public LayerMask inspectMask;

        GameObject pauseMenu;
        Vector2 direction;
        CharacterMovement character;
        new BoxCollider2D collider;

        public void Pause() {
            pauseMenu = GameObject.Instantiate(pauseMenuPrefab);
        }

        void Start() {
            character = GetComponent<CharacterMovement>();
            collider = GetComponent<BoxCollider2D>();
        }

        void Update()
        {
            // ONLY RUN IF NOT PAUSED
            if (pauseMenu != null)
                return;

            if (Input.GetKeyDown(KeyCode.Escape))
                Pause();

            // y inputs
            if (Input.GetKey(KeyCode.S)) {
                character.input.y = -1;
            } else if (Input.GetKey(KeyCode.W)) {
                character.input.y = 1;
            } else {
                character.input.y = 0;
            }
            if (animator.GetFloat("YVel") != character.input.y)
                animator.SetFloat("YVel", character.input.y);

            // x inputs
            if (Input.GetKey(KeyCode.A)) {
                character.input.x = -1;
            } else if (Input.GetKey(KeyCode.D)) {
                character.input.x = 1;
            } else {
                character.input.x = 0;
            }
            if (animator.GetFloat("XVel") != character.input.x)
                animator.SetFloat("XVel", character.input.x);

            // Set direction to input if input has magnitude. This prevents having no direction, which would break inspecting.
            if (character.input.magnitude > 0)
                direction = character.input;

            // handle inspect
            if (Input.GetKeyDown(KeyCode.Space)) {
                var displacement = ((Vector2)collider.bounds.size / 2 * direction) + (.1f * direction);
                var origin = ((Vector2)collider.bounds.center) + displacement;
                var size = Vector2.one * direction;
                var hit = Physics2D.Raycast(origin, direction, size.magnitude, inspectMask);
                Debug.DrawRay(origin, direction, Color.blue, .5f);
                if (hit.collider != null) {
                    var items = hit.collider.gameObject.GetComponents<Inspectable>();
                    foreach (var item in items)
                        item.Inspect();
                }
            }
        }
    }
}
