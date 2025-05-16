using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "QuestList", menuName = "Quest/QuestList")]
public class QuestList : ScriptableObject
{
    public List<QuestInfo> questList = new List<QuestInfo>();

}
