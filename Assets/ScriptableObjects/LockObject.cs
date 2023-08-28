using UnityEngine;

[CreateAssetMenu(menuName = "Lock Object", fileName = "DefaultLockObject")]
public class LockObject : ScriptableObject {
    public ItemObject key;
    public bool unlocked;
}
