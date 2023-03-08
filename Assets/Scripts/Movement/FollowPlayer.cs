using Scripts.Movement;
using UnityEngine;

namespace Scripts.Movement {
    public class FollowPlayer : MonoBehaviour {
        new public BoxCollider2D collider;
        public BasePhysics player;
        public float factor = 1f, maxSpeed;

        void Update() {
            var xdif = 0f;
            if (player.transform.position.x > collider.bounds.max.x)
                xdif = player.transform.position.x - collider.bounds.max.x;
            else if (player.transform.position.x < collider.bounds.min.x)
                xdif = player.transform.position.x - collider.bounds.min.x;

            var ydif = 0f;
            if (player.transform.position.y > collider.bounds.max.y)
                ydif = player.transform.position.y - collider.bounds.max.y;
            else if (player.transform.position.y < collider.bounds.min.y)
                ydif = player.transform.position.y - collider.bounds.min.y;

            xdif *= Mathf.Min(Time.deltaTime * player.accel * factor, maxSpeed);
            ydif *= Mathf.Min(Time.deltaTime * player.accel * factor, maxSpeed);

            if (xdif != 0 || ydif != 0)
                transform.position = new Vector3(
                    transform.position.x + xdif,
                    transform.position.y + ydif,
                    transform.position.z);
        }
    }
}
