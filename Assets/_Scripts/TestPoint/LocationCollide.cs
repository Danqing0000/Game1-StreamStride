using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LocationCollide : MonoBehaviour
{
    public int loactionAmount;
    public int thisAmount = 0; //points that has been checked.
    public TMP_Text sumText;

    // Start is called before the first frame update
    void Start()
    {
        //interactionPanel.enabled = false;
        sumText.text = "0 / " + loactionAmount.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // for (int i = 0; i < gameObject.transform.childCount; i++)
            // {
            //     //gameObject.transform.GetChild(i).gameObject.SetActive(true);
            // }
            Debug.Log("Enter space");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            // foreach (Transform child in gameObject.GetComponentsInChildren<Transform>())
            // {
            //     child.gameObject.SetActive(false);

            // }
            this.gameObject.SetActive(true);
            Debug.Log("Exit space");
        }
    }

    public void locationCheck()
    {
        thisAmount = thisAmount + 1;
        sumText.text = thisAmount.ToString() + " / " + loactionAmount.ToString();
        if (thisAmount == loactionAmount)
        {
            Destroy(GetComponent<SphereCollider>());
            Debug.Log("All location checked");
            GameManager.dayProgressed = true;
            //Debug.Log(GameManager.dayProgressed);
        }
    }
}
