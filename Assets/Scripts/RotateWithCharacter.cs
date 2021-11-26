using UnityEngine;

namespace Scripts {
    // TODO: Is this class even used anymore??
    public class RotateWithCharacter : MonoBehaviour {
        public CharacterMovement target;

        Vector2 direction;

        void Start() {
            direction = new Vector2(target.input.x, target.input.y);
            if (direction.magnitude == 0) {
                // default to down
                direction = new Vector2(0, -1);
            }
            Rotate();
        }

        void Update() {
            if (target.input.magnitude != 0 && (
                target.input.x != direction.x || target.input.y != direction.y
            )) {
                    direction = new Vector2(target.input.x, target.input.y);
                    Rotate();
                }
        }

        void Rotate() {
            // TODO: Now, instead of just rotating 90 degrees, rotate in the direction
            //        the character is facing

            // var angle = Quaternion.Angle(target.transform.rotation, transform.rotation);
            // var angle = 
            // transform.RotateAround(target.transform.position, new Vector3(0,0,1), angle);
        }
    }
}
