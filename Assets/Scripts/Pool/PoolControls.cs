using UnityEngine;

namespace Scripts.Pool {
    public class PoolControls : MonoBehaviour {
        public GameObject cueBallPrefab;
        public Texture2D cursorSprite;
        public bool disableShoot;

        void Start() {
            var hotspot = new Vector2(cursorSprite.width/2, cursorSprite.height/2);
            Cursor.SetCursor(cursorSprite, hotspot, CursorMode.Auto);
        }

        void Update() {
            if (!disableShoot && Input.GetMouseButtonDown(0)) {
                var obj = GameObject.Instantiate(cueBallPrefab, transform.position, Quaternion.identity);
                var cueBall = obj.GetComponent<CueBall>();
                cueBall.Target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }

        void OnDestroy() {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}
