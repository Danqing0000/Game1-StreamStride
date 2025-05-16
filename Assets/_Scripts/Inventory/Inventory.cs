using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[CreateAssetMenu(fileName = "Inventory", menuName = "Invontory/New Bag")]
public class Inventory : ScriptableObject
{
    public List<SingleItem> itemList = new List<SingleItem>();
}
