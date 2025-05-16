using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FishManager : MonoBehaviour
{
    static FishManager myFishManager;
    public Inventory myfishbag;

    private void Awake()
    {
        if (myFishManager != null)
        {
            Destroy(this);
        }
        myFishManager = this;
    }

    public static void RefreshFishBag()
    {
        for (int i = 0; i < myFishManager.myfishbag.itemList.Count; i++)
        {
            GameObject myfishtext = GameObject.Find(myFishManager.myfishbag.itemList[i].fishCountText);
            int temp = myFishManager.myfishbag.itemList[i].itemHeld;
            if (temp < 10)
            {
                myfishtext.GetComponent<TMP_Text>().text = "0" + temp.ToString();
            }
            else
            {
                myfishtext.GetComponent<TMP_Text>().text = temp.ToString();
            }
        }

    }



}
