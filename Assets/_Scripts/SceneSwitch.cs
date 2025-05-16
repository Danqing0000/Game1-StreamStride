using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    public bool sceneStarting = false;
    public bool sceneEnding = false;
    public float fadeSpeed = 5;
    private RawImage rawImage;

    public void Start()
    {
        rawImage = GetComponent<RawImage>();
        rawImage.color = Color.black;
        //StartScene();
        sceneStarting = true;
    }

    // Update is called once per frame
    void Update()
    {
        //StartScene();
        //Endscene();
        if (sceneStarting)
        {
            StartScene();
        }
        if (sceneEnding)
        {
            Endscene();
        }
    }

    private void FadeToClear()
    {
        rawImage.color = Color.Lerp(rawImage.color, Color.clear, fadeSpeed * Time.deltaTime);
    }

    private void FadeToBlack()
    {
        rawImage.color = Color.Lerp(rawImage.color, Color.black, fadeSpeed * Time.deltaTime);
    }

    public void StartScene()
    {
        rawImage.raycastTarget = false;
        FadeToClear();
        if (rawImage.color.a == 0.1f)
        {
            rawImage.color = Color.clear;
            rawImage.enabled = false;
            sceneStarting = false;
        }

    }

    public void Endscene()
    {
        rawImage.enabled = true;
        FadeToBlack();
        if (rawImage.color.a == 0.99f)
        {   
            rawImage.color = Color.black;
            sceneEnding = false;
            rawImage.raycastTarget = true;
            Debug.Log("switched");
            //SceneManager.Loadscene(Globe.Name);
        }
           
    }


}
