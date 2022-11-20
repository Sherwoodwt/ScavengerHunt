using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scripts.Inspectables;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts {
    public class DisplayInventory : MonoBehaviour {
        public Sprite defaultImage;
        public InventoryObject inventory;

        GameObject[] slots;
        int start = 0;
        readonly int count = 7;
        readonly string slotTag = "Slot";

        public void Add(ItemObject item) {
            if (!inventory.items.Contains(item)) {
                inventory.items.Add(item);
                RefreshDisplay();
            }
        }

        public void CycleRight() {
            if (inventory.items.Count > count) {
                start = Mathf.Min(start + count, inventory.items.Count - count);
                RefreshDisplay();
            }
        }

        public void CycleLeft() {
            if (inventory.items.Count > count) {
                start = Mathf.Max(0, start - count);
                RefreshDisplay();
            }
        }

        void Start() {
            slots = GameObject
                .FindGameObjectsWithTag(slotTag)
                .OrderBy(s => s.tag)
                .ToArray();
            RefreshDisplay();
        }

        void RefreshDisplay() {
            int i = start;
            foreach (var slot in slots) {
                var image = slot.GetComponent<Image>();
                if (image == null)
                    throw new MissingComponentException($"object {slot.name} needs an Image component");
                var talkable = slot.GetComponent<Talkable>();
                if (talkable == null)
                    throw new MissingComponentException($"object {slot.name} needs a Talkable component");

                if (i < inventory.items.Count) {
                    var item = inventory.items[i];
                    if (item != null) {
                        image.sprite = item.sprite;
                        talkable.texts = new List<string> { item.Description };
                    } else {
                        image.sprite = defaultImage;
                        talkable.texts.Clear();
                    }
                }
                i++;
            }
        }
    }
}
