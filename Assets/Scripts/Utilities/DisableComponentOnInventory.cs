using UnityEngine;

namespace Scripts.Utilities {
    public class DisableComponentOnInventory : MonoBehaviour {
        public InventoryObject inventory;
        public ItemObject key;
        public Behaviour[] components;

        void Update() {
            if (inventory.Contains(key)) {
                foreach (var comp in components) {
                    comp.enabled = false;
                }
            }
        }
    }
}
