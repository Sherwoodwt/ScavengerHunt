using UnityEngine;

namespace Scripts.Pool {
    [RequireComponent(typeof(Collider2D))]
    public class PoolKillPlayer : MonoBehaviour {
        public PoolGame poolGame;

        void OnTriggerEnter2D(Collider2D collider) {
            if (collider.gameObject.CompareTag("Player")) {
                poolGame.Kill(collider.gameObject);
            }
        }
    }
}
