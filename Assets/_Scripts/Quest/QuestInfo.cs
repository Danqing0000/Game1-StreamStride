using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "QuestInfo", menuName = "Quest/QuestInfo")]
public class QuestInfo : ScriptableObject
{
    [Header("Quest Info")]
    public string questName;
    public Sprite questNote_accepted;
    public Sprite questNote_completed;
    public string questImageplace;
    public enum QuestType { Gathering, Talk, Reach };
    public enum QuestStatus { Waitting, Accepted, Completed };
    public QuestType questType;
    public QuestStatus questStatus;
    

    [Header("Gather Type")]
    public SingleItem questItem;
    public int requiredAmount;

    [Header("Talk Type")]
    public GameObject triggerNPC;

    [Header("Reach Type")]
    public GameObject triggrtArea;

    [Header("Reward")]
    public SingleItem reward; //Reward item

}
