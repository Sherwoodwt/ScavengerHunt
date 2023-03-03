using UnityEngine;

namespace Scripts.Movement {
    public interface Influencable {
        void SetFocus(Vector3 position);
        void DisableFocus();
    }
}
