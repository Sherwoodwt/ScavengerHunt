using UnityEngine;

namespace Scripts.SpiderTrivia {
    [RequireComponent(typeof(Teleport))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpiderCurse : MonoBehaviour {
        public InventoryObject inventory;
        public ItemObject key;

        Teleport teleport;
        SpriteRenderer spriteRenderer;

        void Start() {
            teleport = GetComponent<Teleport>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            if (!inventory.Contains(key)) {
                teleport.enabled = false;
                var oldColor = spriteRenderer.color;
                oldColor.a = .5f;
                spriteRenderer.color = oldColor;
            }
        }
    }
}
