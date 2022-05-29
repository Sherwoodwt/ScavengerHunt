using UnityEngine;

namespace Scripts.Inspectables {
    public class Questionable : MonoBehaviour, Inspectable {
        public GameObject questionPrefab;
        [TextArea(2, 20)]
        public string question, answer, correct, incorrect;
        public ItemObject prize;
        public InventoryObject inventory;

        public void Inspect() {
            var obj = GameObject.Instantiate(questionPrefab);
            var questionBox = obj.GetComponent<QuestionBox>();
            questionBox.question = question;
            questionBox.answer = answer;
            questionBox.correct = correct;
            questionBox.incorrect = incorrect;
            questionBox.prize = prize;
            questionBox.inventory = inventory;
        }
    }
}
