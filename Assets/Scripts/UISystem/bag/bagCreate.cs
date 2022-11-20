using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "新背包", menuName = "bag/创建新背包")]
public class bagCreate : ScriptableObject
{
    public List<itemCreate> bagList = new List<itemCreate>();
}
