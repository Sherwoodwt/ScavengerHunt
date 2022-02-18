﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections;

namespace Scripts {
    [RequireComponent(typeof(AudioSource))]
    public class Passage : MonoBehaviour {
        public LocationObject toLocation;
        public ItemObject[] keys;
        public InventoryObject inventory;
        public SpawnObject spawn;
        public GameObject transitionPrefab;

        readonly string lockedLayer = "LockedDoor";
        readonly string openLayer = "Door";
        readonly string canvasTag = "Canvas";

        new AudioSource audio;
        Transform canvas;
        int inventorySize = 0;
        bool open = false;

        void Start() {
            audio = GetComponent<AudioSource>();
            canvas = GameObject.FindWithTag(canvasTag)?.transform;
            if (canvas == null)
                throw new System.Exception($"No object found with tag {canvasTag}");

            if (keys.Length == 0) {
                open = true;
                gameObject.layer = LayerMask.NameToLayer(openLayer);
            } else {
                gameObject.layer = LayerMask.NameToLayer(lockedLayer);
            }
        }

        void Update() {
            // Once opened they cannot be closed
            if (open || inventorySize == inventory.items.Count || keys.Length == 0)
                return;

            inventorySize = inventory.items.Count;

            open = true;
            foreach (var key in keys) {
                if (!inventory.items.Contains(key)) {
                    open = false;
                    break;
                }
            }

            if (open) {
                gameObject.layer = LayerMask.NameToLayer(openLayer);
            } else {
                gameObject.layer = LayerMask.NameToLayer(lockedLayer);
            }
        }

        // TODO: Clean this up
        void OnTriggerEnter2D(Collider2D collider) {
            if (open) {
                var entrance = toLocation.entrances
                    .Single(e => e.toLocation.sceneName == SceneManager.GetActiveScene().name);
                spawn.spawnpoint = entrance;

                GameObject.Instantiate(transitionPrefab, canvas.position, Quaternion.identity, canvas);
                audio.Play();

                StartCoroutine(Transition());
            }
        }

        IEnumerator Transition() {
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(toLocation.sceneName, LoadSceneMode.Single);
        }
    }
}