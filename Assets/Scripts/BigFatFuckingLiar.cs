using System.Collections.Generic;
using Scripts.Inspectables;
using Scripts.Utilities;
using UnityEngine;

namespace Scripts {
    [RequireComponent(typeof(Talkable))]
    public class BigFatFuckingLiar : Inspectable {
        public GameObject paulGiamattiPrefab, poofPrefab;
        public GameObject disguiseObject;
        public KeyItemsObject keyItems;
        [TextArea]
        public string overrideText;

        Textbox textbox;
        bool watching;
        Talkable talkable;

        void Start() {
            textbox = TextboxUtils.Init();
            talkable = GetComponent<Talkable>();
        }

        void Update() {
            if (watching && !textbox.gameObject.activeSelf) {
                GameObject.Instantiate(poofPrefab);
                GameObject.Instantiate(paulGiamattiPrefab);
                disguiseObject.SetActive(false);
                GameObject.Destroy(this);

                talkable.translatedTexts = new List<string>() { overrideText };
            }
        }

        public override void NoItemResponse() {
            if (keyItems.translator != null && keyItems.translator.Active) {
                watching = true;
            }
        }
    }
}
