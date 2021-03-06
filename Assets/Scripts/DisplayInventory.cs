using System.Collections;
using Scripts.Inspectables;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts {
    public class DisplayInventory : MonoBehaviour {
        public Sprite defaultImage;
        public InventoryObject inventory;

        GameObject[] slots;
        int start = 0;
        int prevStart = -1;
        int prevCount = 0;
        readonly int count = 7;
        readonly string slotTag = "Slot";

        public void Add(ItemObject item) {
            if (!inventory.items.Contains(item)) {
                inventory.items.Add(item);
            }
        }

        public void CycleLeft() {
            if (inventory.items.Count > count)
                start = Mathf.Min(start + count, inventory.items.Count - count);
        }

        public void CycleRight() {
            if (inventory.items.Count > count)
                start = Mathf.Max(0, start - count);
        }

        void Start() {
            slots = GameObject.FindGameObjectsWithTag(slotTag);
            // sort by slot name
            for (int i = 0; i < slots.Length; i++) {
                var first = i;
                for (int j = i+1; j < slots.Length; j++) {
                    if (slots[j].name.CompareTo(slots[first].name) < 0)
                        first = j;
                }
                var temp = slots[i];
                slots[i] = slots[first];
                slots[first] = temp;
            }
        }

        void Update() {
            if (start != prevStart || inventory.items.Count != prevCount) {
                prevStart = start;
                prevCount = inventory.items.Count;
                StartCoroutine(RefreshDisplay());
            }
        }

        IEnumerator RefreshDisplay() {
            int i = start;
            foreach (var slot in slots) {
                var image = slot.GetComponent<Image>();
                if (image == null)
                    throw new MissingComponentException($"object {slot.name} needs an Image component");
                var inspectable = slot.GetComponent<Talkable>();
                if (inspectable == null)
                    throw new MissingComponentException($"object {slot.name} needs a Talkable component");

                if (i < inventory.items.Count) {
                    var item = inventory.items[i];
                    if (item != null) {
                        image.sprite = item.sprite;
                        inspectable.texts.Add(item.Description);
                    } else {
                        image.sprite = defaultImage;
                        inspectable.texts.Clear();
                    }
                }

                yield return i;
                i++;
            }
        }
    }
}
