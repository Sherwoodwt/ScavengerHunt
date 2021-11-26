using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace Scripts.Inspectables {
    public class TextLoader : MonoBehaviour {
        public string filePath;
        public Dictionary<string, string> textBlocks;

        void Awake() {
            using (var fin = new StreamReader(filePath)) {
                var text = fin.ReadToEnd();
                var textBlockSet = JsonUtility.FromJson<TextBlockSet>(text);
                textBlocks = new Dictionary<string, string>();
                foreach (var textBlock in textBlockSet.textBlocks) {
                    textBlocks.Add(textBlock.name, textBlock.text);
                }
            }
        }

        [Serializable]
        private class TextBlockSet {
            public List<TextBlock> textBlocks;
        }

        [Serializable]
        private class TextBlock {
            public string name;
            public string text;
        }
    }
}
