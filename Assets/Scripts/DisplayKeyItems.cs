using UnityEngine;

namespace Scripts {
    public class DisplayKeyItems : MonoBehaviour {
        public KeyItemsObject keyItems;
        public ToggleItem shoeToggle, translatorToggle;

        void Start() {
            if (keyItems.shoes != null) {
                shoeToggle.keyItem = keyItems.shoes;
            }
            if (keyItems.translator != null) {
                translatorToggle.keyItem = keyItems.translator;
            }
        }
    }
}
