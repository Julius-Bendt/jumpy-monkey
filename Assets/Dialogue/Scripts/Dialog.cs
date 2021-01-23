/**
 * Author: Julius Bendt
 * Author URI: www.juto.dk
 * Copyright 2015
 **/

using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Person))]
public class Dialog : MonoBehaviour
{
    public DialogueFile dialogueFile = null;
    public string currentDialog;

    public DialogueWindow window;

    private DialogueManager manager;
    private Dialogue currentDialogue;
    private Dialogue.Choice currentChoice = null;

    public Transform player;

    Person persons;

    [HideInInspector]
    public bool finished, active;

    void Start()
    {
        persons = GetComponent<Person>();
        //StartDialouge(currentDialog);
    }

    public void StartDialouge(string dialogName)
    {
        manager = DialogueManager.LoadDialogueFile(dialogueFile);
        currentDialogue = manager.GetDialogue(dialogName);
        currentChoice = currentDialogue.GetChoices()[0];
        currentDialogue.PickChoice(currentChoice);


        window.gameObject.SetActive(true);

        for(int i = 0; i < window.choices.Length; i++)
        {
            window.choices[i].GetComponent<DialogueButton>().dialog = this;
        }
        window.next.GetComponent<DialogueButton>().dialog = this;

        UpdateWindow();

        finished = false;
        active = true;
    }

    void Update()
    {

        if(active)
            UpdateChoices();
    }

    void UpdateChoices()
    {
        Dialogue.Choice[] choices = currentDialogue.GetChoices();

        for(int i = 0; i < choices.Length; i++)
        {
            window.choices[i].GetComponent<DialogueButton>().choice = choices[i];
        }
    }

    void UpdateWindow()
    {
        window.dialog.text = currentChoice.dialogue;
        window.name.text = currentChoice.speaker;
        window.face.sprite = GetFace(currentChoice.speaker);

        Dialogue.Choice[] choices = currentDialogue.GetChoices();

        if(choices.Length > 1)
        {
            for(int i = 0; i < choices.Length; i++)
            {
                window.choices[i].SetActive(true);
                window.choices[i].GetComponentInChildren<Text>().text = choices[i].dialogue;
            }

            window.next.SetActive(false);
        }
        else if(choices.Length == 1)
        {
            for(int i = 0; i < window.choices.Length; i++)
            {
                window.choices[i].SetActive(false);
            }

            window.next.SetActive(true);
        }
        else
        {
            //end
        }
    }

    Sprite GetFace(string speaker)
    {

        for(int i = 0; i < persons.persons.Count; i++)
        {
            if(persons.persons[i].name.ToLower() == speaker.ToLower())
            {
                return persons.persons[i].face;
            }
        }

        return null;
    }

    void CheckUserdata()
    {
        if (currentChoice.userData.IndexOf("relation:") == 0)
        {
            string[] rel = Regex.Split("relation:", "");
            
            for(int i = 0; i < rel.Length; i++)
            {
                Debug.Log(rel[i]);
            }

            //int rel = int.Parse(currentChoice.userData.Substring(10));
        }
    }

    public void Pick(Dialogue.Choice choice = null)
    {
        if(finished)
        {
            window.gameObject.SetActive(false);
            return;
        }

        if(choice != null)
        {
            currentDialogue.PickChoice(choice);
            currentChoice = choice;
            UpdateChoices();
        }
        else
        {
            currentChoice = currentDialogue.GetChoices()[0];
            currentDialogue.PickChoice(currentChoice);
        }

        if(currentDialogue.GetChoices().Length == 0)
        {
            window.next.GetComponentInChildren<Text>().text = "Leave.";
            finished = true;
        }
        UpdateWindow();
        CheckUserdata();
    }
}

