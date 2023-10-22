using System.Collections;
using Scripts.Movement;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Utilities {
    [RequireComponent(typeof(DisableMovement))]
    public class StartChallenge : MonoBehaviour {
        public Image countdown;
        public Sprite three, two, one, go;
        public float waitTime;

        DisableMovement disableMovement;

        void Start() {
            disableMovement = GetComponent<DisableMovement>();
            disableMovement.enabled = true;

            StartCoroutine(StartGame());
        }

        IEnumerator StartGame() {
            countdown.color = Color.white;

            countdown.sprite = three;
            yield return new WaitForSeconds(waitTime);

            countdown.sprite = two;
            yield return new WaitForSeconds(waitTime);

            countdown.sprite = one;
            yield return new WaitForSeconds(waitTime);

            countdown.sprite = go;
            yield return new WaitForSeconds(waitTime);

            disableMovement.enabled = false;
            countdown.color = Color.clear;
        }
    }
}
