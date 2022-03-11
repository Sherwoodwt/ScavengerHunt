using UnityEngine;

[CreateAssetMenu(menuName = "Location Object", fileName = "DefaultLocationObject")]
public class LocationObject : ScriptableObject {
    public string sceneName;
    public EntranceObject[] entrances;
}
