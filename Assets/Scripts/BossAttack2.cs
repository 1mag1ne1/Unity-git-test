using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack2 : MonoBehaviour
{
    public int damage;
    public float time;
    public float startTime;
  
    private PolygonCollider2D polygon;
    private Animator anim;
    private PlayerHeath playerHeath;
    // Start is called before the first frame update
    void Start()
    {
     
        polygon = GetComponent<PolygonCollider2D>();
        anim = GameObject.FindGameObjectWithTag("Boss").GetComponent<Animator>();
        playerHeath = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHeath>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Boss.getN()==2&&!Boss.b)

        {
            Boss.setN();
                anim.SetTrigger("BossAttack2");
                attack();
            
        }
    }
    void attack()
    {

        StartCoroutine(startAttack());
        
    }
    IEnumerator startAttack()
    {
        yield return new WaitForSeconds(startTime);
        polygon.enabled = true;
        StartCoroutine(diasbleHitBox());
    }
    IEnumerator diasbleHitBox()
    {
        yield return new WaitForSeconds(time);
        polygon.enabled = false;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            playerHeath.DamagePlayer(damage);

        }

    }
}

