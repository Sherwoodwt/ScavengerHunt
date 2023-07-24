using UnityEngine;

[CreateAssetMenu(menuName = "Inventory Object", fileName = "DefaultInventoryObject")]
public class InventoryObject : ScriptableObject {
    public ItemObject[] items = new ItemObject[36];

    public void Add(ItemObject item) {
        if (!Contains(item)) {
            items[nextAvailableSlot()] = item;
        }
    }

    public bool Contains(ItemObject item) {
        for (int i = 0; i < items.Length; i++) {
            if (items[i] == item)
                return true;
        }
        return false;
    }

    public int Count() {
        var count = 0;
        for (int i = 0; i < items.Length; i++) {
            if (items[i] != null)
                count++;
        }
        return count;
    }

    public void Clear() {
        items = new ItemObject[36];
    }

    int nextAvailableSlot() {
        for (int i = 0; i < items.Length; i++) {
            if (items[i] == null)
                return i;
        }
        throw new System.Exception("For some reason there's no space in the inventory");
    }
}
