using UnityEngine;

namespace Scripts {
    public class DisableMovement : MonoBehaviour {
        CharacterPhysics physics;

        void Start() {
            physics = GameObject.FindGameObjectWithTag("Player")?.GetComponent<CharacterPhysics>();
            if (physics == null) {
                throw new MissingReferenceException("No object with tag 'Player' in Scene");
            }
        }

        void Update() {
            physics.input = Vector2.zero;
        }
    }
}