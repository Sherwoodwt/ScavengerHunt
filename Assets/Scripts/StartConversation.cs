using System.Collections;
using Scripts.Inspectables;
using UnityEngine;

namespace Scripts {
    [RequireComponent(typeof(Collider2D))]
    public class StartConversation : MonoBehaviour {
        public Talkable talkable;
        public RandomMovement movement;
        public InventoryObject inventory;
        public ItemObject dogItem;

        void Start() {
            if (inventory.Contains(dogItem)) {
                this.enabled = false;
            }
        }

        void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.tag == "Player") {
                talkable.Inspect();
                if (movement != null) {
                    movement.enabled = false;
                    StartCoroutine(Countdown());
                }
            }
        }

        IEnumerator Countdown() {
            if (movement != null) {
                yield return new WaitForSeconds(20);
                movement.enabled = true;
            }
        }
    }
}
