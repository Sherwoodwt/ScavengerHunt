using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Scripts.Movement;

namespace Scripts.Inspectables {
    [RequireComponent(typeof(AudioSource))]
    public class Textbox : MonoBehaviour {
        public Text displayText;
        public string text;
        public float textSpeed = .01f;
        public int chunkSize;
        public AudioClip[] clips;
        public string playerTag = "Player";

        new AudioSource audio;
        Canvas canvas;
        DisableMovement disableMovement;
        NormalController characterController;
        bool writing, skip;
        [SerializeField] string[] chunks;
        int currentChunk;

        void OnEnable() {
            if (!audio || !disableMovement || !characterController) {
                audio = GetComponent<AudioSource>();
                disableMovement = GetComponent<DisableMovement>();
                characterController = GameObject.FindWithTag(playerTag).GetComponent<NormalController>();
            }
            disableMovement.enabled = true;
            characterController.DisableInspect = true;

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
            PlayAudio();
            StartCoroutine(WriteText(chunks[currentChunk]));
        }

        void OnDisable() {
            disableMovement.enabled = false;
            characterController.DisableInspect = false;
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                if (writing) {
                    skip = true;
                } else {
                    currentChunk++;
                    if (currentChunk < chunks.Length) {
                        PlayAudio();
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

        void PlayAudio() {
            if (audio.isPlaying) {
                audio.Stop();
            }
            var i = Random.Range(0, clips.Length);
            audio.PlayOneShot(clips[i]);
        }
    }
}
