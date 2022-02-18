using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Inspectables {
    public class Talkable : MonoBehaviour, Inspectable {
        public GameObject textboxPrefab;
        [TextArea(2,6)]
        public List<string> texts;

        [Header("Lock Stuff")]
        public ItemObject key;
        public InventoryObject inventory;
        [TextArea(2,6)]
        public string unlockedText;

        int textIndex = 0;
        Textbox textbox;

        void Start() {
            if (textboxPrefab == null)
                throw new MissingComponentException("Needs a textbox prefab");
        }

        public virtual void Inspect() {
            if (key != null && inventory != null && inventory.Contains(key) && !string.IsNullOrEmpty(unlockedText)) {
                var obj = GameObject.Instantiate(textboxPrefab);
                textbox = obj.GetComponent<Textbox>();
                textbox.text = unlockedText;
            } else if (textbox == null && texts.Count > 0) {
                var obj = GameObject.Instantiate(textboxPrefab);
                textbox = obj.GetComponent<Textbox>();
                textbox.text = texts[textIndex];
                textIndex = (textIndex + 1) % texts.Count;
            }
        }
    }
}
