using UnityEngine;

namespace Scripts.Utilities {
    // TODO: Is this obsolete? I feel like yes
    public static class LayerUtils {
        public static bool InLayerMask(this GameObject gameObject, LayerMask mask) {
            return (mask == (mask | 1 << gameObject.layer));
        }
    }
}
