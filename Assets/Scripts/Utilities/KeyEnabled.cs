using UnityEngine;

namespace Scripts.Utilities {
    public class KeyEnabled : MonoBehaviour {
        public InventoryObject invetory;
        public ItemObject key;
        public MonoBehaviour[] behaviours;

        void Update() {
            // TODO: I was going to have this enable/disable monobehaviours based on if key in inventory
            // but instead I want to add an event to InvetoryObject.Add and subscribe here, and also in
            // NewItem (currently checking every update)
        }
    }
}
