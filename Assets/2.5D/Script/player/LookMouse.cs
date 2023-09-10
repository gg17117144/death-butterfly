using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookMouse : MonoBehaviour
{
    GameObject gun;

    private GameObject player;

    private Animator animator;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        gun = GameObject.FindGameObjectWithTag("gun");

        animator = gun.GetComponent<Animator>();
    }

    void Update()
    {
        if (GameManager.isStoping == false)
        {
            if (gun.activeSelf)
            {
                Looking();
            }
        }
    }

    void Looking()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, transform.position.z));
        
        Vector3 lookDir = mouseWorldPos - transform.position; 
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = rotation;

        //Debug.Log($"angle = {angle} , rotation{rotation}");

        if (angle >= -22.5 && angle < 22.5f)
        {
            // 正右邊 -22.5~22.5
            //Debug.Log("正右邊");
            animator.Play("RightSide");
            //gun.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (angle >= 22.5f && angle < 67.5f)
        {
            // 右上角 22.5~67.5
            //Debug.Log("右上角");
            animator.Play("RightUP");
            //gun.transform.rotation = Quaternion.Euler(0, 0, -45);
        }
        else if (angle >= 67.5f && angle < 112.5f)
        {
            // 正上方 67.5~112.5
            //Debug.Log("正上方");
            animator.Play("UP");
            //gun.transform.rotation = Quaternion.Euler(0, 0, 67.5f);
        }
        else if (angle >= 112.5f && angle < 157.5f)
        {
            // 左上方 112.5~157.5
            //Debug.Log("左上方");
            animator.Play("LiftUP");
            //gun.transform.rotation = Quaternion.Euler(0, 0, -67.5f);
        }
        else if (angle >= 157.5f || angle < -157.5f)
        {
            // 正左邊 
            //Debug.Log("正左邊");
            animator.Play("LiftSide");
            //gun.transform.rotation = Quaternion.Euler(0, 0, -90f);
        }
        else if (angle >= -157.5f && angle < -112.5f)
        {
            // 左下方 -112.5~-157.5
            //Debug.Log("左下方");
            animator.Play("LiftDown");
            //gun.transform.rotation = Quaternion.Euler(0, 0, 225);
        }
        else if (angle >= -112.5f && angle < -67.5f)
        {
            // 正下方 -67.5~-112.5
            //Debug.Log("正下方");
            animator.Play("Down");
            //gun.transform.rotation = Quaternion.Euler(0, 0, 270);
        }
        else if (angle >=  -67.5f && angle < -22.5f )
        {
            // 右下方 -22.5~-47.5
            //Debug.Log("右下方");
            animator.Play("RightDown");
            //gun.transform.rotation = Quaternion.Euler(0, 0, 315);
        }
    }
}
