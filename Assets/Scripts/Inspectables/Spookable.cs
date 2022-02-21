using System.Collections;
using Scripts.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Inspectables {
    [RequireComponent(typeof(AudioSource))]
    public class Spookable : MonoBehaviour, Inspectable {
        public LocationObject location;
        public EntranceObject entrance;
        public SpawnObject spawnObject;
        [Range(.2f, 5)]
        public float delay;

        new AudioSource audio;

        void Start() {
            audio = GetComponent<AudioSource>();
        }

        public void Inspect() {
            spawnObject.spawnpoint = entrance;
            audio.Play();
            StartCoroutine(LoadScene());
        }

        IEnumerator LoadScene() {
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene(location.sceneName);
        }
    }
}
