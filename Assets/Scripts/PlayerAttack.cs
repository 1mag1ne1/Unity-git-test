using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int playerDamage;
    public float attackBoxExistTime;
    public float attackBoxStartTime;


    private Animator playerOfAn;
    public PolygonCollider2D attackCol;
    public Rigidbody2D playerRb;
    //
    
    public bool isAttack = false;
    
    
    public float attackSpeed;

    // Start is called before the first frame update
    void Start()
    {
        playerDamage = GameObject.FindGameObjectWithTag("PlayerAttack").GetComponent<PlayerAttack>().playerDamage;
        playerOfAn = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        attackCol = this.GetComponent<PolygonCollider2D>();
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        //
        
    }

    // Update is called once per frame
    void Update()
    {
        //playerAttack();
        checkAttacking();
        if (Input.GetKeyDown(KeyCode.J) && !isAttack)
        {
            isAttack = true;
            playerOfAn.SetTrigger("Attack");
            SoundManager.PlayAttackSound();
        }
    }

    void checkAttacking()
    {
        isAttack = (playerOfAn.GetCurrentAnimatorStateInfo(0).IsName("Attack"));
    }

    /*void playerAttack()
    {
        if (Input.GetKeyDown(KeyCode.J)&&!isAttacking)
        {
            StartCoroutine(startAttack());
            playerOfAn.SetTrigger("Attack");
            //playerOfAn.Play("Attack", 0);
            StartCoroutine(disableHitBox());
            playerOfAn.SetTrigger("Attack");
           

        }
    }*/
    //
    public void AttackOver()
    {
        isAttack = false;
    }
    /*IEnumerator startAttack()
    {
        yield return new WaitForSeconds(attackBoxStartTime);
        attackCol.enabled = true;
    }

    IEnumerator disableHitBox()
    {
        yield return new WaitForSeconds(attackBoxExistTime);
        attackCol.enabled = false;
        playerOfAn.SetBool("Stand", true);
    }*/

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Enemy"))
        {
            if (transform.localScale.x>= 0)
            {
                other.GetComponent<EnemyCon>().takeDamage(playerDamage,Vector2.right);
            }
            else
            {
                other.GetComponent<EnemyCon>().takeDamage(playerDamage, new Vector2(-1,0));
            }
        }
        if (other.CompareTag("Boss"))
        {
            
            other.GetComponent<Boss>().takeDamage(playerDamage);
           
        }
    }
}
