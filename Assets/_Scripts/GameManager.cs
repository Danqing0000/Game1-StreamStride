using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Audio;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public DialogueTreeController dialogueTree;
    public static bool dayProgressed = false;
    public int day = 0;
    public RawImage rawImage;
    public static bool keyInput = true;
    public GameObject progressUI;
    public TMP_Text todoList;
    public Button nextDayBtn;
    public List<GameObject> airWall;
    public List<GameObject> testPoint;
    public List<GameObject> fishingPoint;
    public List<GameObject> NPC;
    public GameObject tutorialPage;
    public GameObject airwallDay0;
    public GameObject npcDay0;
    public GameObject player;
    public TMP_Text dayCount;
    public List<QuestInfo> questList;
    public GameObject reportPage;
    public GameObject credits;
    public GameObject endingPage;
    public Button quitGame;
    public DialogueTreeController endingdialogueTree;
    public AudioMixerSnapshot ending;
    public List<Sprite> mapList;
    public Image map;
    public TMP_Text testPointCount;

    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (day == 0)
        {   
            player.transform.position = new Vector3(-145, 0, -269);
            player.transform.rotation = Quaternion.Euler(0,0,0);
            keyInput = false;
            tutorialPage.SetActive(true);
            airwallDay0.SetActive(true);
            npcDay0.SetActive(true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void KeyboardDisable(bool key)
    {
        keyInput = key;
    }

    public void DayProgressCheck() //to chcek if the game can move to the next day
    {
        if ((dayProgressed) && (day != 4))
        {
            day++;
            dialogueTree.gameObject.GetComponent<Blackboard>().SetValue("itemName", "dayFinished");
        }
        else if ((dayProgressed == false) && (day != 4))
        {
            Debug.Log("Day not progressed");
            dialogueTree.gameObject.GetComponent<Blackboard>().SetValue("itemName", "dayNotFinished");
        }
        else if (day == 4) //last day
        {
            Debug.Log("Day 4");
            dialogueTree.gameObject.GetComponent<Blackboard>().SetValue("itemName", "dayLast");
        }
        dialogueTree.StartDialogue();
    }

    public void DayProgress() //loding into the next day, fade in and fade out
    {
        rawImage.GetComponent<RawImage>().DOFade(1,1f); //逐渐变黑
        StartCoroutine(Wait());
        //progressUI.SetActive(true);
        if (day == 0)
        {
            todoList.text = "1. Walk around Harbour Village.\n2. Water Quality Test in 5 spots.\n3. Live like a local!";
        }
        else if (day == 1)
        {
            todoList.text = "1. Visit Lian Hua Village.\n2. Water Quality Test in 6 spots.\n3. Live like a local!";
        }
        else if (day == 2)
        {
            todoList.text = "1. Travel to the southeast corner.\n2. Water Quality Test in 5 spots.\n3. Live like a local!";
        }
        else if (day == 3)
        {
            todoList.text = "1. Finish the REPORT! \n2. Finish the REPORT! \n3. Finish the REPORT!";
        }
        testPointCount.text = "? / ?";
        //展示ui
        //rawImage.GetComponent<SceneSwitch>().sceneEnding = true;
    }

    public void NewDayStart()
    {
        dayProgressed = false;
        player.transform.position = new Vector3(-146, 0, -192);

        for (int i = 0; i < 3; i++)
        {
            airWall[i].SetActive(false);
            testPoint[i].SetActive(false);
            fishingPoint[i].SetActive(false);
            NPC[i].SetActive(false);
            
        }
        if (day < 3 )
        {
            airWall[day].SetActive(true);
            testPoint[day].SetActive(true);
            fishingPoint[day].SetActive(true);
            NPC[day].SetActive(true);
            map.sprite = mapList[day];

            StartCoroutine(WaittoStart());
        }
        else if (day == 3)
        {
            showReport();
        }
        dayCount.text = "DAY " + (day + 1).ToString();
        
    }

    public void showReport()
    {
        //show report
        StartCoroutine(WaitforReport());
    }

    IEnumerator DayProgressCoroutine(int dayTime)
    {
        yield return new WaitForSecondsRealtime(8);
        Debug.Log("scene fade in");
        //yield return new WaitForSecondsRealtime(8);
        //DayProgress(day);
        //After we have waited 5 seconds print the time again.
    }

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(2);
        progressUI.SetActive(true);
    }

    IEnumerator WaitforButton()
    {
        yield return new WaitForSecondsRealtime(2);
        nextDayBtn.gameObject.SetActive(true);
    }

    IEnumerator WaittoStart()
    {
        yield return new WaitForSecondsRealtime(2);
        rawImage.GetComponent<RawImage>().DOFade(0,1f);
        keyInput = true;
    }

    IEnumerator WaitforReport()
    {
        yield return new WaitForSecondsRealtime(2);
        reportPage.SetActive(true);
        keyInput = false;
    }

    public bool QuestCheck(int i)
    {
        if (questList[i].questStatus == QuestInfo.QuestStatus.Completed)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void FinalDecision(bool t)
    {
        if (t == true)
        {
            Debug.Log("environment");
        }
        else
        {
            Debug.Log("development");
        }
        reportPage.SetActive(false);
        StartCoroutine(WaitforEnding(t));
    }

    IEnumerator WaitforEnding(bool t)
    {
        yield return new WaitForSecondsRealtime(2);
        if (t == true)
        {
            endingdialogueTree.gameObject.GetComponent<Blackboard>().SetValue("itemName", "environment");
            Debug.Log("environment2");
        }
        else
        {
            endingdialogueTree.gameObject.GetComponent<Blackboard>().SetValue("itemName", "development");
            Debug.Log("development2");
        }
        endingdialogueTree.StartDialogue();
    }

    public void Ending()
    {
        Debug.Log("ending");
        StartCoroutine(EndandCredits());
    }

    IEnumerator EndandCredits()
    {
        yield return new WaitForSecondsRealtime(2);
        ending.TransitionTo(2f);
        endingPage.SetActive(true);
        credits.transform.DOMoveY(2000, 25f).SetEase(Ease.Linear);
    }
}
