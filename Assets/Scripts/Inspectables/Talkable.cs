using System.Collections.Generic;
using UnityEngine;
using Scripts.Utilities;
using Scripts.Movement;

namespace Scripts.Inspectables {
    public class Talkable : MonoBehaviour, Inspectable {
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
            textbox = TextboxUtils.Init();
        }

        public virtual void Inspect() {
            // check for lock to determine which text to use
            if (key != null && inventory != null && inventory.Contains(key) && !string.IsNullOrEmpty(unlockedText)) {
                textbox.text = unlockedText;

            } else {
                textbox.text = texts[textIndex];
                textIndex = (textIndex + 1) % texts.Count;
            }
            textbox.talker = this.gameObject;

            // activate textbox
            if (!textbox.gameObject.activeSelf && texts.Count > 0) {
                textbox.gameObject.SetActive(true);
            }
        }
    }
}
