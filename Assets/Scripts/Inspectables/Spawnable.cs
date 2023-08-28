using UnityEngine;

namespace Scripts.Inspectables {
    public class Spawnable : Inspectable {
        public GameObject prefab, successPrefab, failPrefab;

        public override void NoItemResponse() {
            Instantiate(prefab);
        }

        public override void CorrectResponse() {
            if (successPrefab == null) {
                base.CorrectResponse();
            } else {
                Instantiate(successPrefab);
            }
        }

        public override void IncorrectResponse() {
            if (failPrefab == null) {
                base.IncorrectResponse();
            } else {
                Instantiate(failPrefab);
            }
        }
    }
}
