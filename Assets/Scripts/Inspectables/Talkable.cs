using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Inspectables {
    public class Talkable : MonoBehaviour, Inspectable {
        public GameObject textboxPrefab;
        [TextArea(2, 20)]
        public List<string> texts;

        [Header("Lock Stuff")]
        public ItemObject key;
        public InventoryObject inventory;
        [TextArea()]
        public string unlockedText;

        int textIndex = 0;
        Textbox textbox;

        void Start() {
            if (textboxPrefab == null)
                throw new MissingComponentException("Needs a textbox prefab");
        }

        public void Inspect() {
            if (textbox == null) {
                if (key != null && inventory != null && inventory.Contains(key) && !string.IsNullOrEmpty(unlockedText)) {
                    // unlocked
                    var obj = GameObject.Instantiate(textboxPrefab);
                    textbox = obj.GetComponent<Textbox>();
                    textbox.text = unlockedText;
                } else if (texts.Count > 0) {
                    // locked / no key required
                    var obj = GameObject.Instantiate(textboxPrefab);
                    textbox = obj.GetComponent<Textbox>();
                    textbox.text = texts[textIndex];
                    textIndex = (textIndex + 1) % texts.Count;
                }
            }
        }
    }
}
