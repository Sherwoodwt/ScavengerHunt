using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory Object", fileName = "DefaultInventoryObject")]
public class InventoryObject : ScriptableObject {
    public List<ItemObject> items;

    public void Add(ItemObject item) {
        if (!Contains(item))
            items.Add(item);
    }

    public bool Contains(ItemObject item) {
        return items.Contains(item);
    }
}
