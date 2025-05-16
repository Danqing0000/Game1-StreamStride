using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishPointOnWorld : MonoBehaviour
{
    public Canvas interactionPanel;
    public bool collideState = false;
    //public GameObject fishingPanel;
    // Start is called before the first frame update
    public Inventory myBag;
    public List<SingleItem> fishingStuff = new List<SingleItem>(); //这个点位上可以钓到的物品
    public int allFishAmount; //在鱼塘中钓鱼点的位置
    //public List<Image> allPosition = new List<Image>(); // 存放所有钓鱼页面上的问号点
    public bool fished = false;
    public GameObject FishingPrefab;
    public GameObject myCanvas;
    public GameObject clonePanel;
    SingleItem tempItem;
    public GameObject broadcastPanel;

    void Start()
    {
        interactionPanel.enabled = false;
        allFishAmount = fishingStuff.Count;
        //allPosition.Add(GameObject.FindWithTag("FishingPoint").GetComponent<Image>());
        //fishingPosition = GameObject.FindGameObjectsWithTag("FishingPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if ((FishingControl.fishingState == true) && (FishingControl.fishAmount == allFishAmount) && (GameManager.keyInput == true))
        {
            gameObject.SetActive(false);
            interactionPanel.enabled = false;
        }

        if ((collideState == true) && (Input.GetKeyDown(KeyCode.F)))
        {
            //interactionPanel.enabled = false;
            panelInstantiate();
            showFishingPanel();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Detection")
        {
            interactionPanel.enabled = true;
        }
        else if (other.tag == "Player")
        {
            collideState = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Detection")
        {
            interactionPanel.enabled = false;
            collideState = false;
        }
        else if (other.tag == "Player")
        {
            collideState = false;
        }
    }

    public void panelInstantiate()
    {
        if (clonePanel == null)
        {
            clonePanel = Instantiate(FishingPrefab, myCanvas.transform);
            FishingControl.controlGameObject = gameObject.name;
            FishingControl.fishingState = false;
            FishingControl.fishAmount = allFishAmount;
        }
        else
            Debug.Log("Panel exsit already");
    }

    public void showFishingPanel()
    {
        clonePanel.SetActive(true);
    }

    public void fishAddItem()
    {
        int temp = Random.Range(0, fishingStuff.Count);
        tempItem = fishingStuff[temp];
        Debug.Log("item added");
        Debug.Log(tempItem.itemName);

        if (!myBag.itemList.Contains(tempItem)) //包里没有这项物品就将这个物品加到背包列表中
        {
            tempItem.itemHeld += 1;
            myBag.itemList.Add(tempItem);
        }
        else
        {
            tempItem.itemHeld += 1;
        }
        broadcastPanel.GetComponent<BroadcastUI>().ShowFishBroadcast(tempItem.itemName);
        fishingStuff.RemoveAt(temp);

        InventoryManager.RefreshSlot();

    }
}
