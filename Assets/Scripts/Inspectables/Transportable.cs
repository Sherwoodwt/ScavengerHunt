using System.Collections;
using Scripts.Animation;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Inspectables {
    [RequireComponent(typeof(AudioSource))]
    public class Transportable : MonoBehaviour, Inspectable {
        public LocationObject location;
        public EntranceObject entrance;
        public SpawnObject spawnObject;
        [Range(.2f, 5)]
        public float delay;

        new AudioSource audio;
        Wobble wobble;

        void Start() {
            audio = GetComponent<AudioSource>();
            wobble = GetComponent<Wobble>();
        }

        public void Inspect() {
            spawnObject.spawnpoint = entrance;
            audio.Play();
            if (wobble != null) {
                wobble.enabled = true;
            }
            StartCoroutine(LoadScene());
        }

        IEnumerator LoadScene() {
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene(location.sceneName);
        }
    }
}
