using UnityEngine;

namespace Scripts.Movement {
    public class DisableMovement : MonoBehaviour {
        NormalController controller;
        NormalPhysics physics;

        void OnEnable() {
            var player = GameObject.FindGameObjectWithTag("Player");
            controller = player.GetComponent<NormalController>();
            physics = player.GetComponent<NormalPhysics>();

            controller.DisableInputs = true;
            physics.input = Vector2.zero;
        }

        void OnDisable() {
            controller.DisableInputs = false;
        }
    }
}