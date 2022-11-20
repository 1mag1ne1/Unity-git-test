using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    
    public static int EHealthCurrent;
    public int EHealthMax;

    private Image healthBar;

    void Start()
    {
        healthBar=GetComponent<Image>();
        EHealthCurrent = EHealthMax;

    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (float)EHealthCurrent/(float)EHealthMax;
    }
    
}
