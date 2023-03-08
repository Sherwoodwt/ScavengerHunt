using Scripts.Movement;
using UnityEngine;

namespace Scripts.Shuffle {
    [RequireComponent(typeof(Collider2D))]
    public class BedMonster : MonoBehaviour {
        public GameObject handPrefab;
        public float startY;

        Collider2D rangeCollider;

        void Start() {
            rangeCollider = GetComponent<Collider2D>();
        }

        void OnTriggerEnter2D(Collider2D collider) {
            if (collider.tag == "Player") {
                var instance = GameObject.Instantiate(handPrefab);
                instance.transform.position = new Vector3(collider.transform.position.x, startY, transform.position.z);

                var movement = instance.GetComponent<PathMovement>();
                if (movement != null) {
                    movement.points = new Vector2[2] {
                        collider.transform.position,
                        instance.transform.position + new Vector3(0, -.1f, 0),
                    };
                }

                rangeCollider.enabled = false;
            }
        }
    }
}
