 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;

public class TutorialTrigger : MonoBehaviour
{
    public DialogueTreeController dialogueTree;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "player")
        {
            Debug.Log("444444");
            dialogueTree.gameObject.GetComponent<Blackboard>().SetValue("itemName", "firstday");
            dialogueTree.StartDialogue();
        }
    }
}
