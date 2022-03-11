using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Pool {
    public class PoolControls : MonoBehaviour {
        // TODO: Draw cursor as crosshair
        public GameObject cueBallPrefab;

        void Update() {
            if (Input.GetMouseButtonDown(0)) {
                var obj = GameObject.Instantiate(cueBallPrefab, transform.position, Quaternion.identity);
                var cueBall = obj.GetComponent<CueBall>();
                cueBall.Target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
    }
}
