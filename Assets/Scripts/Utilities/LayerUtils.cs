using UnityEngine;

namespace Scripts.Utilities {
    public static class LayerUtils {
        public static bool InLayerMask(this GameObject gameObject, LayerMask mask) {
            return (mask == (mask | 1 << gameObject.layer));
        }
    }
}
