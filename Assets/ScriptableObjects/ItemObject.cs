using UnityEngine;

[CreateAssetMenu(menuName = "Item Object", fileName = "DefaultItemObject")]
public class ItemObject : ScriptableObject {
    [TextArea(2, 4)]
    public string description;
    public Sprite sprite;

    public virtual string Description {
        get { return description; }
    }
}
