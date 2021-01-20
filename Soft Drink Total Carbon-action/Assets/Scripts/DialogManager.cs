﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogManager : MonoBehaviour
{
    private Queue<string> sentences;
    public TextMeshProUGUI speakerName;
    public TextMeshProUGUI dialogText;
    public GameObject canvas;

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialog(Dialog dialogue)
    {
        canvas.SetActive(true);
        speakerName.text = dialogue.speakerName;
        Debug.Log("Starting conversation with: " + dialogue.speakerName);

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        dialogText.text = sentence;
        Debug.Log(sentence);
    }

    public void EndDialogue()
    {
        canvas.SetActive(false);
        Debug.Log("End of conversation!");
    }
}
