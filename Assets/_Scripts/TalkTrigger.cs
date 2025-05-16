using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.DialogueTrees;

public class TalkTrigger : MonoBehaviour
{
    public DialogueTreeController dialogueTree;
    public bool collideState = false;
    public Canvas interactionPanel;
    public Questable questable; //NPC may contains a quest. Null = not questable
    public bool questStatus = false; //Quest status

    // Start is called before the first frame update
    void Start()
    {
        dialogueTree = gameObject.GetComponent<DialogueTreeController>();
        //interactionPanel = GameObject.Find("F Talk").GetComponent<Canvas>();
        interactionPanel.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((collideState == true) && (Input.GetKeyDown(KeyCode.F)) && (GameManager.keyInput == true))
        {
            Debug.Log("F");
            //interactionPanel.enabled = false;
            dialogueTree.StartDialogue();
        }
    }

    private void OnTriggerEnter(Collider other)
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

    private void OnTriggerExit(Collider other)
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

    public void questAssign()
    {
        questable.DelegateQuest();
        questStatus = true; //用于对话树的调用
    }
}
