using Scripts.Utilities;
using UnityEngine;

namespace Scripts {
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(PathMovement))]
    [RequireComponent(typeof(DisableMovement))]
    public class MonsterHand : MonoBehaviour {
        Collider2D handCollider;
        PathMovement movement;
        DisableMovement disableMovement;
        new AudioSource audio;
        GameObject grabbed = null;
        // shameless hack
        [SerializeField] bool first;

        void Start() {
            audio = GetComponent<AudioSource>();
            handCollider = GetComponent<Collider2D>();
            movement = GetComponent<PathMovement>();
            disableMovement = GetComponent<DisableMovement>();
            disableMovement.enabled = false;
            first = true;
        }

        void LateUpdate() {
            if (grabbed != null) {
                grabbed.transform.position = handCollider.transform.position;
            }
            if (!first) {
                if ((Vector2)transform.position == movement.points[0]) {
                    GameObject.Destroy(gameObject);
                }
            } else {
                first = false;
            }
        }

        void OnTriggerEnter2D(Collider2D collider) {
            if (collider.tag == "Player") {
                disableMovement.enabled = true;
                grabbed = collider.gameObject;
            }
        }
    }
}
