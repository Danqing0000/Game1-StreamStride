using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "SingleItem", menuName = "Invontory/SingleItem")]
public class SingleItem : ScriptableObject
{
    public string itemName;
    public Sprite itemImage; //icon
    public Sprite itemDetail;
    public int itemHeld; //held in the bag
    public int itemConfig; //default number 
    [TextArea]
    public string itemInfo;
    public bool isFish;
    public string fishCountText;
}
