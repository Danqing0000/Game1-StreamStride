using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCheck : MonoBehaviour
{
    public Canvas interactionPanel;
    public bool collideState = false;

    void Update()
    {        
        if ((collideState == true) && (Input.GetKeyDown(KeyCode.F)))
        {
            GameManager.instance.DayProgressCheck();
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
        }
        else if (other.tag == "Player")
        {
            collideState = false;
        }
    }
}
