using UnityEngine;

[CreateAssetMenu(menuName = "Locked Item Object", fileName = "DefaultLockedItemObject")]
public class LockedItemObject : ItemObject {
    public ItemObject key;
    public InventoryObject inventory;
    [TextArea(2, 4)]
    public string unlockedDescription;

    public override string Description {
        get {
            if (inventory.Contains(key)) {
                return unlockedDescription;
            }
            return description;
        }
    }
}
