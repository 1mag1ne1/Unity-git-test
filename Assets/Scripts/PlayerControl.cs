using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public Animator playerAn;
    public BoxCollider2D playerFeet;
    public CapsuleCollider2D playerBody;
    private float sc = 6.4f;
    //冲刺变量
    bool isDashing = false;
    public float speed=5f;
    public float dashTime;
    public float cooldownTime = 2;
    float timer = 2;    //计时器
    public float dashSpeed;
    public GameObject dashObj;
    float startDashTimer;

    //攻击变量
    
    //
    public float max_run_speed;
    public float max_jump_speed;
    public bool isGround;
    public bool isDoubleJump;


    //收集
    public int coin = 0;//金币
    public Text coinNum;
    public Text attack;
    public Text health;
    private int primaryAttack = 3;
    private int primaryHealth = 20;
    public int maxHealth = 20;//最大生命值
    public int currentAttack;//现在的攻击力
    public int currentHealth;//现在的生命值

    public GameObject characterPanel;
    public GameObject bagPanel;
    bool isopen;
    bool right;

    //

    // Start is called before the first frame update
    void Start()
    {
        playerRb = this.GetComponent<Rigidbody2D>();
        playerAn = this.GetComponent<Animator>();
        playerFeet = this.GetComponent<BoxCollider2D>();
        playerBody = this.GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        playerFlip();
        playerRun();
        checkGround();
        playerJump();
        jumpAnimationSwitch();
        //
        Dash();
        //
        updateCoinNum();
        openMyPanel();
        openShop();
        updateCharacterPanel();

    }

    private void OnEnable()
    {
        currentHealth = primaryHealth;
        currentAttack = primaryAttack;
    }

    //

    //
 

    public void changeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }

    public void showHealth()
    {
        print(currentHealth);
    }

    //苹果


    //攻击力
    /*public void changeAttack(int amount)
    {
        attack += amount;
    }

    public void showAttack()
    {
        print(attack);
    }*/

    //攻击力

    //速度
    public void changeSpeed(int amount)
    {
        max_run_speed += amount;
    }

    public void showSpeed()
    {
        print(max_run_speed);
    }
    //速度
    private void playerFlip()
    {
        if (Input.GetAxis("Horizontal") < 0)
            transform.localScale = new Vector3(-1*sc, sc, sc);
        else if (Input.GetAxis("Horizontal") > 0)
            transform.localScale = new Vector3(sc,  sc, sc);

    }
    private void checkGround()
    {
        isGround = playerFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if (playerFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
            isDoubleJump = true;
            
    }
    private void playerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGround)
            {
                playerAn.SetBool("Up", true);
                playerRb.velocity = new Vector2(playerRb.velocity.x, max_jump_speed);
                isDoubleJump = true;

            }
            else if (isDoubleJump)
            {
                playerAn.SetBool("Up", true);
                playerRb.velocity = new Vector2(playerRb.velocity.x, max_jump_speed);
                isDoubleJump = false;
            }
            isGround = false;
        }
    }
    private void jumpAnimationSwitch()
    {
        playerAn.SetBool("Stand", false);
        if (playerAn.GetBool("Up") && playerRb.velocity.y < 0)
        {
            playerAn.SetBool("Up", false);
            playerAn.SetBool("Down", true);
        }
        else if (isGround)
        {
            playerAn.SetBool("Down", false);
            playerAn.SetBool("Stand", true);
        }
    }
    private void playerRun()
    {
        float input = Input.GetAxis("Horizontal");
        playerRb.velocity = new Vector2(input * max_run_speed, playerRb.velocity.y);
        bool isPlayerHasAxisSpeed = Mathf.Abs(playerRb.velocity.x) > Mathf.Epsilon;
        playerAn.SetBool("Run", isPlayerHasAxisSpeed);
    }
    private void Dash()
    {
        if (!isDashing)
        {
            playerRb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, playerRb.velocity.y);
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                //StartDashing
                if (timer > cooldownTime)
                {
                    dashObj.SetActive(true);
                    SoundManager.PlayDash();
                    isDashing = true;
                    startDashTimer = dashTime;
                }
                else { return; }
            }
        }
        else
        {
            startDashTimer -= Time.deltaTime;
            if (startDashTimer <= 0)
            {

                isDashing = false;
                dashObj.SetActive(false);
            }
            else
            {

                playerRb.velocity = new Vector2(transform.localScale.x * dashSpeed,0);
                timer = 0;

            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "coin")
        {
            Destroy(collision.gameObject);
            coin += 1;
        }
    }
    void updateCoinNum()
    {
        coinNum.text = coin.ToString();
    }

    void updateCharacterPanel()
    {
        attack.text = currentAttack.ToString();
        health.text = currentHealth.ToString();
    }

    void openMyPanel()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isopen = !isopen;
            characterPanel.SetActive(isopen);
        }
    }
    void openShop()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            right = !right;
            bagPanel.SetActive(right);
        }
    }

}
