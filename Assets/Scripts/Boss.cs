using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boss : MonoBehaviour
{
    public int damage;
    public int health;
    public SpriteRenderer sr;
    public Color orginalColor;
    public float flashTime;
    public PlayerHeath playerHeath;
    public float time=0;
    public float startTime=0;
    private Animator anim;
    private Transform trans;
    private static int n;
    private static int n1;
    public static bool b;
    // Start is called before the first frame update
    public void Start()
    {
        playerHeath = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHeath>();
        sr = GetComponent<SpriteRenderer>();
        orginalColor = sr.color;
        anim = GetComponent<Animator>();

        trans = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
   public void Update()
    {
        attack();
        
        if (health <= 50)
        {
            anim.SetBool("BossIdle", false);
            anim.SetBool("BossIdle2",true);
            anim.SetBool("BossIdle3", true);
            b = true;
        }
        if (health <= 0)
        {
            anim.SetTrigger("BossDeath");
            float stime = Time.deltaTime;
            if (Time.deltaTime - stime > 1.1)
            {
                Destroy(gameObject);
            }
        }
    }
    public static int getN()
    {
       
        return n;
    }
    public static void setN()
    {

        n = 0;
    }
   
  
    public void takeDamage(int damage)
    {
        health -= damage;
        SoundManager.PlayBossHurt();
        EnemyHealthBar.EHealthCurrent=health;
        FlashColor(flashTime);

    }
    public void FlashColor(float time)
    {
        sr.color = Color.red;
        Invoke("ResetColor",time);
    }
    public void ResetColor()
    {
        sr.color = orginalColor;
    }
   void attack()
    {
        if (health <= 50)
        {
            if (Math.Abs(transform.position.x - trans.position.x) < 5)
            {
                

                    time = Time.time;
                    if (time - startTime > 3)
                    {
                        n = Random.Range(1,4);
                        startTime = time;

                    }
       
                anim.SetBool("BossIdle3", true);

            }
        }
        else
        {
            if (Math.Abs(transform.position.x - trans.position.x) < 5)
            {
                if (Math.Abs(transform.position.x - trans.position.x) < 2)
                {
                    time = Time.time;
                    if (time - startTime > 3)
                    {
                        n1 = Random.Range(0, 10);
                        if (n1 < 6)
                        {
                            n = 2;
                        }
                        else if (n1 < 8)
                        {
                            n = 1;
                        }
                        else if (n1 < 10)
                        {
                            n = 3;
                        }
                        startTime = time;

                    }
                }
                else
                {

                    time = Time.time;
                    if (time - startTime > 3)
                    {
                        n = Random.Range(1, 4);
                        startTime = time;

                    }
                }
                anim.SetBool("BossIdle", true);

            }
        }
    
    }
   
    


}
