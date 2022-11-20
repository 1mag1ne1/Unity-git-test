using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy
{
    public Rigidbody2D enemyRb;
    public Animator enemyAn;

    public float min_r_AttackingArea;
    public float attackingTime;
    public float deathTime;
    public bool isAttacking;
    public bool isHurting;

    public GameObject dropCoin;
    //
   






    // Start is called before the first frame update
    public void Start()
    {

        isAttacking = false;
        isHurting = false;

        enemyRb = GetComponent<Rigidbody2D>();
        enemyAn = GetComponent<Animator>();
        //
        

    }

    // Update is called once per frame
    public void Update()
    {
        base.Update();
        enemyJump();
        enemyAttack();
        if(health<=0){
            print(1);
            Instantiate(dropCoin,transform.position,Quaternion.identity);
        }
       // enemyDeath();
        //d
        


    }


   /* private void enemyDeath()
    {
        if (health < 0)
        {
            StartCoroutine(deathWait());
            //Destroy(gameObject);
        }
    }

    IEnumerator deathWait()
    {
        enemyAn.SetTrigger("EDeath");
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }*/

    private void enemyAttack()
    {
        if (isPlayerInEnemyAttackArea())
        {
            enemyRb.constraints = RigidbodyConstraints2D.FreezePositionX;
            enemyRb.constraints = RigidbodyConstraints2D.FreezePositionY;
            enemyAn.SetTrigger("EAttack");
            enemyAn.SetBool("EStand", false);
            StartCoroutine(attackingWait());
            //


        }
    }
    //
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isPlayerInEnemyAttackArea())
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.GetComponent<PlayerHeath>().DamagePlayer(base.damage);
            }
        }
    }

    IEnumerator attackingWait()
    {
        yield return new WaitForSeconds(attackingTime);
        enemyRb.constraints = RigidbodyConstraints2D.None;
        enemyRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        if (!isPlayerInEnemyAttackArea())
            enemyAn.SetBool("EStand", true);


    }

    private void enemyJump()
    {
        if (enemyRb.velocity.x != 0 && !isAttacking && !isHurting)
        {
            enemyAn.SetBool("EStand", false);
            enemyAn.SetBool("EJump", true);
        }
        else if (enemyRb.velocity.x == 0)
        {
            enemyAn.SetBool("EJump", false);
            enemyAn.SetBool("EStand", true);
        }
    }

    private bool isPlayerInEnemyAttackArea()
    {
        float X = transform.localPosition.x - GameObject.FindGameObjectWithTag("Player").transform.localPosition.x;
        float R = Mathf.Abs(X);
     
        if (R < min_r_AttackingArea)
            return true;
        else
            return false;
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.CompareTag("PlayerAttack"))
    //    {
    //        enemyAn.SetTrigger("EHurt");
    //      
    //
    //    }
    //}
    
    
}
    
