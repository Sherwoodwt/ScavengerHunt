using System.Collections.Generic;
using System.Linq;
using Scripts.Inspectables;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts {
    public class DisplayInventory : MonoBehaviour {
        public Sprite defaultSlotImage;
        public InventoryObject inventory;

        GameObject[] slots;
        string oldHash;
        readonly string slotTag = "Slot";

        void Start() {
            slots = GameObject
                .FindGameObjectsWithTag(slotTag)
                .OrderBy(s => s.tag)
                .ToArray();
            oldHash = HashInventory();
            RefreshDisplay();
        }

        void FixedUpdate() {
            var hash = HashInventory();

            if (hash.ToString() != oldHash) {
                oldHash = hash;
                RefreshDisplay();
            }
        }

        void RefreshDisplay() {
            for (int i = 0; i < inventory.items.Length; i++) {
                var item = inventory.items[i];
                var image = slots[i].GetComponent<Image>();
                if (item == null){
                    image.sprite = null;
                } else {
                    image.sprite = item.sprite;
                }
            }
        }

        string HashInventory() {
            var hash = new Hash128();
            foreach (var item in inventory.items) {
                if (item != null)
                    hash.Append(item.name);
                else
                    hash.Append("empty");
            }
            return hash.ToString();
        }
    }
}
