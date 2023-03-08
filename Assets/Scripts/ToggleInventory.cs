using UnityEngine;

namespace Scripts {
    public class ToggleInventory : MonoBehaviour {
        public GameObject openContainer, closedContainer;
        public KeyCode toggleKey;

        public void Toggle() {
            if (openContainer.activeSelf) {
                openContainer.SetActive(false);
                closedContainer.SetActive(true);
            } else {
                openContainer.SetActive(true);
                closedContainer.SetActive(false);
            }
        }

        void Update() {
            if (Input.GetKeyDown(toggleKey)) {
                Toggle();
            }
        }
    }
}
