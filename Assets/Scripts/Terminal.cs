using UnityEngine;
using UnityEngine.UI;

namespace Scripts {
    [RequireComponent(typeof(AudioSource))]
    public class Terminal : MonoBehaviour {
        public InventoryObject invetory;
        public ItemObject prize;
        public InputField input;
        public Text text;
        public AudioClip failureClip;
        public AudioClip successClip;
        [TextArea(1, 2)]
        public string failureText;
        [TextArea(1, 2)]
        public string successText;
        public string password;

        new AudioSource audio;

        void Start() {
            audio = GetComponent<AudioSource>();
            input.Select();
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.Return)) {
                var submission = input.text;
                input.text = string.Empty;

                if (submission == password) {
                    audio.clip = successClip;
                    audio.Play();
                    text.text = successText;
                    invetory.Add(prize);
                }  else {
                    audio.clip = failureClip;
                    audio.Play();
                    text.text = failureText;
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab)) {
                Destroy(this.gameObject);
            }
        }
    }
}
