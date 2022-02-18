using UnityEngine;

namespace Scripts.Inspectables {
    /// <summary>
    /// Sort of like an inspectable knockoff but for just collisions idk why I put a pogo game in this stupid game.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class Slurpable : MonoBehaviour {
        public InventoryObject inventory;
        public ItemObject item;

        new AudioSource audio;

        void Start() {
            audio = GetComponent<AudioSource>();
            if (item == null)
                throw new MissingComponentException("Grabbable needs an item to give the player");
        }

        void OnTriggerEnter2D (Collider2D collider) {
            if (collider.tag == "Player") {
                inventory.Add(item);
                Debug.Log($"Adding Item: {item.description}");
                audio.Play();
            }
        }
    }
}
