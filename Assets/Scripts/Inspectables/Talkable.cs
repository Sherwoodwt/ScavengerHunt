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

        public virtual void Inspect() {
            if (key != null && inventory != null && inventory.Contains(key) && !string.IsNullOrEmpty(unlockedText)) {
                // Lock conditions present and has key, show unlock text
                var obj = GameObject.Instantiate(textboxPrefab);
                textbox = obj.GetComponent<Textbox>();
                textbox.text = unlockedText;
            } else if (textbox == null && texts.Count > 0) {
                // Show textbox with the current text
                var obj = GameObject.Instantiate(textboxPrefab);
                textbox = obj.GetComponent<Textbox>();
                textbox.text = texts[textIndex];
                textIndex = (textIndex + 1) % texts.Count;
            }

            // If it moves, it don't move no more
            GetComponentInChildren<RandomMovement>();
            // TODO: There are more movement examples here. Could it be that refactoring
            // movement to be more generally referencable would benefit me?

            // If it rotates, rotate towards the one whomst started it
        }
    }
}
