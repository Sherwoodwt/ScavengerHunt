using UnityEngine;

namespace Scripts.Pogo {
    [RequireComponent(typeof(PogoPhysics))]
    public class PogoController : MonoBehaviour {
        PogoPhysics physics;

        void Start() {
            physics = GetComponent<PogoPhysics>();
        }

        void Update() {
            if (Input.GetKey(KeyCode.A)) {
                physics.MoveLeft();
            } else if (Input.GetKey(KeyCode.D)) {
                physics.MoveRight();
            } else {
                physics.MoveStop();
            }
        }
    }
}
