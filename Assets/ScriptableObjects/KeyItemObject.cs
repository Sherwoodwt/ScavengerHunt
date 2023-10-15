using UnityEngine;

[CreateAssetMenu(menuName = "KeyItem Object", fileName = "DefaultKeyItemObject")]
public class KeyItemObject : ItemObject {
    public Sprite activatedSprite;
    public new string name;

    [SerializeField]
    bool active = false;

    public void Toggle() {
        active = !active;
    }

    public bool Active {
        get { return active; }
    }

    public Sprite CurrentSprite() {
        return active ? activatedSprite : sprite;
    }
}
