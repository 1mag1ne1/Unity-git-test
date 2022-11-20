using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddBagUI : MonoBehaviour
{
    static AddBagUI toBagUI;
    void Start()
    {
        if (!toBagUI)
        {
            toBagUI = null;
        }
        toBagUI = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnEnable()
    {
        refreshItem();
    }

    public static void addToBagUI(itemCreate item)
    {
        BagGridPerfab bagGrid = Resources.Load<BagGridPerfab>("perfabs/itemDataTest/교관목");
        GameObject bag = GameObject.FindGameObjectWithTag("trigger").transform.GetChild(0).gameObject;
        GameObject BagGrid = bag.transform.GetChild(2).gameObject;
        BagGridPerfab newItem = Instantiate(bagGrid, BagGrid.transform.position, Quaternion.identity);
        newItem.gameObject.transform.SetParent(BagGrid.transform);

        newItem.item = item;
        newItem.GetComponent<Image>().sprite = item.itemImage;
        newItem.gameObject.transform.GetChild(0).GetComponent<Text>().text = item.itemNum.ToString();
    }

    public static void refreshItem()
    {
        GameObject bag = GameObject.FindGameObjectWithTag("trigger").transform.GetChild(0).gameObject;
        GameObject BagGrid = bag.transform.GetChild(2).gameObject;
        for (int i = 0; i < BagGrid.transform.childCount; i++)
        {
            if (BagGrid.transform.childCount == 0)
            {
                break;
            }
            else
            {
                Destroy(BagGrid.transform.GetChild(i).gameObject);
            }
        }
        bagCreate bagList = Resources.Load<bagCreate>("perfabs/itemDataTest/bagData/교관");

        for (int j = 0; j < bagList.bagList.Count; j++)
        {
            addToBagUI(bagList.bagList[j]);
        }
    }

    public static void showItem(itemCreate item, int index)
    {
        GameObject bag = GameObject.FindGameObjectWithTag("trigger").transform.GetChild(0).gameObject;
        bag.transform.GetChild(1).GetComponent<Text>().text = item.itemDescription;

        GameObject BagGrid = bag.transform.GetChild(2).gameObject;

        for (int k = 0; k < BagGrid.transform.childCount; k++)
        {
            if (k == index)
            {
                if (item.use == true)
                {
                    bag.transform.GetChild(2).GetChild(k).GetChild(1).gameObject.SetActive(true);
                }
            }
            else
            {
                bag.transform.GetChild(2).GetChild(k).GetChild(1).gameObject.SetActive(false);
            }
        }
    }
}
