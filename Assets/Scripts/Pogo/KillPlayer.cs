using Scripts.Inspectables;
using UnityEngine;

namespace Scripts.Pogo {
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class KillPlayer : MonoBehaviour {
        public LayerMask layerMask;
        public Transform player;
        public EntranceObject spawnPoint;

        new AudioSource audio;
        new BoxCollider2D collider;
        Grabbable grabbable;

        void Start() {
            audio = GetComponent<AudioSource>();
            collider = GetComponent<BoxCollider2D>();
            grabbable = GetComponent<Grabbable>();
        }

        void OnTriggerEnter2D(Collider2D other) {
            if (collider.IsTouchingLayers(layerMask)) {
                audio.Play();
                player.position = spawnPoint.entrypoint;

                if (grabbable)
                    grabbable.Inspect();
            }
        }
    }
}
