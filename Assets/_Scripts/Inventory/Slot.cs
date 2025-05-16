using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour
{
    public SingleItem slotItem;
    public Image slotImage;
    public TMP_Text slotNum;
    
    public void showDetail()
    {
        Debug.Log("The cursor entered the selectable UI element.");
        InventoryManager.InfoUpdate(slotItem.itemName, slotItem.itemInfo, slotItem.itemDetail);
    }

}
