using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookMouse : MonoBehaviour
{
    GameObject gun;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        gun = GameObject.FindGameObjectWithTag("gun");
    }
    void Update()
    {
        if (GameManager.isStoping == false)
        {
            Looking();
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
            // 正右邊
            Debug.Log("正右邊");
        }
        else if (angle >= 22.5f && angle < 67.5f)
        {
            // 右上角
            Debug.Log("右上角");
        }
        else if (angle >= 67.5f && angle < 112.5f)
        {
            // 正上方
            Debug.Log("正上方");
        }
        else if (angle >= 112.5f && angle < 157.5f)
        {
            // 左上方
            Debug.Log("左上方");
        }
        else if (angle >= 157.5f && angle > -157.5f)
        {
            // 正左邊
            Debug.Log("正左邊");
        }
        else if (angle >= -157.5f && angle < -112.5f)
        {
            // 左下方
            Debug.Log("左下方");
        }
        else if (angle >= -112.5f && angle < -67.5f)
        {
            // 正下方
            Debug.Log("正下方");
        }
        else if (angle >=  -67.5f && angle < -22.5f )
        {
            // 右下方
            Debug.Log("右下方");
        }
        
    }

}
