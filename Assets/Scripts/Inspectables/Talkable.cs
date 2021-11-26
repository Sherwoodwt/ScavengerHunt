using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Inspectables {
    public class Talkable : MonoBehaviour, Inspectable {
        public GameObject textboxPrefab;

        [TextArea(2,6)]
        public List<string> texts;
        
        int textIndex = 0;
        Textbox textbox;

        void Start() {
            if (textbox == null) {
                throw new MissingComponentException("Prefab needs a textbox component");
            }
        }

        public virtual void Inspect() {
            if (textbox == null && texts.Count > 0) {
                var obj = GameObject.Instantiate(textboxPrefab);
                textbox = obj.GetComponent<Textbox>();
                textbox.text = texts[textIndex];
                textIndex = (textIndex + 1) % texts.Count;
            }
        }
    }
}
