using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System;

public class OnGui : EditorWindow
{
    [MenuItem("Tools/Dialogue Editor")]

    static void OpenWindow()
    {
        GetWindow<OnGui>();
    }

    int dialogueIndex;
    string[] dialogueAssets;
    string[] dialogueAssetsName;
    Vector2 distance;
    string imageName;

    private void OnGUI()
    {
        titleContent = new GUIContent("Dialogue Editor");

        if(GUILayout.Button("New Dialogue"))
        {
            NewDialogue();
        }

        LoadAllDialogues();

        if(dialogueAssets.Length == 0)
        {
            EditorGUILayout.HelpBox("No Dialogues Found", MessageType.Error);
            return;
        }

        dialogueIndex = EditorGUILayout.Popup("Dialogues", dialogueIndex, dialogueAssetsName);

        GUILayout.Label(dialogueAssets[dialogueIndex]);

        DialogueObject dialogueObject = AssetDatabase.LoadAssetAtPath<DialogueObject>(dialogueAssets[dialogueIndex]);

        distance = EditorGUILayout.BeginScrollView(distance);

        GUILayout.Label("Dialogues List");
        GUILayout.Space(6f);

        for(int i = 0; i < dialogueObject.dialogueStrings.Count; i++)
        {
            if(dialogueObject.dialogueStrings[i].image != null)
            {
                imageName = dialogueObject.dialogueStrings[i].image.ToString();
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(dialogueObject.dialogueStrings[i].name, imageName != null ? imageName : "null");
            GUILayout.Space(20f);

            GUILayout.Label(dialogueObject.dialogueStrings[i].text);
            
            if(GUILayout.Button("Edit", EditorStyles.miniButtonRight, GUILayout.Width(25f)))
            {
                DialogueWindowEditor dialogueWindowEditor = EditorWindow.GetWindow<DialogueWindowEditor>();

                dialogueWindowEditor.stringIndex = i;
                dialogueWindowEditor.stringText = dialogueObject.dialogueStrings[i].text;
                dialogueWindowEditor.characterName = dialogueObject.dialogueStrings[i].name;
                dialogueWindowEditor.dialogueObject = dialogueObject;
            }

            if(GUILayout.Button("Remove", EditorStyles.miniButtonLeft, GUILayout.Width(50f)))
            {
                if(EditorUtility.DisplayDialog("Confirm",
                    $"You are about to remove the string {dialogueObject.dialogueStrings[i].text}",
                    "Remove", "Cancel"))
                {
                    RemoveString(dialogueObject.dialogueStrings[i].text);
                    break;
                }
            }

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView();
    }

    private void RemoveString(string text)
    {
        foreach(string dialogue in dialogueAssets)
        {
            DialogueObject dialogueObject = AssetDatabase.LoadAssetAtPath<DialogueObject>(dialogue);
            dialogueObject.dialogueStrings.RemoveAll(d => d.text == dialogue);

            EditorUtility.SetDirty(dialogueObject);
        }
    }

    private void LoadAllDialogues()
    {
        dialogueAssets = AssetDatabase.FindAssets("t: DialogueObject");
        dialogueAssetsName = new string[dialogueAssets.Length];

        for(int i = 0; i < dialogueAssets.Length; i++)
        {
            dialogueAssets[i] = AssetDatabase.GUIDToAssetPath(dialogueAssets[i]);
            dialogueAssetsName[i] = Path.GetFileName(dialogueAssets[i]);
        }
    }

    private void NewDialogue()
    {
        string dialoguePath = EditorUtility.SaveFilePanelInProject("New Dialogue Asset", "NewDialogue", "asset", "New Dialogue Created");

        if(!string.IsNullOrEmpty(dialoguePath))
        {
            DialogueObject newDialogueObject = ScriptableObject.CreateInstance<DialogueObject>();
            AssetDatabase.CreateAsset(newDialogueObject, dialoguePath);

            EditorUtility.SetDirty(newDialogueObject);
        }
    }
}
