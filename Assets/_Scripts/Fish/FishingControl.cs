using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class FishingControl : MonoBehaviour
{
    public int direction = 1;
    public bool isFishing = false; //是否完成钓鱼的动作
    public float Speed = 0.006f;
    bool Xstate = true;
    bool Ystate = false;
    public TMP_Text hint;
    public TMP_Text fishCountText;
    public bool catched = false; //是否捕捉到
    Vector2 defaultPosition;
    Vector2 defaultBoatPosition;
    public int fishCount = 0;
    public static int fishAmount;
    public static string controlGameObject;
    public static bool fishingState = false;
    public AudioSource myAudioSource;
    public AudioMixerSnapshot success;
    public AudioMixerSnapshot fail;
    public AudioMixerSnapshot moving;

    int temp;

    int frameCount = 0;
    int frameCountPre;
    string fishSoundPlay;
    bool isPlaying = false;
    bool fishing = false;
    bool newStart = true;
    public bool allfished = false;


    public GameObject boat;
    //public List<GameObject> fishPosition = new List<GameObject>()


    //public List<SingleItem> fishingList = new List<SingleItem>();

    // Start is called before the first frame update
    void Start()
    {
        pondInitialize();
        defaultPosition = gameObject.transform.localPosition;
        defaultBoatPosition = boat.transform.localPosition;
        fishCountText.text = fishAmount.ToString() + " fishes has been detected. \nYou have caught " + fishCount.ToString() + " fishes in total.";

        //moving.TransitionTo(0.5f);
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if (fishCount < fishAmount) //池子里的鱼数量小于鱼的总数
    //     {
    //         fishingProcess();

    //         if ((isFishing == true) && (catched == true))
    //         {
    //             fishSoundPlay = "Success";

    //             hint.text = "Congratulations! You caught a fish!";
    //             fishCount++;
    //             fishCountText.text = "You have caught " + fishCount.ToString() + " fishes in total! \nYou have " + (fishAmount - fishCount).ToString() + " fishes left!";
    //             isFishing = false;
    //             catched = false;
    //             Debug.Log(fishCount);
    //             frameCount = 0;
    //             StartCoroutine(Restart());
    //         }
    //         else if ((isFishing == true) && (catched == false))
    //         {
    //             fishSoundPlay = "fail";
    //             hint.text = "Please try again!";
    //             //Debug.Log(isFishing.ToString() + catched.ToString());
    //             frameCount = 0;
    //             //Debug.Log(frameCount + " " + frameCountPre);

    //             StartCoroutine(Restart());
    //         }
    //         fishingSFX();
    //         frameCountPre = frameCount;
    //     }
    //     else
    //     {
    //         StartCoroutine(Restart());
    //         hint.text = "All fish are caught!";
    //         fishingState = true;
    //         Xstate = false;
    //         fishCount = 10;
    //         gameObject.transform.localPosition = defaultPosition;
    //         //myAudioSource2.Stop();
    //     }
    //     //Debug.Log(frameCount);
    //     //fishingSFX();

    //     frameCount++;
    //     //Debug.Log(frameCount);

    // }

    //第二个方案 用按钮控制何时重新钓鱼
    void Update()
    {
        if ((fishCount < fishAmount) && (fishing == true)) //池子里的鱼数量小于鱼的总数
        {
            fishingProcess();
            //Debug.Log("Fishing");
        }
        else if (fishCount == fishAmount)
        {
            fishingState = true;
            hint.text = "All fish are caught!";
            allfished = true;
        }
    }

    public void startFishing()
    {
        fishing = true;
        Xstate = true;
        Ystate = false;
        isFishing = false;
        gameObject.transform.localPosition = defaultPosition;
        Debug.Log("Fishing now + " + fishing.ToString());
    }



    public void fishingSFX(bool t)
    {
        //myAudioSource.Stop();
        // if ((t == true) && (myAudioSource.isPlaying == false))
        // {
        //     // myAudioSource.clip = myAudioClip[1];
        //     // myAudioSource.Play();
        //     //myAudioSource.PlayOneShot(myAudioClip[0]);
        //     // isPlaying = true;
        //     // success.TransitionTo(0.5f);
        //     // fishSoundPlay = "";
        // }
        if ((t == false) && (myAudioSource.isPlaying == false))
        {
            //myAudioSource.clip = myAudioClip[2];
            myAudioSource.Play();
            //myAudioSource.PlayOneShot(myAudioClip[1]);
            // StartCoroutine(FailSound());
            // fail.TransitionTo(0.5f);
            // fishSoundPlay = "";
            // Debug.Log("fail");
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log(other.name);
        if ((other.tag == "FishingPoint") && (isFishing == true)) //x y都确定，判断光标是否在钓鱼点上
        {
            //isFishing = false;
            catched = true;
            other.gameObject.SetActive(false); 
            hint.text = "Congratulations! You caught a fish!";
            fishCount++;
            fishCountText.text = fishAmount.ToString() + " fishes has been detected. \nYou have caught" + fishCount.ToString() + " fishes in total.";
            isFishing = false;
            //fishingSFX(true);
            catched = false;
            GameObject.Find(controlGameObject).GetComponent<FishPointOnWorld>().fishAddItem();
            
            //myAudioSource.PlayOneShot(myAudioClip[1]);
            //Debug.Log("catch");
        }
        else if ((other.tag != "FishingPoint") && (isFishing == true))
        {
            //Debug.Log("not catch");
            hint.text = "Please try again!";
            catched = false;
            fishingSFX(false);
            //myAudioSource.PlayOneShot(myAudioClip[2]);
        }
        //fishingFeedback();
    }

    public void fishingFeedback()
    {
        if (catched == true)
        {

            Debug.Log(fishCount);
        }
        else if (catched == false)
        {
            hint.text = "Please try again!";
        }
        Debug.Log("Feedback + " + fishing.ToString());
        //gameObject.transform.localPosition = defaultPosition;
        fishing = false;
        newStart = true;

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Image") //碰到边界后，转换方向
        {
            direction = direction * -1;
            //Debug.Log("collider exit");
        }
    }

    public void fishingProcess()
    {
        // if (((Input.GetKeyDown(KeyCode.E)) && (fishing == false) && (fishCount < fishAmount)) || (fishing = true))
        // {
        //fishing = true;
        //TODO:先固定x轴，再固定y轴，再判断是否在钓鱼点上（通过collider判断）。中间间隔3秒 ui动画补充
        fishingXAxis();
        if ((Input.GetKeyDown(KeyCode.E)) && (Xstate == true))
        {
            //Debug.Log("X Locked");
            Xstate = false;
            StartCoroutine(wait());
        }
        fishingYAxis();
        if ((Input.GetKeyDown(KeyCode.E)) && (Xstate == false) && (Ystate == true))
        {
            //Debug.Log("Y Locked");
            Ystate = false;
            isFishing = true;
            //fishingFeedback();
        }
        //}
    }

    private void fishingXAxis()
    {
        if (Xstate == true)
        {
            boat.transform.localPosition = new Vector2((boat.transform.localPosition.x + direction) * Speed, boat.transform.localPosition.y);
        }
    }

    private void fishingYAxis()
    {
        if (Ystate == true)
        {
            gameObject.transform.localPosition = new Vector2(gameObject.transform.localPosition.x, (gameObject.transform.localPosition.y + direction) * Speed);
        }

    }

    IEnumerator wait()
    {
        //print(Time.time);
        yield return new WaitForSecondsRealtime(1);
        //Debug.Log("Wait");
        Ystate = true;
        //print(Time.time);
    }

    IEnumerator FailSound()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        fail.TransitionTo(0.5f);
        yield return new WaitForSecondsRealtime(0.5f);
    }

    IEnumerator Restart()
    {

        yield return new WaitForSecondsRealtime(1f);
        //isFishing = false; //是否完成钓鱼的动作
        catched = false; //是否捕捉到
        //gameObject.transform.localPosition = defaultPosition;
        Xstate = true;
        Ystate = false;
        //isFishing = false;
        //moving.TransitionTo(2f);
        hint.text = "Please press E to start fishing!";
        Debug.Log("Restart");
    }



    public void pondInitialize()
    {
        GameObject[] fishingPosition = GameObject.FindGameObjectsWithTag("FishingPoint");
        //Debug.Log(fishingPosition.Length);
        foreach (GameObject position in fishingPosition)
        {
            position.SetActive(false);
        }

        List<int> tempList = new List<int>();

        for (int i = 0; i < fishAmount; i++)
        {
            temp = Random.Range(0, 16);

            if (i >= 1) //需要生成的数大于两个时
            {
                for (int j = 0; j < tempList.Count; j++)
                {
                    if (temp == tempList[j]) //在if循环中重新生成temp，在相等时直接将该循环从头进行，已确保temp和列表中的每个数都不相同
                    {
                        temp = Random.Range(0, 16);
                        j = -1; //j = j-1 只比较当前位置的值是否相同，不能确定temp是否与先前的数相同
                    }
                }
            }
            //Debug.Log("i = " + i);
            tempList.Add(temp);
            fishingPosition[temp].SetActive(true);
            //Debug.Log("position: " + temp);
        }
    }

    public void CheckClose()
    {
        if (allfished)
        {
            
        }
    }
}



