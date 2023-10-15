using UnityEngine;

namespace Scripts.Inspectables {
    [RequireComponent(typeof(AudioSource))]
    public class Grabbable : Inspectable {
        public InventoryObject inventory;
        public KeyItemsObject keyItems;
        public ItemObject item;

        protected new AudioSource audio;

        void Start() {
            audio = GetComponent<AudioSource>();
            if (item == null)
                throw new MissingComponentException("Grabbable needs an item to give the player");
        }

        public override void NoItemResponse() {
            if (key != null) {
                return;
            }

            if (item.GetType() == typeof(KeyItemObject)) {
                var keyItem = (KeyItemObject)item;
                if (keyItem.name == "Shoe") {
                    keyItems.shoes = keyItem;
                } else if (keyItem.name == "Translator") {
                    keyItems.translator = keyItem;
                }
            } else if (!inventory.Contains(item)) {
                inventory.Add(item);
            }
        }

        public override void CorrectResponse() {
            if (!inventory.Contains(item)) {
                inventory.Add(item);
                if (audio != null) {
                    audio.Play();
                }
            }
        }
    }
}
