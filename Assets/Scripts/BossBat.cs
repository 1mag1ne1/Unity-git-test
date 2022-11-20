using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBat : MonoBehaviour
{
    public float speed;
    public float startWaitTime;
    public float waitTime;
    public Transform movePos;
    public Transform RightPos;
    public Transform LeftPos;
    public int health;
    public int damage;
    public SpriteRenderer sr;
    public Color orginalColor;
    public float flashTime;
    public PlayerHeath playerHeath;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        playerHeath = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHeath>();
        sr = GetComponent<SpriteRenderer>();
        orginalColor = sr.color;
        anim = GetComponent<Animator>();
        waitTime = startWaitTime;
        movePos.position = GetRandomPos();

    }
    public void takeDamage(int damage)
    {
        health -= damage;
        EnemyHealthBar.EHealthCurrent = health;
        FlashColor(flashTime);

    }
    public void FlashColor(float time)
    {
        sr.color = Color.red;
        Invoke("ResetColor", time);
    }
    public void ResetColor()
    {
        sr.color = orginalColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        transform.position = Vector2.MoveTowards(transform.position, movePos.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, movePos.position) < 0.1f)
        {
            if (waitTime <= 0)
            {
                movePos.position = GetRandomPos();
                waitTime = startWaitTime;
            }
            else
            {
                waitTime = Time.deltaTime;
            }
        }

        
    }
    Vector2 GetRandomPos()
    {
        Vector2 randPos = new Vector2(Random.Range(LeftPos.position.x, RightPos.position.x), Random.Range(LeftPos.position.y, RightPos.position.y));
        return randPos;
    }
}
