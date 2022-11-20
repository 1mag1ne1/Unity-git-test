using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHeath : MonoBehaviour
{
    public int health;
    public float dyingTime;

    public Rigidbody2D playerRb;
    public Animator playerAn;
    //�ܹ�����
    
    public bool isInvincible=true;
    float invincibleTimer=2f;
    public float timeInvincible = 10.0f;
    public bool isHit;
    private Vector2 direction;
    
    public float speed;
    private AnimatorStateInfo info;

    // Start is called before the first frame update
    void Start()
    {
        playerAn = this.GetComponent<Animator>();
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isPlayerDeath();
        
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        
        info = playerAn.GetCurrentAnimatorStateInfo(0);
        if (isHit)
        {
            playerRb.velocity = direction * speed;
            if (info.normalizedTime >= 0.6f)
            {
                isHit = false;
            }
        }
    }

    private void isPlayerDeath()
    {
        if(health <= 0)
        {
            StartCoroutine(dyingWait());
        }
    }

    IEnumerator dyingWait()
    {
        playerRb.constraints = RigidbodyConstraints2D.FreezePosition;
        playerAn.SetBool("Death", true);
        yield return new WaitForSeconds(dyingTime);
        SceneManager.LoadScene(0);
    }

    public void DamagePlayer(int damage)
    {
        
        playerAn.SetTrigger("Hurt");
        
        
        if (isInvincible)
            return;

        isInvincible = true;
        invincibleTimer = timeInvincible;
        isHit = true;
        HealthBar.HealthCurrent=Mathf.Clamp(HealthBar.HealthCurrent - damage, 0, 20);
        health -= damage;
        SoundManager.PlayPlayerHurt();
        this.direction = new Vector2(-transform.localScale.x, 0);
        


    }
    




}
