using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public DialogueObject currentDialogue;
    public Image currentImage;
    public TextMeshProUGUI currentName;
    public TextMeshProUGUI currentDialogueText;
    public Color textColor;
    int index;
    float currentTextSpeed;
    float currentDialogueDelay;

    void Start()
    {
        currentDialogueText.text = currentDialogue.dialogueStrings[0].text;
        currentName.text = currentDialogue.dialogueStrings[0].name;
        currentImage.sprite = currentDialogue.dialogueStrings[0].image;
        textColor = currentDialogue.dialogueStrings[0].color;
        currentDialogueText.color = textColor;
        currentTextSpeed = currentDialogue.dialogueStrings[0].textSpeed;
        currentDialogueDelay = currentDialogue.dialogueStrings[0].dialogueDelay;
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K) && index < currentDialogue.dialogueStrings.Count -1)
        {
            index++;
            currentTextSpeed = 0f;
            StartCoroutine(Wait());

        }

        if(Input.GetKeyDown(KeyCode.J) && index > 0)
        {
            index--;
            currentTextSpeed = 0f;
            StartCoroutine(Wait());
        }
    }

    private void NextLine(int index)
    {
        currentDialogueText.text = currentDialogue.dialogueStrings[index].text;
        currentName.text = currentDialogue.dialogueStrings[index].name;
        currentImage.sprite = currentDialogue.dialogueStrings[index].image;
        textColor = currentDialogue.dialogueStrings[index].color;
        currentDialogueText.color = textColor;
        currentTextSpeed = currentDialogue.dialogueStrings[index].textSpeed;
        currentDialogueDelay = currentDialogue.dialogueStrings[index].dialogueDelay;

        StartCoroutine(Type());
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(currentDialogueDelay);
        NextLine(index);
    }

    public IEnumerator Type()
    {
        currentDialogueText.text = "";
        foreach (char letter in currentDialogue.dialogueStrings[index].text.ToCharArray())
        {
            currentDialogueText.text += letter;

            yield return new WaitForSeconds(currentTextSpeed);
        }
    }
}
