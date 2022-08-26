using Scripts.Inspectables;
using Scripts.Utilities;
using UnityEngine;

namespace Scripts.Pogo {
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Collider2D))]
    public class KillPlayer : MonoBehaviour {
        public LayerMask layerMask;
        public Transform player;
        public EntranceObject spawnPoint;
        public string playerTag = "Player";
        public GameObject transitionPrefab;
        public Transform canvas;
        public EmptyEvent OnKill;

        new AudioSource audio;
        new Collider2D collider;
        Grabbable grabbable;

        void Start() {
            audio = GetComponent<AudioSource>();
            collider = GetComponent<Collider2D>();
            grabbable = GetComponent<Grabbable>();
            if (player == null && !string.IsNullOrEmpty(playerTag)) {
                player = GameObject.FindGameObjectWithTag(playerTag)?.transform;
            }
        }

        void OnTriggerEnter2D(Collider2D other) {
            if (collider.IsTouchingLayers(layerMask)) {
                audio.Play();
                player.position = spawnPoint.entrypoint;

                if (grabbable) {
                    grabbable.Inspect();
                }
                
                OnKill();

                if (transitionPrefab != null) {
                    var t = GameObject.Instantiate(transitionPrefab, canvas.position, Quaternion.identity, canvas);
                    GameObject.Destroy(t, 1);
                }
            }
        }
    }
}
