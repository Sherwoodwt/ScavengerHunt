using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory Object", fileName = "DefaultInventoryObject")]
public class InventoryObject : ScriptableObject
{
    public List<ItemObject> items;

    public void Add(ItemObject item) {
        if (!items.Contains(item))
            items.Add(item);
    }

    public bool ContainsAll(List<ItemObject> items) {
        bool contained = true;
        foreach (var item in items) {
            if (!this.items.Contains(item)) {
                contained = false;
                break;
            }
        }
        return contained;
    }
}
