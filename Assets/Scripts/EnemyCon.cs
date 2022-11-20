using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCon : MonoBehaviour
{
    public Rigidbody2D enemyRb;

    public float enemySpeed;
    public float min_r_enemyToPlay;
    public float min_r_enemyStartAttackPlay;
    //
    public float speed;
    private Vector2 direction;
    private bool isHit;
    private AnimatorStateInfo info;
    private Animator animator;

    
    public int health;
    public float deathTime;
    public GameObject dropCoin;
    // Start is called before the first frame update
    public void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        //
        animator = transform.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    public void Update()
    {
        EFlip();
        isPlayerInEnemyArea();
        EFollow();
        //
        enemyDeath();
        info = animator.GetCurrentAnimatorStateInfo(0);
        if (isHit)
        {
            enemyRb.velocity = direction * speed;
            if (info.normalizedTime >= 0.6f)
            {
                isHit = false;
                
            }
        }
    }
    private void enemyDeath()
    {
        if (health <= 0)
        {
            StartCoroutine(deathWait());
        
        }
    }

    IEnumerator deathWait()
    {
        animator.SetTrigger("EDeath");
        yield return new WaitForSeconds(deathTime);
        Instantiate(dropCoin,transform.position,Quaternion.identity);
        Destroy(gameObject);
        
  
    }
    public void takeDamage(int playerDamage, Vector2 direction)
    {
        animator.SetTrigger("EHurt");
        SoundManager.PlayEnemyHurt();
        isHit = true;
        this.direction = direction;
        health -= playerDamage;

    }

    private void EFollow()
    {
   
       if (isPlayerInEnemyArea())
        {
            //print("LOOK OUT!");
            float X = GameObject.FindGameObjectWithTag("Player").transform.localPosition.x - transform.localPosition.x;
            X = X == 0 ? 0 : X / Mathf.Abs(X);
            enemyRb.velocity = new Vector2(X * enemySpeed, enemyRb.velocity.y);
       }
    }
    private void EFlip()
    {
        if (enemyRb.velocity.x > 0)
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        else if (enemyRb.velocity.x < 0)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
    }
    private bool isPlayerInEnemyArea()
    {
        float X = transform.localPosition.x - GameObject.FindGameObjectWithTag("Player").transform.localPosition.x;
        float R = Mathf.Abs(X);
        if (R < min_r_enemyToPlay)
            return true;
        else
            return false;
    }

}
