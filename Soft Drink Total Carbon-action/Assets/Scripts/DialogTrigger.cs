using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialogue;

    public void TriggerDialog()
    {
        // Singleton pattern?

        FindObjectOfType<DialogManager>().StartDialog(dialogue);
    }
}
