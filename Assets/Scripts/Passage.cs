using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections;
using Scripts.Inspectables;

namespace Scripts {
    [RequireComponent(typeof(AudioSource))]
    public class Passage : Inspectable {
        public LocationObject toLocation;
        public SpawnObject spawn;
        public float transitionTime = 1;
        public GameObject transitionPrefab;
        public AudioClip doorNoise;
        public LocksObject locks;

        readonly string lockedLayer = "LockedDoor";
        readonly string openLayer = "Door";
        readonly string canvasTag = "Canvas";

        new AudioSource audio;
        Transform canvas;
        bool open = false;

        void Start() {
            audio = GetComponent<AudioSource>();
            canvas = GameObject.FindWithTag(canvasTag)?.transform;
            if (canvas == null && transitionPrefab != null)
                throw new System.Exception($"No object found with tag {canvasTag}");

            if (key == null || (locks != null && locks.Get(key)?.unlocked == true)) {
                open = true;
                gameObject.layer = LayerMask.NameToLayer(openLayer);
            } else {
                gameObject.layer = LayerMask.NameToLayer(lockedLayer);
            }
        }

        public override void CorrectResponse() {
            open = true;
            locks.Get(key).unlocked = true;
            gameObject.layer = LayerMask.NameToLayer(openLayer);
        }

        void OnTriggerEnter2D(Collider2D collider) {
            if (open && collider.gameObject.tag == "Player") {
                try {
                    var entrance = toLocation.entrances
                        .Single(e => e.toLocation.sceneName == SceneManager.GetActiveScene().name);
                    spawn.spawnpoint = entrance;

                    if (transitionPrefab != null) {
                        GameObject.Instantiate(transitionPrefab, canvas.position, Quaternion.identity, canvas);
                    }
                    audio.clip = doorNoise;
                    audio.Play();

                    StartCoroutine(Transition());
                } catch (System.Exception ex) {
                    Debug.LogError($"no matching enterance in {toLocation.sceneName}");
                    throw ex;
                }
            }
        }

        IEnumerator Transition() {
            yield return new WaitForSeconds(transitionTime);
            SceneManager.LoadScene(toLocation.sceneName, LoadSceneMode.Single);
        }
    }
}
