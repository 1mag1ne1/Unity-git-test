using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "��Ʒ��", menuName = "bag/��������Ʒ")]
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
