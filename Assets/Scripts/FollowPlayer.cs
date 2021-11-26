using UnityEngine;

namespace Scripts {
    public class FollowPlayer : MonoBehaviour {
        new public BoxCollider2D collider;
        public CharacterMovement player;

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

            xdif *= Time.deltaTime * player.magnitude;
            ydif *= Time.deltaTime * player.magnitude;

            if (xdif != 0 || ydif != 0)
                transform.position = new Vector3(
                    transform.position.x + xdif,
                    transform.position.y + ydif,
                    transform.position.z);
        }
    }
}
