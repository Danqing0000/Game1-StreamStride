using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movespeed;
    //public Transform cam;
    //float turnSmoothVelocity;
    public float turnSmoothAngle;
    Quaternion original;
    float originalAngle;
    public AudioSource myAudioSource;
    public AudioClip[] clips;
    float h;
    float v;
    bool isMoving = false;
    bool isPlaying = false;
    public AudioMixerSnapshot moving;
    public AudioMixerSnapshot idle;

    //public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        original = Quaternion.Euler(0f, transform.rotation.y, 0f);
        originalAngle = original.y;
        Debug.Log(originalAngle);
        myAudioSource = GetComponent<AudioSource>();
        idle.TransitionTo(0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal"); //KEY: A/D
        v = Input.GetAxisRaw("Vertical");  //KEY: W/S

        Vector3 moveInput = (new Vector3(0, 0, v)).normalized;


        Vector3 dir = new Vector3(h, 0, v);
        float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

        if (GameManager.keyInput == true)
        {   
            if (Input.GetAxis("Horizontal") > 0)
            {
                //Debug.Log("d");
                transform.rotation = Quaternion.Euler(0f, originalAngle + turnSmoothAngle, 0f);
                originalAngle = originalAngle + turnSmoothAngle;
            }
            //Debug.Log(Input.GetAxis("Horizontal"));
            if (Input.GetAxis("Horizontal") < 0)
            {
                //Debug.Log("a");
                transform.rotation = Quaternion.Euler(0f, originalAngle - turnSmoothAngle, 0f);
                originalAngle = originalAngle - turnSmoothAngle;
            }

            //Debug.Log(dir);
            if (Input.GetAxis("Vertical") > 0)
            {
                //transform.position = Time.deltaTime * movespeed * moveInput; // move
                //Debug.Log(transform.position);
                //Debug.Log("w");
                transform.Translate(Vector3.forward * Time.deltaTime * movespeed, Space.Self);
            }

            if (Input.GetAxis("Vertical") < 0)
            {
                //transform.position = Time.deltaTime * movespeed * moveInput; // move
                //Debug.Log(transform.position);
                //Debug.Log("s");
                transform.Translate(Vector3.forward * Time.deltaTime * movespeed * (-1), Space.Self);
            }
        }

        BoatSound();


        //transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);


        //Debug.Log(transform.forward);
    }

    public void BoatSound()
    {
        if ((h != 0 || v != 0) && (isPlaying == false))  //船移动并且audio没在播放, h和v都不等于零
        {
            moving.TransitionTo(0.5f);
            isPlaying = true;
            Debug.Log("Sound Play");
        }
        else if (h == 0 && v == 0) //船静止，h和v都等于零
        {
            idle.TransitionTo(0.5f);
            isPlaying = false;
            //Debug.Log("Sound Play");
        }
    }
}
