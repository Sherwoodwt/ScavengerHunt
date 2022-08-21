using System.Collections.Generic;
using System.Linq;
using Scripts.Inspectables;
using Scripts.Pogo;
using UnityEngine;

namespace Scripts.SpiderTrivia {
    // A class for genearting trivia questions and setting random choices to the correct answer.
    // Meant to be used a number of times, then at teh end present a new room and a prize.
    public class QuestionGame : MonoBehaviour {
        public QuestionObject[] possibleQuestions;
        public int goalScore;
        public string questionTag = "Question", answerTag = "Answer";

        [SerializeField] int score;
        int questionIndex;
        Talkable question;
        Talkable[] answers;

        void Start() {
            question = GameObject.FindGameObjectWithTag(questionTag).GetComponent<Talkable>();
            answers = GameObject.FindGameObjectsWithTag(answerTag)
                .Where(o => o.GetComponent<Talkable>() != null)
                .Select(o => o.GetComponent<Talkable>())
                .ToArray();
            InitRoom();
        }

        // Chooses a random question and sets up the room objects
        void InitRoom() {
            questionIndex = Random.Range(0, possibleQuestions.Length);
            var cur = possibleQuestions[questionIndex];

            question.texts = new List<string>{ cur.question };
            for (int i = 0; i < answers.Length; i++) {
                if (answers.Length <= i) {
                    throw new System.Exception($"Need 3 answers to question, only have {i}");
                }
                answers[i].texts = new List<string> { cur.answers[i] };
                var killPlayer = answers[i].GetComponentInChildren<KillPlayer>();
                if (cur.correctAnswer == i) {
                    killPlayer.OnKill += Correct;
                } else {
                    killPlayer.OnKill += Incorrect;
                }
            }
        }

        void Correct() {
            Debug.Log("CORRECT");
        }

        void Incorrect() {
            Debug.Log("WRONG BITCH");
        }

        // TODO: False transition between rooms, to avoid having to store data between rooms
        // TODO: New room on win
        // TODO: Update: check which room, set up question
        // TODO: Wrong/Right functionality for levers to hook to
        // TODO: Random question selection (only a subset required to win, no duplicates)
        // TODO: Sign only on the first room
    }
}
