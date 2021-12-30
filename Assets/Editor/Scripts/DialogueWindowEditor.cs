using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DialogueWindowEditor : EditorWindow
{
    public SerializedProperty image;
    public DialogueObject dialogueObject;
    public string characterName;
    public string stringText;
    public int stringIndex;

    public bool confirmed;

    private void OnGUI()
    {
        titleContent = new GUIContent("Edit Dialogue Text");

        GUILayout.Label("Character Name");
        EditorGUILayout.TextField(characterName);

        if(image != null)
        {
            EditorGUILayout.ObjectField(image);
        }

        GUILayout.Label("Text");
        stringText = EditorGUILayout.TextArea(stringText, GUILayout.ExpandHeight(true));

        GUILayout.Space(20f);

        GUILayout.BeginHorizontal();

        if(GUILayout.Button("Confirm"))
        {
            confirmed = true;
            dialogueObject.dialogueStrings[stringIndex].text = stringText;
            Close();
        }

        if(GUILayout.Button("Cancel"))
        {
            Close();
        }
    }
}
