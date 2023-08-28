using UnityEngine;

namespace Scripts.Inspectables {
    public class Questionable : Inspectable {
        public GameObject questionPrefab;
        [TextArea(2, 20)]
        public string question, answer, correct, incorrect;
        public ItemObject prize;
        public InventoryObject inventory;

        public override void NoItemResponse() {
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
