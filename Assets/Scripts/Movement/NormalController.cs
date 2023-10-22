using Scripts.Inspectables;
using UnityEngine;

namespace Scripts.Movement {
    [RequireComponent(typeof(NormalPhysics))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class NormalController : BaseControls {
        public GameObject pauseMenuPrefab;
        public InventoryObject inventory;
        public KeyItemObject shoeItem;
        public Animator animator;
        public LayerMask inspectMask;

        GameObject pauseMenu;
        Vector2 direction;
        NormalPhysics physics;
        new BoxCollider2D collider;
        bool running, disableInspect;

        public void Pause(GameObject prefab) {
            pauseMenu = GameObject.Instantiate(prefab);
        }

        public bool DisableInspect {
            get { return disableInspect; }
            set { disableInspect = value; }
        }

        void Start() {
            physics = GetComponent<NormalPhysics>();
            collider = GetComponent<BoxCollider2D>();
        }

        void Update()
        {
            // If paused, DON'T RUN! WALK! SLOWLY!
            if (pauseMenu != null)
                return;

            if (Input.GetKeyDown(KeyCode.Tab))
                Pause(pauseMenuPrefab);

            var speed = running ? 1.5f : 1;
            animator.speed = running ? 1.5f : 1;
            // y inputs
            if (!DisableInputs) {
                if (Input.GetKey(KeyCode.S)) {
                    physics.input.y = -speed;
                } else if (Input.GetKey(KeyCode.W)) {
                    physics.input.y = speed;
                } else {
                    physics.input.y = 0;
                }
            }
            if (animator.GetFloat("YVel") != physics.input.y)
                animator.SetFloat("YVel", physics.input.y);

            // x inputs
            if (!DisableInputs) {
                if (Input.GetKey(KeyCode.A)) {
                    physics.input.x = -speed;
                } else if (Input.GetKey(KeyCode.D)) {
                    physics.input.x = speed;
                } else {
                    physics.input.x = 0;
                }
            }
            if (animator.GetFloat("XVel") != physics.input.x)
                animator.SetFloat("XVel", physics.input.x);

            // Set direction to input if input has magnitude. This prevents having no direction, which would break inspecting.
            if (physics.input.magnitude > 0)
                direction = physics.input;

            // handle inspect
            if (!disableInspect) {
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

            // handle run
            running = shoeItem.Active;
        }
    }
}
