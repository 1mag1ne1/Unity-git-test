using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "�±���", menuName = "bag/�����±���")]
public class bagCreate : ScriptableObject
{
    public List<itemCreate> bagList = new List<itemCreate>();
}
