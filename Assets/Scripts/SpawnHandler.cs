using UnityEngine;

namespace Scripts {
    public class SpawnHandler : MonoBehaviour {
        public SpawnObject spawn;

        void Start() {
            if (spawn.spawnpoint != null)
                transform.position = (Vector3)spawn.spawnpoint.entrypoint;
        }
    }
}
