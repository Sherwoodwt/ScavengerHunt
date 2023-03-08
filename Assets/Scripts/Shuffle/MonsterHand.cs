using System.Linq;
using Scripts.Movement;
using UnityEngine;

namespace Scripts.Shuffle {
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(PathMovement))]
    [RequireComponent(typeof(DisableMovement))]
    public class MonsterHand : MonoBehaviour {
        Collider2D handCollider;
        PathMovement movement;
        DisableMovement disableMovement;
        GameObject grabbed = null;

        void Start() {
            handCollider = GetComponent<Collider2D>();
            movement = GetComponent<PathMovement>();
            disableMovement = GetComponent<DisableMovement>();

            disableMovement.enabled = true;
        }

        void LateUpdate() {
            if (grabbed != null) {
                grabbed.transform.position = handCollider.transform.position;
            }
            if (movement.LoopCount >= 1) {
                this.gameObject.SetActive(false);
                grabbed?.gameObject?.SetActive(false);
            }
        }

        void OnTriggerEnter2D(Collider2D collider) {
            if (collider.tag == "Player") {
                grabbed = collider.gameObject;
            }
        }
    }
}
