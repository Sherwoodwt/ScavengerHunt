using System.Collections;
using Scripts.Inspectables;
using Scripts.Movement;
using Scripts.Utilites;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.JayBoss {
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(NormalPhysics))]
    [RequireComponent(typeof(DisableMovement))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class ScaryJay : MonoBehaviour {
        public InventoryObject inventory;
        public ItemObject computerCable, mustache;
        public FollowPlayer cameraFocus;
        public AudioClip transitionAudio;
        public Image transitionImage;
        [TextArea()]
        public string introSpeech;

        Textbox textbox;
        new AudioSource audio;
        DisableMovement disableMovement;
        SpriteRenderer spriteRenderer;
        NormalPhysics physics;
        bool introFinished;

        void Start() {
            textbox = TextboxUtils.Init();
            audio = GetComponent<AudioSource>();
            disableMovement = GetComponent<DisableMovement>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            physics = GetComponent<NormalPhysics>();

            if (inventory.Contains(computerCable) && !inventory.Contains(mustache)) {
                disableMovement.enabled = true;
                spriteRenderer.enabled = true;
                StartCoroutine(BeforeBoss());
            } else {
                disableMovement.enabled = false;
                spriteRenderer.enabled = false;
            }
        }

        IEnumerator BeforeBoss() {
            audio.Play();
            yield return new WaitForSeconds(2);

            cameraFocus.player = this.physics;
            textbox.gameObject.SetActive(true);
            textbox.text = introSpeech;
            introFinished = true;
        }

        IEnumerator Transition() {
            audio.clip = transitionAudio;
            audio.Play();
            int total = 100;
            for (int i = 0; i < total; i++) {
                transitionImage.color = Color.Lerp(Color.clear, Color.white, ((float)i)/total);
                yield return new WaitForFixedUpdate();
            }

            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene("HackGame", LoadSceneMode.Single);
        }

        void Update() {
            if (!textbox.gameObject.activeSelf && introFinished) {
                StartCoroutine(Transition());
                introFinished = false;
            }
        }
    }
}
