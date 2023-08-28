using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts {
    [RequireComponent(typeof(Image))]
    public class ToggleItem : MonoBehaviour {
        public KeyCode key;
        public KeyItemsObject keyItems;
        public KeyItemObject keyItem;

        Image image;

        void Start() {
            image = GetComponent<Image>();
            if (keyItem != null) {
                image.sprite = keyItem.CurrentSprite();
            }
        }

        void Update() {
            if (keyItems.Contains(keyItem)) {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
                if (Input.GetKeyDown(key)) {
                    Toggle();
                }
            } else {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
            }
        }

        public void Toggle() {
            keyItem.Toggle();
            image.sprite = keyItem.CurrentSprite();
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
