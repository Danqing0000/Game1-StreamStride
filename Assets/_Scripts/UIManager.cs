using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //public GameObject BasicUI;
    public Button Map;
    public Button Inventory;
    //public Button Sub;
    public Button Quest;
    public Button Setting;
    public Button Exit;
    public GameObject UI;
    public GameObject InventoryUI;
    public GameObject QuestUI;
    public GameObject MapUI;
    bool isBagOpen = false;
    bool isQuestOpen = false;
    public static UIManager instance;


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

    void Update()
    {
        if (GameManager.keyInput == true)
        {
            if (UI.activeSelf == false)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Setting.onClick.Invoke();
                }
                else if (Input.GetKeyDown(KeyCode.B))
                {
                    //InventoryManager.RefreshSlot();
                    Inventory.onClick.Invoke();
                    isBagOpen = true;
                    OpenBagCheck(isBagOpen);
                }
                else if (Input.GetKeyDown(KeyCode.M))
                {
                    Map.onClick.Invoke();
                }
                else if (Input.GetKeyDown(KeyCode.Q))
                {
                    //QuestManager.questUpdateUI();
                    Quest.onClick.Invoke();
                    isQuestOpen = true;
                    OpenQuestCheck(isQuestOpen);
                }
            }
            else if ((UI.activeSelf == true) && (Input.GetKeyDown(KeyCode.Escape)))
            {
                Exit.onClick.Invoke();
            }
        }

    }

    public void OpenBagCheck(bool refreshed)
    {
        if ((InventoryUI.activeSelf == true) && (refreshed == true))
        {
            InventoryManager.RefreshSlot();
            isBagOpen = false;
        }
    }

    public void OpenQuestCheck(bool refreshed)
    {
        if ((QuestUI.activeSelf == true) && (refreshed == true))
        {
            QuestManager.questUpdateUI();
            isQuestOpen = false;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void SceneChange()
    {
        SceneManager.LoadScene("Scene0", LoadSceneMode.Single);
    }
}