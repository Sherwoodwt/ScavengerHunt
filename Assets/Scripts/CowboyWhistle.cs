using System.Collections;
using System.Collections.Generic;
using Scripts.Inspectables;
using Scripts.Movement;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts {
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(ToggleItem))]
    [RequireComponent(typeof(DisableMovement))]
    public class CowboyWhistle : MonoBehaviour {
        public float distance = 10f;
        public GameObject cowboyPrefab;
        public KeyCode key;
        public KeyItemsObject keyItems;
        public KeyItemObject keyItem;

        new AudioSource audio;
        ToggleItem toggle;
        DisableMovement disable;

        void Start() {
            audio = GetComponent<AudioSource>();
            toggle = GetComponent<ToggleItem>();
            disable = GetComponent<DisableMovement>();
        }

        void Update() {
            if (keyItems.Contains(keyItem)) {
                if (Input.GetKeyDown(key)) {
                    StartCoroutine(BlowWhistle());
                }
            }
        }

        public IEnumerator BlowWhistle() {
            audio.Play();
            toggle.Toggle();
            disable.enabled = true;
            yield return new WaitForSeconds(3);

            toggle.Toggle();
            disable.enabled = false;

            // Summon the cowboy
            var player = GameObject.FindGameObjectWithTag("Player");
            var paintings = FindObjectsOfType<Painting>();
            float smallest = float.MaxValue;
            Painting closest = null;
            foreach (var painting in paintings) {
                var vec = painting.transform.position - player.transform.position;
                if (vec.magnitude < distance && vec.magnitude < smallest) {
                    smallest = vec.magnitude;
                    closest = painting;
                }
            }
            if (closest != null) {
                var obj = Instantiate(cowboyPrefab, closest.transform);
                var talkable = obj.GetComponent<Talkable>();
                talkable.texts = new List<string> { closest.cowboyText };
                talkable.Inspect();
            }
        }
    }
}
