using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagGridPerfab : MonoBehaviour
{
    public itemCreate item;

    public void itemClicked()
    {
        int index = transform.GetSiblingIndex();
        AddBagUI.showItem(item, index);
    }

    public void itemUseClicked()
    {
        itemUse();
    }

    public void itemUse()
    {
        int index = transform.GetSiblingIndex();
        GameObject bag = GameObject.FindGameObjectWithTag("trigger").transform.GetChild(0).gameObject;
        GameObject bagGrid = bag.transform.GetChild(2).gameObject;
        bagCreate bagList01 = Resources.Load<bagCreate>("perfabs/itemDataTest/bagData/±³°ü");
        PlayerControl playertest = GameObject.FindGameObjectWithTag("Player").transform.gameObject.GetComponent<PlayerControl>();
        if (playertest.currentHealth < playertest.maxHealth)
        {
            int usedHealth = bagList01.bagList[index].healthAdd + playertest.currentHealth;
            playertest.currentHealth = usedHealth;
        }
        else
        {
            playertest.currentHealth = playertest.maxHealth;
        }
        int usedAttack = bagList01.bagList[index].attackAdd + playertest.currentAttack;
        playertest.currentAttack = usedAttack;

        if (bagList01.bagList[index].itemNum == 1)
        {

            bagList01.bagList.Remove(bagList01.bagList[index]);
            Destroy(bagGrid.transform.GetChild(index).gameObject);
            AddBagUI.refreshItem();
        }
        else
        {

            bagList01.bagList[index].itemNum -= 1;
            AddBagUI.refreshItem();
        }

    }
}
