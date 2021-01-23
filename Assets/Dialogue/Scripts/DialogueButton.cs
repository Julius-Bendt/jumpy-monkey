using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//Developed by: Julius Bendt, JutoGames.


public class DialogueButton : MonoBehaviour {

    [HideInInspector]
    public Dialog dialog = null;

    [HideInInspector]
    public Dialogue.Choice choice;

    public void OnClick()
    {
        dialog.Pick(choice);  
    }
}
