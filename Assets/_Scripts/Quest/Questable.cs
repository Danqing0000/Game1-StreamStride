using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script will be hanging on the NPCs that will be able to give quests to the player
//called the script after finishing talking to the npc.
public class Questable : MonoBehaviour
{
    public QuestInfo quest;
    public Inventory myBag;
    public QuestList myQuestList;
    public SingleItem extraItem;


    private void Awake()
    {
        QuestRefresh();
    }

    public void QuestRefresh()
    {
        quest.questStatus = QuestInfo.QuestStatus.Waitting; //重置任务状态
        myQuestList.questList.Clear(); //清空任务列表
    }

    public void DelegateQuest() //将quest存储到quest列表中
    {
        if (quest.questStatus == QuestInfo.QuestStatus.Waitting)
        {
            //TODO: Delegate the quest to the player

            //写法一：在player脚本中创建任务列表。
            //Player.instance.questList.Add(quest);
            //quest.questStatus = Quest.QuestStatus.Accepted;
            //GameObject.Find("Boat").GetComponent<Player>().questUpdateUI();


            //写法二：单独用scriptableobject来创建任务列表，储存任务。
            quest.questStatus = QuestInfo.QuestStatus.Accepted;
            myQuestList.questList.Add(quest);
            //QuestManager.questUpdateUI();
            Debug.Log("Quest " + quest.questName + " Accepted");

        }
        else
        {
            //TODO: player has already accepted the quest, so no need to accept it again
            //任务的领取由对话树控制，理论上来说是不会出现重复领取任务的情况。
            Debug.Log("Player has already accepted the quest");
        }
    }

    public bool QuestCompleteCheck() //尽管没有接取任务，也可以检测收集类任务是否完成
    {
        bool check = false;
        //SingleItem thisItem = myBag.itemList.Find(t => t == quest.questItem);
        if (myBag.itemList.Contains(quest.questItem)) //包中有特定的任务道具
        {
            if (quest.questItem.itemHeld == quest.requiredAmount) //道具数量满足任务需求
            {
                check = true;
            }
        }
        else
        {
            Debug.Log("Quest item is not in the bag");
        }
        return check;
        //if (myBag.itemList.)
        //if (quest.questStatus == Quest.QuestStatus.Waitting) && (Player)
    }

    public void ExtraQuestAdd()
    {
        quest.questStatus = QuestInfo.QuestStatus.Completed;
        QuestManager.questBroadcast(quest.questName);
        if (!myQuestList.questList.Contains(quest))
            myQuestList.questList.Add(quest);
        //Player.instance.questList.Add(quest);
    }

    public string QuestStatusCheck()
    {
        Debug.Log(quest.questStatus);
        //string s = "1";
        string s = quest.questStatus.ToString();

        return s;
    }

    public void Reward()
    {
        myBag.itemList.Add(quest.reward);
        myBag.itemList.Find(t => t == quest.reward).itemHeld += 1;
        InventoryManager.AddItemBroadcast(quest.reward.itemName);
        if (quest.questItem != null)
        {
            myBag.itemList.Remove(quest.questItem);
        }
        Debug.Log("Reward" + quest.reward.name + " assigned to the bag.");

        //quest.questStatus = QuestInfo.QuestStatus.Completed;
    }

    //用一个代码来检测 1是否收集完成 2是否到达某区域 3是否完成和某人对话

    public void AddExtraItem()
    {
        myBag.itemList.Add(extraItem);
        Debug.Log("Extra item " + extraItem.name + " added to the bag.");
    }
    public void RemoveExtraItem()
    {
        myBag.itemList.Remove(extraItem);
        Debug.Log("Extra item " + extraItem.name + " removed from the bag.");
    }
}
