using UnityEngine;

namespace Scripts.Inspectables {
    [RequireComponent(typeof(AudioSource))]
    public class Grabbable : MonoBehaviour, Inspectable {
        public InventoryObject inventory;
        public ItemObject item;
        public ItemObject key;

        new AudioSource audio;

        void Start() {
            audio = GetComponent<AudioSource>();
            if (item == null)
                throw new MissingComponentException("Grabbable needs an item to give the player");
        }

        public void Inspect() {
            if (key == null || inventory.Contains(key)) {
                inventory.Add(item);
                Debug.Log($"Adding Item: {item.description}");
                audio.Play();
            }
        }
    }
}
