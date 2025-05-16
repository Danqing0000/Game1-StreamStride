using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public enum QuestType { Gathering, Talk, Reach };
    public enum QuestStatus { Waitting, Accepted, Completed };

    public string questName;
    public QuestType questType;
    public QuestStatus questStatus;

    public SingleItem reward; //Reward item

    [Header("Gather Type")]
    public int requiredAmount;
    public SingleItem questItem;
}
