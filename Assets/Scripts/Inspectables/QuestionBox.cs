using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Inspectables {
    public class QuestionBox : MonoBehaviour {
        public AudioClip questionSound, correctSound, incorrectSound;
        public string question, answer, correct, incorrect;
        public ItemObject prize;
        public InventoryObject inventory;
        public Text text;

        void Start() {
            if (inventory.Contains(prize)) {
                text.text = correct;
            } else {
                text.text = question;
            }
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.Return)) {

            }
        }
    }
}
