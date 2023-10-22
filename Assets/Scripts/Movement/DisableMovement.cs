using Scripts.Pogo;
using UnityEngine;

namespace Scripts.Movement {
    public class DisableMovement : MonoBehaviour {
        BaseControls controller;
        BasePhysics physics;

        void OnEnable() {
            var player = GameObject.FindGameObjectWithTag("Player");
            controller = player.GetComponent<BaseControls>();
            physics = player.GetComponent<BasePhysics>();

            controller.DisableInputs = true;
            physics.input = Vector2.zero;
        }

        void FixedUpdate() {
            controller.DisableInputs = true;
            physics.input = Vector2.zero;
        }

        void OnDisable() {
            controller.DisableInputs = false;
        }
    }
}