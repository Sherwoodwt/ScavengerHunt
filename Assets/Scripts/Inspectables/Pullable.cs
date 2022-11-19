using Scripts.Inspectables;
using UnityEngine;

namespace Scripts.SpiderTrivia {
    public delegate void PullAction();

    public class Pullable : MonoBehaviour, Inspectable {
        public PullAction onPull;

        public void Inspect() {
            transform.RotateAround(transform.position, Vector3.up, 180);

            onPull();
        }
    }

}
