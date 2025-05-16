using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//放在需要完成任务的对象上
public class QuestTarget : MonoBehaviour
{
    public QuestInfo quest;
    public QuestList myQuestList;
    // public string questName;
    // public enum QuestType { Gathering, Talk, Reach };
    // //gathering/ reach: 可以在没有接任务的情况下完成，获取后该任务直接变成finished状态。获取道具时会有对话框提示。
    // //talk: 必须接任务才能完成
    // public QuestType questType;
    public SingleItem questItem; //获取类任务需要获取的特殊物品
    public Inventory myBag;

    [Header("Gather Type")]
    public bool hasGathered;

    [Header("Talk Type")]
    public bool hasTalked;

    [Header("Reach Type")]
    public bool hasReached;

    private void Update()
    {
        if (quest.questType == QuestInfo.QuestType.Gathering)
        {
            //QuestItemCheck();
        }
    }

    //任务完成之后调用
    //原先方法
    // public void QuestComplete()
    // {
    //     for (int i = 0; i < Player.instance.questList.Count; i++)
    //     {
    //         if ((questName == Player.instance.questList[i].questName) && (Player.instance.questList[i].questStatus == Quest.QuestStatus.Accepted)) //任务存在并未accepted状态
    //         {
    //             switch (questType)
    //             {
    //                 case QuestType.Gathering:
    //                     QuestItemCheck();
    //                     if (hasGathered == true)
    //                     {
    //                         Player.instance.questList[i].questStatus = Quest.QuestStatus.Completed;
    //                         Debug.Log("Quest " + questName + " Complete");
    //                     }
    //                     break;
    //                 case QuestType.Reach:
    //                     if (hasReached == true)
    //                     {
    //                         Player.instance.questList[i].questStatus = Quest.QuestStatus.Completed;
    //                         Debug.Log("Quest " + questName + " Complete");
    //                     }
    //                     break;
    //                 case QuestType.Talk:
    //                     if (hasTalked == true)
    //                     {
    //                         Player.instance.questList[i].questStatus = Quest.QuestStatus.Completed;
    //                         Debug.Log("Quest " + questName + " Complete");
    //                     }
    //                     break;
    //             }
    //         }
    //     }
    // }

    public void QuestComplete()
    {
        for (int i = 0; i < myQuestList.questList.Count; i++)
        {
            if ((quest.questName == myQuestList.questList[i].questName) && (myQuestList.questList[i].questStatus == QuestInfo.QuestStatus.Accepted)) //任务存在并未accepted状态
            {
                switch (quest.questType)
                {
                    case QuestInfo.QuestType.Gathering:
                        QuestItemCheck();
                        if (hasGathered == true)
                        {
                            myQuestList.questList[i].questStatus = QuestInfo.QuestStatus.Completed;
                            Debug.Log("Quest " + quest.questName + " Complete");
                        }
                        break;
                    case QuestInfo.QuestType.Reach:
                        if (hasReached == true)
                        {
                            myQuestList.questList[i].questStatus = QuestInfo.QuestStatus.Completed;
                            Debug.Log("Quest " + quest.questName + " Complete");
                        }
                        break;
                    case QuestInfo.QuestType.Talk:
                        if (hasTalked == true)
                        {
                            myQuestList.questList[i].questStatus = QuestInfo.QuestStatus.Completed;
                            Debug.Log("Quest " + quest.questName + " Complete");
                        }
                        break;
                }
            }
        }
    }

    //判定是否完成任务
    //gathering：判断目标物品是否存在在背包中，若存在则将对应任务的状态设置为finished
    //reach：到达即完成 OnTriggerEnter
    //talk：对话结束即完成
    public void OnTriggerEnter(Collider other)
    {
        if ((quest.questType == QuestInfo.QuestType.Reach) && (other.CompareTag("Player")))
        {
            hasReached = true;
            QuestComplete();
        }
    }

    public void QuestItemCheck()
    {
        //Debug.Log("QuestItemCheck");
        //QuestInfo thisQuest = myQuestList.questList.Find(t => t.questName == quest.questName);
        if (myBag.itemList.Contains(questItem)) //判断目标物品是否存在在背包中
        {
            //SingleItem newItem = myBag.itemList.Find(t => t.itemName == quest.questItem.itemName);
            if (quest.questItem.itemHeld == quest.requiredAmount) //背包中目标物品数量=任务需求中目标物体数量
            {
                quest.questStatus = QuestInfo.QuestStatus.Completed;
                hasGathered = true;
            }
        }
    }
}
