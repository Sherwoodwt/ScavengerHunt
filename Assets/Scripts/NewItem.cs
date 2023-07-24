using UnityEngine;
using UnityEngine.UI;

namespace Scripts {
    public class NewItem : MonoBehaviour {
        public InventoryObject inventory;
        public Sprite normalSprite, newItemSprite;

        Image image;
        int oldCount;

        void Start() {
            image = GetComponent<Image>();
            Reset();
        }

        void OnEnable() {
            Reset();
        }

        void Update() {
            if (oldCount != inventory.Count()) {
                image.sprite = newItemSprite;
            }
        }

        void Reset() {
            oldCount = inventory.Count();
            if (image != null)
                image.sprite = normalSprite;
        }
    }
}
