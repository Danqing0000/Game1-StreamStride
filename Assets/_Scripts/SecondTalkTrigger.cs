using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;

public class SecondTalkTrigger : MonoBehaviour
{
    public DialogueTreeController dialogueTree;
    public string itemName;
    public bool collideState = false;
    public Canvas interactionPanel;
    public bool itemCheck = false; //物体上也会挂载操作提示，通过该变量判断是不是可收集的物体。不是物品的话要用secondtrigger显示交互面板

    void Update()
    {
        if ((collideState == true) && (Input.GetKeyDown(KeyCode.F)) && (itemCheck == false) && (GameManager.keyInput == true))
        {
            secondTrigger();
        }
    }

    public void secondTrigger()
    {
        dialogueTree.gameObject.GetComponent<Blackboard>().SetValue("itemName", itemName);
        dialogueTree.StartDialogue();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (itemCheck == false)
        {
            if (other.tag == "Detection")
            {
                interactionPanel.enabled = true;
            }
            else if (other.tag == "Player")
            {
                collideState = true;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (itemCheck == false)
        {
            if (other.tag == "Detection")
            {
                interactionPanel.enabled = false;
                collideState = false;
            }
            else if (other.tag == "Player")
            {
                collideState = false;
            }
        }
    }
}
