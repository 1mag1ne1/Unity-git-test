using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchItem : MonoBehaviour
{
    public Transform fatherItem;
    public bagCreate playerBag;
    public itemCreate item;

    void Start()
    {
        fatherItem = GetComponent<Transform>();
        if (fatherItem.name.Contains("("))
        {
            string[] strArray = fatherItem.name.Split(' ');
            fatherItem.name = strArray[0];

        }
        playerBag = Resources.Load<bagCreate>("perfabs/itemDataTest/bagData/背包");
        item = Resources.Load<itemCreate>("perfabs/itemDataTest/itemData/" + fatherItem.name);
    }

    private void OnEnable()
    {
        playerBag.bagList.Clear();
        item.itemNum = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        fatherItem = GetComponent<Transform>();
        PlayerControl player01 = GameObject.FindGameObjectWithTag("Player").transform.gameObject.GetComponent<PlayerControl>();
        if (fatherItem.name.Contains("attack"))
        {
            if (player01.coin >= 2)
            {
                if (collision.gameObject.CompareTag("Player"))
                {
                    updateData();
                    Destroy(fatherItem.gameObject);
                    player01.coin -= 2;
                }
            }
        }
        else if (fatherItem.name.Contains("health"))
        {
            if (player01.coin >= 1)
            {
                if (collision.gameObject.CompareTag("Player"))
                {
                    updateData();
                    Destroy(fatherItem.gameObject);
                    player01.coin -= 1;
                }
            }

        }
    }
    public void updateData()
    {
        SpriteRenderer sprite = fatherItem.gameObject.GetComponent<SpriteRenderer>();
        item.itemImage = sprite.sprite;
        if (!playerBag.bagList.Contains(item))
        {
            if (playerBag.bagList.Count <= 18)
            {
                playerBag.bagList.Add(item);
            }
            else
            {
                print("背包已满");
            }
        }
        else
        {
            if (item.diejia == true)
            {
                item.itemNum += 1;
            }
            else
            {
                if (playerBag.bagList.Count <= 18)
                {
                    playerBag.bagList.Add(item);
                }
                else
                {
                    print("背包已满");
                }
            }
        }

        AddBagUI.refreshItem();
    }
}
