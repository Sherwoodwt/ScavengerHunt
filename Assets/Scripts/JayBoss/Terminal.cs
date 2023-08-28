using UnityEngine;
using UnityEngine.UI;

namespace Scripts.JayBoss {
    [RequireComponent(typeof(AudioSource))]
    public class Terminal : MonoBehaviour {
        void Update() {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space)) {
                Destroy(this.gameObject);
            }
        }
    }
}
