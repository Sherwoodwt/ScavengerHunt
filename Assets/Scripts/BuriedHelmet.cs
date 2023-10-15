using Scripts.Inspectables;

namespace Scripts {
    public class BuriedHelmet : Grabbable {
        public LockObject loche;

        public override void NoItemResponse() {
            if (inventory.Contains(key) && !inventory.Contains(item)) {
                inventory.Add(item);
                if (audio != null) {
                    audio.Play();
                    loche.unlocked = true;
                }
            }
        }
    }
}
