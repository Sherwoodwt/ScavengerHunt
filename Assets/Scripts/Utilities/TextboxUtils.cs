using UnityEngine;
using Scripts.Inspectables;

namespace Scripts.Utilities {
    public static class TextboxUtils {
        static string textboxTag = "Textbox";

        // Setup reference to Textbox
        public static Textbox Init() {
            var parent = GameObject.FindGameObjectWithTag(textboxTag);
            var textbox = parent.GetComponentInChildren<Textbox>(true);
            return textbox;
        }
    }
}
