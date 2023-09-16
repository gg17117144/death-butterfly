using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnime : MonoBehaviour
{
    GameObject player;

    Animator animator;

    AudioSource audioSource;

    private PlayerHeart playerHeart;

    public AudioClip walk;

    SpriteRenderer spriteRenderer;
    
    private void Awake()
    {
        player = gameObject;
        playerHeart = GetComponent<PlayerHeart>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sortingOrder = 3 - (int)player.transform.position.y;

        //lookmouse();
        if (playerHeart.newhp > 0)
        {
            RunWhere();
        }
        else
        {
            Dead();
        }
    }

    public void Dead()
    {
        animator.Play("Dead");
    }

    public void DeadText()
    {
        Task.ResetTask();
        GameManager.instance.relife();
    }

    void RunWhere()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        
        if (verticalInput != 0 || horizontalInput != 0)
        {
            // 檢查斜角方向
            if (verticalInput > 0 && horizontalInput < 0)       // 左上斜角的動畫和音效
            {
                //Debug.Log("左上斜");
            }
            else if (verticalInput > 0 && horizontalInput > 0)  // 右上斜角的動畫和音效
            {
                //Debug.Log("右上斜");
            }
            else if (verticalInput < 0 && horizontalInput < 0)  // 左下斜角的動畫和音效
            {
                //Debug.Log(" 左下斜");
            }
            else if (verticalInput < 0 && horizontalInput > 0)  // 右下斜角的動畫和音效
            {
                //Debug.Log("右下斜");
            }
            else if (verticalInput > 0)     // 向上的動畫和音效
            {
                animator.Play("RunUp");
            }
            else if (verticalInput < 0)     // 向下的動畫和音效
            {
                animator.Play("RunDown");
            }
            else if (horizontalInput < 0)   // 向左的動畫和音效
            {
                spriteRenderer.flipX = true;

                animator.Play("LiftSide");
            }
            else if (horizontalInput > 0)   // 向右的動畫和音效
            {
                spriteRenderer.flipX = false;

                animator.Play("RightSide");
            }

            if (!audioSource.isPlaying)
            {
                audioSource.clip = walk;
                audioSource.Play();
            }
        }
        else
        {
            animator.Play("Idle");
 
            audioSource.Stop();
        }

    }

}
