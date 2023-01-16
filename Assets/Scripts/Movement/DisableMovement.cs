using UnityEngine;

namespace Scripts.Movement {
    public class DisableMovement : MonoBehaviour {
        NormalPhysics physics;

        void Start() {
            physics = GameObject.FindGameObjectWithTag("Player")?.GetComponent<NormalPhysics>();
            if (physics == null) {
                throw new MissingReferenceException("No object with tag 'Player' in Scene");
            }
        }

        void Update() {
            physics.input = Vector2.zero;
        }
    }
}