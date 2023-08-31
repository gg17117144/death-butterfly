using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    GameObject player; //角色

    Animator animator;

    public Vector3 offset; //偏移量
    public float smoothSpeed = 5f; //平滑移動的速度

    private Vector3 targetPosition; // 目標

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        animator = gameObject.GetComponent<Animator>();
    }
    private void LateUpdate()
    {
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = (int)player.GetComponent<SpriteRenderer>().sortingOrder;

        targetPosition = player.transform.position + offset; // 默认使用偏移量

        if (player.GetComponent<SpriteRenderer>().flipX)
        {
            // 当角色翻转时，将偏移量的 x 分量取反
            offset.x = Mathf.Abs(offset.x);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            // 当角色不翻转时，将偏移量的 x 分量设置为绝对值
            offset.x = -Mathf.Abs(offset.x);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        // 使用插值平滑地移动跟随物体
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            animator.SetBool("runing", true);
        }
        else
        {
            animator.SetBool("runing", false);
        }
    }
}
