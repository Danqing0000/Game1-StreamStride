using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;

public class ItemOnWorld : MonoBehaviour
{
    public SingleItem thisItem;
    //public ItemInteraction itemInteraction;
    public bool collideState = false;
    public Canvas interactionPanel;
    public Inventory myBag;
    public bool TalkTrigger;
    public bool itemGet = false;
    public GameObject dialogueTree;


    private void Awake()
    {
        //itemRefresh();
        //Debug.Log("itemrefreshed");

        
    }
    
    private void Start()
    {
        interactionPanel.enabled = false;
        InventoryManager.RefreshSlot();
    }


    //TODO:在检测到碰撞的同时按f获取道具
    void Update()
    {        
        if ((collideState == true) && (Input.GetKeyDown(KeyCode.F)) && (GameManager.keyInput == true))
        {
            addNewItem(thisItem);
            if (gameObject.GetComponent<QuestTarget>())
            {
                gameObject.GetComponent<QuestTarget>().QuestComplete(); //判断收集物是否是任务收集物，如果是，则在trigger enter的时候检测数量是否相同
                Debug.Log("Check questComlete");
            }
            else
            {
                Debug.Log("it's not a quest target");
            }
            itemGet = true;
            
            if (this.gameObject.GetComponent<SecondTalkTrigger>())
            {
                gameObject.GetComponent<SecondTalkTrigger>().secondTrigger();
            }
            interactionPanel.enabled = false;
            gameObject.SetActive(false);
        }
    }


    //TODO: detect collision, open f-press panel
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

    private void itemRefresh()
    {
        thisItem.itemHeld = thisItem.itemConfig;
        myBag.itemList.Clear();
    }

    public void addNewItem(SingleItem item)
    {
        //return thisItem;
        //Debug.Log("item added");
        //Debug.Log(item.itemName);
        if (!myBag.itemList.Contains(item)) //包里没有这项物品就将这个物品加到背包列表中
        {
            item.itemHeld += 1;
            myBag.itemList.Add(item);
        }
        else
        {
            item.itemHeld += 1;
        }

        InventoryManager.RefreshSlot();
        InventoryManager.AddItemBroadcast(item.itemName);

    }

    public bool secondQuestTrigger()
    {
        if (TalkTrigger && itemGet)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
}
