using UnityEngine;

namespace Scripts.Inspectables {
    public class Playable : MonoBehaviour, Inspectable {
        public ItemObject key;
        public InventoryObject inventory;
        
        new AudioSource audio;

        void Start() {
            audio = GetComponent<AudioSource>();
        }

        public virtual void Inspect() {
            if (key != null && inventory != null) {
                if (inventory.Contains(key)) {
                    audio.Play();
                }
            } else {
                audio.Play();
            }
        }
    }
}