using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Scripts.Movement;
using Scripts.Utilities;

namespace Scripts.Inspectables {
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(RandomAudio))]
    public class Textbox : MonoBehaviour {
        public Text displayText;
        public string text;
        public GameObject talker;
        public float textSpeed = .01f;
        public int chunkSize;
        public string playerTag = "Player";

        new AudioSource audio;
        RandomAudio randomAudio;
        Canvas canvas;
        DisableMovement disableMovement;
        NormalController characterController;
        RandomMovement targetRandomMovement;
        PathMovement targetPathMovement;
        bool writing, skip;
        [SerializeField] string[] chunks;
        int currentChunk;
        GameObject player;

        void OnEnable() {
            player = GameObject.FindWithTag(playerTag);
            audio = GetComponent<AudioSource>();
            randomAudio = GetComponent<RandomAudio>();

            var chonques = new List<string>();
            var words = text.Split(' ');
            var chonque = string.Empty;
            for (int i = 0; i < words.Length; i++) {
                if (chonque.Length + words[i].Length + 1 >= chunkSize) {
                    chonques.Add(chonque);
                    chonque = words[i] + ' ';
                } else {
                    chonque += words[i] + ' ';
                }
            }
            if (!string.IsNullOrEmpty(chonque))
                chonques.Add(chonque);

            chunks = chonques.ToArray();
            currentChunk = 0;
            randomAudio.PlayAudio();
            StartCoroutine(WriteText(chunks[currentChunk]));

            Pause();
        }

        void OnDisable() {
            disableMovement.enabled = false;
            characterController.DisableInspect = false;


            if (targetRandomMovement != null)
                targetRandomMovement.DisableFocus();

            if (targetPathMovement != null)
                targetPathMovement.DisableFocus();
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                if (writing) {
                    skip = true;
                } else {
                    currentChunk++;
                    if (currentChunk < chunks.Length) {
                        randomAudio.PlayAudio();
                        StartCoroutine(WriteText(chunks[currentChunk]));
                    } else {
                        audio.Stop();
                        gameObject.SetActive(false);
                    }
                }
            }
        }

        IEnumerator WriteText(string text) {
            displayText.text = "";
            writing = true;
            var chars = text.ToCharArray();
            for (int i = 0; i < chars.Length; i++) {
                if (skip) {
                    displayText.text = text;
                    skip = false;
                    audio.Stop();
                    break;
                }
                displayText.text += chars[i];
                yield return new WaitForSeconds(textSpeed);
            }
            writing = false;
        }

        void Pause() {
            if (!disableMovement || !characterController) {
                disableMovement = GetComponent<DisableMovement>();
                characterController = player.GetComponent<NormalController>();
            }
            disableMovement.enabled = true;
            characterController.DisableInspect = true;

            Debug.Log("PAUSING");

            targetRandomMovement = talker.GetComponent<RandomMovement>();
            if (targetRandomMovement != null)
                targetRandomMovement.SetFocus(player.transform.position);
            targetPathMovement = talker.GetComponent<PathMovement>();
            if (targetPathMovement != null)
                targetPathMovement.SetFocus(player.transform.position);
        }
    }
}
