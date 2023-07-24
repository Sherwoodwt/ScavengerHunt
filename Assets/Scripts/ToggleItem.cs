using UnityEngine;
using UnityEngine.UI;

namespace Scripts {
    [RequireComponent(typeof(Image))]
    public class ToggleItem : MonoBehaviour {
        public KeyCode key;
        public KeyItemObject keyItem;

        Image image;

        void Start() {
            image = GetComponent<Image>();
            image.sprite = keyItem.CurrentSprite();
        }

        void Update() {
            if (Input.GetKeyDown(key)) {
                Toggle();
            }
        }

        public void Toggle() {
            keyItem.Toggle();
            image.sprite = keyItem.CurrentSprite();
        }
    }
}
