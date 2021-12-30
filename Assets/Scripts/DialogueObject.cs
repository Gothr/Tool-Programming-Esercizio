using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueObject.asset", menuName = "Dialogue")]
public class DialogueObject : ScriptableObject
{
    [System.Serializable]
    public class DialogueString
    {
        public Sprite image;
        public string text;
        public string name;
        public Color color;
        public float textSpeed;
        public float dialogueDelay;
    }

    public List<DialogueString> dialogueStrings = new List<DialogueString>();
}
