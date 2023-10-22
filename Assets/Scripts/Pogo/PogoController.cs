using UnityEngine;

namespace Scripts.Pogo {
    [RequireComponent(typeof(PogoPhysics))]
    public class PogoController : BaseControls {
        PogoPhysics physics;

        void Start() {
            physics = GetComponent<PogoPhysics>();
        }

        void Update() {
            if (DisableInputs)
                return;
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
