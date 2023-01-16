using System.Collections.Generic;
using UnityEngine;
using Scripts.Utilites;
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

            // activate textbox
            if (!textbox.gameObject.activeSelf && texts.Count > 0) {
                textbox.gameObject.SetActive(true);
            }

            // TODO: 4. Make talker look at the inspector
            // If it moves, it don't move no more
            GetComponentInChildren<RandomMovement>();
            // TODO: 3. There are more movement examples here. Could it be that refactoring
            // movement to be more generally referencable would benefit me?

            // If it rotates, rotate towards the one whomst started it
        }
    }
}
