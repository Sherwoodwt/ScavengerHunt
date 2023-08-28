using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Locks Object", fileName = "DefaultLocksObject")]
public class LocksObject : ScriptableObject {
    public LockObject[] locks;

    public LockObject Get(ItemObject key) {
        return locks.FirstOrDefault(l => l.key == key);
    }
}
