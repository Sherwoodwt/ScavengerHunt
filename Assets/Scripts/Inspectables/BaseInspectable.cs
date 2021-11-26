using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Inspectables {
    public abstract class BaseInspectable : MonoBehaviour {
        public List<ItemObject> keys;
        public InventoryObject inventory;

        public void Inspect() {
            if (keys.Count == 0 || inventory.ContainsAll(keys)) {
                InspectOpen();
            } else {
                InspectClosed();
            }
        }

        protected abstract void InspectOpen();
        protected abstract void InspectClosed();
    }
}
