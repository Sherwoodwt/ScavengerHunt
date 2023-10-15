using System.Collections.Generic;
using Scripts.Inspectables;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts {
    [RequireComponent(typeof(Talkable))]
    public class ItemDescription : MonoBehaviour {
        public ItemObject item;

        Talkable talkable;

        void Start() {
            talkable = GetComponent<Talkable>();
        }

        public void DescribeItem() {
            talkable.texts = new List<string> { item.Description };
            talkable.Inspect();
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
