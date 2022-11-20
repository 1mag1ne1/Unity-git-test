using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioSource audioSrc;
    public static AudioClip audioAtt;
    public static AudioClip playerHurt;
    public static AudioClip enemyHurt;
    public static AudioClip dash;
    public static AudioClip bossHurt;
    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        audioAtt = Resources.Load<AudioClip>("Attack");
        playerHurt = Resources.Load<AudioClip>("PlayerHurt");
        enemyHurt = Resources.Load<AudioClip>("EnemyHurt");
        dash = Resources.Load<AudioClip>("Dash");
        bossHurt = Resources.Load<AudioClip>("BossHurt");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void PlayBossHurt()
    {
        audioSrc.PlayOneShot(bossHurt);
    }
    public static void PlayAttackSound()
    {
        audioSrc.PlayOneShot(audioAtt);
    }
    public static void PlayPlayerHurt()
    {
        audioSrc.PlayOneShot(playerHurt);
    }
    public static void PlayEnemyHurt()
    {
        audioSrc.PlayOneShot(enemyHurt);
    }
    public static void PlayDash()
    {
        audioSrc.PlayOneShot(dash);

    }
}
