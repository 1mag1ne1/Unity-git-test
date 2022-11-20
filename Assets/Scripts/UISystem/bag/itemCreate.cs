using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "物品名", menuName = "bag/创建新物品")]
public class itemCreate : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    [TextArea]
    public string itemDescription;
    public int itemNum;
    public int attackAdd;
    public int healthAdd;

    public bool use;
    public bool diejia;
}
