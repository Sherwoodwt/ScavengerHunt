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
        public string questionTag = "Question",
            answerTag = "Answer",
            loreTag = "Lore",
            portalTag = "Portal";

        [Space]
        [SerializeField] int score;
        [SerializeField] QuestionObject[] originalQuestions;
        int questionIndex;
        Talkable question;
        Talkable[] answers;
        GameObject loreSign, portal;

        void Start() {
            originalQuestions = possibleQuestions.Select(q => q).ToArray();
            question = GameObject.FindGameObjectWithTag(questionTag).GetComponent<Talkable>();
            answers = GameObject.FindGameObjectsWithTag(answerTag)
                .Where(o => o.GetComponent<Talkable>() != null)
                .Select(o => o.GetComponent<Talkable>())
                .ToArray();
            loreSign = GameObject.FindGameObjectWithTag(loreTag);
            portal = GameObject.FindGameObjectWithTag(portalTag);
            InitRoom();
        }

        // Chooses a random question and sets up the room objects
        void InitRoom() {
            if (score == 0) {
                portal.SetActive(false);
                loreSign.SetActive(true);
            } else {
                loreSign.SetActive(false);
                portal.SetActive(true);

                var sprite = portal.GetComponent<SpriteRenderer>();
                var c = sprite.color;
                var ratio = score / (float)goalScore;
                sprite.color = new Color(c.r, c.g, c.b, ratio);

                if (score == goalScore) {
                    portal.GetComponent<Rotate>().enabled = true;
                    portal.GetComponent<Passage>().enabled = true;

                    question.gameObject.SetActive(false);
                    foreach (var answer in answers) {
                        var door = answer.GetComponentInChildren<KillPlayer>();
                        door.enabled = false;
                        answer.gameObject.SetActive(false);
                    }
                }
            }
            questionIndex = Random.Range(0, possibleQuestions.Length);
            var cur = possibleQuestions[questionIndex];

            question.texts = new List<string>{ cur.question };
            var displacement = Random.Range(0, answers.Length);
            Debug.Log(displacement);
            for (int i = 0; i < answers.Length; i++) {
                if (answers.Length <= i) {
                    throw new System.Exception($"Need 3 answers to question, only have {i}");
                }
                var di = (i + displacement) % answers.Length;
                answers[i].texts = new List<string> { cur.answers[di] };
                var killPlayer = answers[i].GetComponentInChildren<KillPlayer>();
                if (cur.correctAnswer == di) {
                    killPlayer.OnKill = Correct;
                } else {
                    killPlayer.OnKill = Incorrect;
                }
            }
        }

        void Correct() {
            score++;
            var newList = new QuestionObject[possibleQuestions.Length-1];
            for (int i = 0; i < possibleQuestions.Length; i++) {
                if (i < questionIndex) {
                    newList[i] = possibleQuestions[i];
                } else if (i > questionIndex) {
                    newList[i-1] = possibleQuestions[i];
                }
            }
            possibleQuestions = newList;
            InitRoom();
        }

        void Incorrect() {
            score = 0;
            possibleQuestions = originalQuestions.Select(q => q).ToArray();
            InitRoom();
        }

        // TODO: Random question placement
    }
}
