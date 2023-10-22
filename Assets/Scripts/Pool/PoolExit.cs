using UnityEngine;

namespace Scripts.Pool {
    public class PoolExit : MonoBehaviour {
        public PoolGame poolGame;

        void OnTriggerEnter2D(Collider2D collider) {
            poolGame.ExitPool();
        }
    }
}
