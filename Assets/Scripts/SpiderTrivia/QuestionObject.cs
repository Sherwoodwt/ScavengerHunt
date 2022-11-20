using UnityEngine;

[CreateAssetMenu(menuName = "Question Object", fileName = "DefaultQuestionObject")]
public class QuestionObject : ScriptableObject {
    public string question;
    public string[] answers;
    public int correctAnswer;
}
