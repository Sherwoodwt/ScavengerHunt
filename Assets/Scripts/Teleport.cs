using System.Collections;
using Scripts.Movement;
using Scripts.Utilities;
using UnityEngine;

namespace Scripts {
    [RequireComponent(typeof(RandomAudio))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(DisableMovement))]
    public class Teleport : MonoBehaviour {
        public GameObject poofPrefab;
        public Vector2 destination;
        public string cameraTag = "MainCamera";
        
        RandomAudio randomAudio;
        FollowPlayer followPlayer;
        DisableMovement disableMovement;

        void Start() {
            randomAudio = GetComponent<RandomAudio>();
            followPlayer = GameObject.FindWithTag(cameraTag).GetComponent<FollowPlayer>();
            disableMovement = GetComponent<DisableMovement>();
            disableMovement.enabled = false;
        }

        void OnTriggerEnter2D(Collider2D collider) {
            if (collider.tag == "Player") {
                StartCoroutine(SendIt(collider.transform));
            }
        }

        IEnumerator SendIt(Transform player) {
            var playerSprite = player.GetComponent<SpriteRenderer>();
            randomAudio.PlayAudio();

            var poof = GameObject.Instantiate(poofPrefab, player);
            if (playerSprite) playerSprite.enabled = false;
            disableMovement.enabled = true;
            followPlayer.enabled = false;
            yield return new WaitForSeconds(.5f);

            // GameObject.Destroy(poof);
            player.position = destination;
            if (playerSprite) playerSprite.enabled = true;
            followPlayer.enabled = true;
            disableMovement.enabled = false;
        }
    }
}