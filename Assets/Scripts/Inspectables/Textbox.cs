using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Inspectables {
    [RequireComponent(typeof(AudioSource))]
    public class Textbox : MonoBehaviour {
        public Text displayText;
        public string text;
        public float textSpeed = .01f;
        public int chunkSize;
        public AudioClip[] clips;

        new AudioSource audio;
        bool writing;
        [SerializeField] string[] chunks;
        int currentChunk;

        void Start() {
            audio = GetComponent<AudioSource>();

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

        void Update() {
            if (writing)
                return;

            if (Input.GetKeyDown(KeyCode.Space)) {
                currentChunk++;
                if (currentChunk < chunks.Length) {
                    PlayAudio();
                    StartCoroutine(WriteText(chunks[currentChunk]));
                } else {
                    audio.Stop();
                    GameObject.Destroy(this.gameObject);
                }
            }
        }

        IEnumerator WriteText(string text) {
            displayText.text = "";
            writing = true;
            var chars = text.ToCharArray();
            for (int i = 0; i < chars.Length; i++) {
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
