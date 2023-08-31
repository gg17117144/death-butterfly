using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public int scenesNum;

    public bool isMinCamera;

    public Transform playerTransform;

    private void Start()
    {
        if (!isMinCamera)
        {
            GameManager.mainCamera = gameObject;
        }

        if (GameObject.FindGameObjectWithTag("Player"))
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

    }

    private void Update()
    {
        if (playerTransform != null && isMinCamera)
        {
            // 將小地圖移動到角色位置
            transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);

        }
    }
}
