using System.Collections.Generic;
using Scripts.Inspectables;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts {
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(Talkable))]
    public class Slot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {
        public InventoryObject inventory;

        int index;
        Image image;
        RectTransform rect;
        Vector2 startPosition;
        Button button;
        Canvas canvas;
        Talkable talkable;

        public ItemObject Item {
            get { return inventory.items[index]; }
            set { inventory.items[index] = value; }
        }

        void OnEnable() {
            image = GetComponent<Image>();
            rect = GetComponent<RectTransform>();
            button = GetComponent<Button>();
            canvas = GetComponent<Canvas>();
            talkable = GetComponent<Talkable>();
            startPosition = rect.anchoredPosition;
            index = int.Parse(name.Split("t")[1]); // ._.

            Refresh();
        }

        public void Description() {
            talkable.texts = new List<string>{ Item.description };
            talkable.Inspect();
        }

        public void Refresh() {
            image.sprite = inventory.items[index]?.sprite;
            if (image.sprite == null) {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
            } else {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
            }
        }

        public void OnBeginDrag(PointerEventData eventData) {
            image.raycastTarget = false;
            canvas.overrideSorting = true;
            canvas.sortingOrder = 20;
            if (button != null) {
                button.enabled = false;
            }
        }

        public void OnDrag(PointerEventData eventData) {
            rect.anchoredPosition += eventData.delta;
        }

        public void OnEndDrag(PointerEventData eventData) {
            image.raycastTarget = true;
            canvas.overrideSorting = false;
            rect.anchoredPosition = startPosition;
            if (button != null) {
                button.enabled = true;
            }

            var otherObj = eventData.pointerCurrentRaycast.gameObject;
            var otherSlot = otherObj?.GetComponent<Slot>();

            // Swap places
            if (otherObj != null && otherSlot != null) {
                var temp = inventory.items[index];
                inventory.items[index] = inventory.items[otherSlot.index];
                inventory.items[otherSlot.index] = temp;

                Refresh();
                otherSlot.Refresh();
            }

            // Use Item
            var inspectable = otherObj?.GetComponent<Inspectable>();
            if (otherObj != null && inspectable != null) {
                // TODO: Inspect with item here
                // TODO: Update Inspectable to have base-level item possibly used
            }
        }
    }
}
