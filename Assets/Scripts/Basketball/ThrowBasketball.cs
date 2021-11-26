using UnityEngine;

namespace Scripts.Basketball {
    public class ThrowBasketball : MonoBehaviour {
        public GameObject basketballPrefab;

        [SerializeField] GameObject basketball;

        void Update() {
            if (Input.GetKeyDown(KeyCode.M) && basketball == null) {
                basketball = GameObject.Instantiate(basketballPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
