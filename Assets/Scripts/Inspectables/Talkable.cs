using System.Collections.Generic;
using UnityEngine;
using Scripts.Utilities;
using Scripts.Movement;

namespace Scripts.Inspectables {
    public class Talkable : Inspectable {
        public KeyItemObject translator;
        [TextArea()]
        public List<string> texts, translatedTexts;

        [TextArea()]
        public string successText, incorrectText;
        public bool killTalker = false;
        Textbox textbox;

        int textIndex = 0;

        void OnEnable() {
            textbox = TextboxUtils.Init();
        }

        void ActivateTextbox() {
            textbox.talker = this.gameObject;
            if (!textbox.gameObject.activeSelf && texts.Count > 0) {
                textbox.gameObject.SetActive(true);
                var tb = textbox.GetComponent<Textbox>();
                tb.killTalker = killTalker;
            }
        }

        public override void CorrectResponse() {
            if (!string.IsNullOrEmpty(successText)) {
                textbox.text = successText;
                ActivateTextbox();
            } else {
                base.CorrectResponse();
            }
        }

        public override void IncorrectResponse() {
            if (!string.IsNullOrEmpty(incorrectText)) {
                textbox.text = incorrectText;
                ActivateTextbox();
            } else {
                base.IncorrectResponse();
            }
        }

        public override void NoItemResponse() {
            var curTexts = texts;
            if (translatedTexts.Count > 0 && translator.Active) {
                curTexts = translatedTexts;
            }
            if (curTexts.Count > 0) {
                textbox.text = curTexts[textIndex];
                textIndex = (textIndex + 1) % curTexts.Count;
                ActivateTextbox();
            }
        }
    }
}
