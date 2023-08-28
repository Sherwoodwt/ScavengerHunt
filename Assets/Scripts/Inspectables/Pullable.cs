using Scripts.Inspectables;
using UnityEngine;

namespace Scripts.SpiderTrivia {
    public delegate void PullAction();

    public class Pullable : Inspectable {
        public PullAction onPull;

        public override void NoItemResponse() {
            transform.RotateAround(transform.position, Vector3.up, 180);
            onPull();
        }
    }

}
