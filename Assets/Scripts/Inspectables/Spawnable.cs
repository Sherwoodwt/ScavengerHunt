using UnityEngine;

namespace Scripts.Inspectables {
    public class Spawnable : MonoBehaviour, Inspectable {
        public GameObject prefab;

        public void Inspect() {
            Instantiate(prefab);
        }
    }
}
