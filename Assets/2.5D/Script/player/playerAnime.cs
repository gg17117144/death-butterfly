using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnime : MonoBehaviour
{
    GameObject player;

    Animator animator;

    AudioSource audioSource;

    public AudioClip walk;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }


    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3 - (int)player.transform.position.y;

        //lookmouse();

        RunWhere();
    }

    void lookmouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 characterPosition = transform.position;
        Vector3 offset = mousePosition - Camera.main.WorldToScreenPoint(characterPosition);

        if (offset.x > 0)
        {
            // 鼠标在右侧，将角色图像向右转向
            spriteRenderer.flipX = false; // 不翻转图像
        }
        else if (offset.x < 0)
        {
            // 鼠标在左侧，将角色图像向左转向
            spriteRenderer.flipX = true; // 翻转图像
        }
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

                animator.Play("Side");
            }
            else if (horizontalInput > 0)   // 向右的動畫和音效
            {
                spriteRenderer.flipX = false;

                animator.Play("Side");
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
