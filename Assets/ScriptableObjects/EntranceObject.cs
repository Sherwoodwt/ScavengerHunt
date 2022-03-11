using UnityEngine;

[CreateAssetMenu(menuName = "Entrance Object", fileName = "DefaultEntranceObject")]
public class EntranceObject : ScriptableObject {
    public Vector2 entrypoint;
    public LocationObject toLocation;
    public LocationObject FromLocation;
}
