using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.Utilities {
    [RequireComponent(typeof(Button))]
    public class AutoDeselect : MonoBehaviour, IPointerUpHandler {
        Button button;

        void Start() {
            button = GetComponent<Button>();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
