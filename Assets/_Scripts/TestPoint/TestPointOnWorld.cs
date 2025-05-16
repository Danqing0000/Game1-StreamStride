using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPointOnWorld : MonoBehaviour
{
    public Canvas interactionPanel;
    int keyFrame = 0;
    public bool collideState = false;
    public AudioSource myAudioSource;
    public List<AudioClip> myClips;
    // Start is called before the first frame update
    void Start()
    {
        interactionPanel.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((collideState == true) && (Input.GetKey(KeyCode.F)) && (GameManager.keyInput == true))
        {
            keyFrame++;
        }

        if ((collideState == true) && (keyFrame == 300)) //Add water pumping sound
        {
            keyFrame = 0;
            interactionPanel.enabled = false;
            gameObject.transform.parent.gameObject.GetComponent<LocationCollide>().locationCheck();
            myAudioSource.PlayOneShot(myClips[1]);
            for (int i = 0; i < 5000; i++)
            {
                Debug.Log("waiting");
            }
            myAudioSource.Stop();
            Destroy(gameObject);
            
        }
        if ((collideState == true) && (Input.GetKeyUp(KeyCode.F)) && (GameManager.keyInput == true)) //Add water pumping sound
        {
            myAudioSource.Stop();
        }
        pumping(keyFrame);
    }

    public void pumping(int keyFrame)
    {
        if (keyFrame == 1)
        {
            myAudioSource.clip = myClips[0];
            myAudioSource.Play();
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
}
