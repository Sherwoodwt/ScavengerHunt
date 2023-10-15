using UnityEngine;

[CreateAssetMenu(menuName = "KeyItems Object", fileName = "DefaultKeyItemsObject")]
public class KeyItemsObject : ScriptableObject {
    public KeyItemObject shoes, translator, whistle;

    public bool Contains(KeyItemObject item) {
        return shoes == item || translator == item || whistle == item;
    }
}
