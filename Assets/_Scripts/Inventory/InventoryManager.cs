using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager myInventoryManager;

    public Inventory mybag;
    public Inventory myfishbag;
    public GameObject slotGrid;
    public Slot slotPrefab;
    public TMP_Text itemName;
    public TMP_Text itemInfomation;
    public Image itemDetail;
    public TMP_Text itemBroadcast;
    public GameObject broadcastPanel;

    private void Awake()
    {
        if (myInventoryManager != null)
        {
            Destroy(this);
        }
        myInventoryManager = this;
    }

    private void OnEnable()
    {
        RefreshSlot();
        //myInventoryManager.itemInfomation.text = "";
    }

    public static void CreateNewSlot(SingleItem item)
    {
        //TODO: 生成一个新的slot类的物体，这个物体使用slotprefab为模板（？），transform与slotgrid相关联
        Slot newitem = Instantiate(myInventoryManager.slotPrefab, myInventoryManager.slotGrid.transform);
        //建立父子级关系，新生成的newitem属于slotgrid的子集
        newitem.gameObject.transform.SetParent(myInventoryManager.slotGrid.transform);

        //传输数据 将列表中item的信息传给slot
        newitem.slotItem = item;
        newitem.slotImage.sprite = item.itemImage;
        newitem.slotNum.text = item.itemHeld.ToString();
    }

    public static void RefreshSlot()
    {
        for (int i = 0; i < myInventoryManager.slotGrid.transform.childCount; i++)
        {
            Destroy(myInventoryManager.slotGrid.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < myInventoryManager.mybag.itemList.Count; i++)
        {
            CreateNewSlot(myInventoryManager.mybag.itemList[i]);
        }
        //myInventoryManager.itemInfomation.text = "";
        Debug.Log("refresh slot");

        if (myInventoryManager.mybag.itemList.Count > 0)
        {
            InfoUpdate(myInventoryManager.mybag.itemList[0].itemName, myInventoryManager.mybag.itemList[0].itemInfo, myInventoryManager.mybag.itemList[0].itemDetail);
        }

    }

    public static void InfoUpdate(string itemName, string itemInfo, Sprite itemDetail) //function call on Slot.cs
    {
        myInventoryManager.itemName.text = itemName;
        myInventoryManager.itemInfomation.text = itemInfo;
        myInventoryManager.itemDetail.sprite = itemDetail;
    }

    public static void AddItemBroadcast(string thisitemName)
    {
        //myInventoryManager.itemBroadcast.text = thisitemName;
        //myInventoryManager.broadcastPanel.SetActive(true);
        myInventoryManager.broadcastPanel.GetComponent<BroadcastUI>().ShowBroadcast(thisitemName);
    }

    public static void RefreshFishSlot()
    {
        for (int i = 0; i < myInventoryManager.myfishbag.itemList.Count; i++)
        {
            GameObject myfishtext = GameObject.Find(myInventoryManager.myfishbag.itemList[i].fishCountText);
            int temp = myInventoryManager.myfishbag.itemList[i].itemHeld;
            if (temp < 10)
            {
                myfishtext.GetComponent<TMP_Text>().text = "0" + temp.ToString();
            }
        }

    }
}
