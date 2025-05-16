using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class QuestManager : MonoBehaviour
{
    static QuestManager myQuestManager;
    public QuestList myQuestList;
    public GameObject questBroadcastPanel;
    public TMP_Text questBroadcastText;

    private void Awake()
    {
        if (myQuestManager != null)
        {
            Destroy(this);
        }
        myQuestManager = this;
    }

    
    public static void questUpdateUI()
    {
        //让UI显示任务列表，每次打开背包的时候都要刷新任务列表？可以用prefab生成列表，在quest panel中增加grid限制间距。
        //限制任务列表的数量，如果任务列表过多，可以用scrollview来显示。
        //需不需要单独这一个代码控制任务列表的显示？

        for (int j = 0; j < myQuestManager.myQuestList.questList.Count; j++)
        {
            GameObject questImage = GameObject.Find(myQuestManager.myQuestList.questList[j].questImageplace);
            questImage.GetComponent<Image>().enabled = true;
            if (myQuestManager.myQuestList.questList[j].questStatus == QuestInfo.QuestStatus.Accepted)
            {
                
                questImage.GetComponent<Image>().sprite = myQuestManager.myQuestList.questList[j].questNote_accepted;
            }
            else if (myQuestManager.myQuestList.questList[j].questStatus == QuestInfo.QuestStatus.Completed)
            {
                
                questImage.GetComponent<Image>().sprite = myQuestManager.myQuestList.questList[j].questNote_completed;
            }
        
        }
        //Debug.Log("List refreshed");
    }

    public static void questBroadcast(string name)
    {
        myQuestManager.questBroadcastPanel.GetComponent<BroadcastQuest>().QuestBroadcast(name);

    }

}
