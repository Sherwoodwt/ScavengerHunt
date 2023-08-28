using UnityEngine;

namespace Scripts.Inspectables {
    public abstract class Inspectable : MonoBehaviour {
        public ItemObject key;

        public void Inspect() {
            Inspect(null);
        }

        public void Inspect(ItemObject item = null) {
            if (item == null) {
                NoItemResponse();
            } else if (item == key) {
                CorrectResponse();
            } else if (item != key) {
                IncorrectResponse();
            }
        }

        public virtual void NoItemResponse() {}

        public virtual void CorrectResponse() {
            NoItemResponse();
        }

        public virtual void IncorrectResponse() {
            NoItemResponse();
        }
    }
}
