using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts {
    public class MenuHandler : MonoBehaviour {
        public SpawnObject spawn;
        public InventoryObject inventory;

        readonly string mainMenuScene = "MainMenu";

        public void HandleStartNewGame() {
            spawn.spawnpoint = null;
            inventory.items.Clear();
            SceneManager.LoadScene(1);
        }

        public void HandleContinueGame() {
            var currentScene = spawn.spawnpoint.FromLocation.sceneName;
            SceneManager.LoadScene(currentScene, LoadSceneMode.Single);
        }

        public void HandleExit() {
# if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
# else
            Application.Quit();
# endif
        }

        public void HandleUnpause() {
            Destroy(gameObject);
        }

        public void HandleExitGame() {
            SceneManager.LoadScene(mainMenuScene, LoadSceneMode.Single);
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.Tab))
                HandleUnpause();
        }
    }
}
