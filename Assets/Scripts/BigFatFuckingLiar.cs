using Scripts.Inspectables;
using Scripts.Utilities;
using UnityEngine;

namespace Scripts {
    public class BigFatFuckingLiar : MonoBehaviour, Inspectable {
        public GameObject paulGiamattiPrefab, poofPrefab;
        public GameObject disguiseObject;
        public InventoryObject inventory;
        public ItemObject key;

        Textbox textbox;
        bool watching;

        public void Inspect() {
            watching = true;
        }

        void Start() {
            textbox = TextboxUtils.Init();
        }

        void Update() {
            if (inventory.Contains(key) && watching && !textbox.gameObject.activeSelf) {
                GameObject.Instantiate(poofPrefab);
                GameObject.Instantiate(paulGiamattiPrefab);
                disguiseObject.SetActive(false);
                GameObject.Destroy(this);
            }
        }
    }
}