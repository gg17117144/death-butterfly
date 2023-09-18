using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createMonster : MonoBehaviour
{

    public int monsternum; //怪物數量

    public GameObject bug_monsterprefab; //怪物物件列表

    public GameObject monster_parent;

    Transform player;

    float timer = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        player = player = GameObject.FindGameObjectWithTag("Player").transform;

        monster_parent = GameObject.FindGameObjectWithTag("monsterSpawn");

        //createmonster();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (player != null)
        {
            // 計算距離
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (GameManager.creatMonster == true)
            {
                // 如果距離大於距離，蟲洞生成
                if (distance < 20f && timer > 10)
                {
                    createmonster2();   //蟲洞生成
                    timer = 0;
                }
            }
        }
    }

    void createmonster2()
    {
        var index = Random.Range(6, monsternum);
        for (int i = 0; i < index; i++)
        {
            Instantiate(bug_monsterprefab, gameObject.transform.position, Quaternion.identity, monster_parent.transform);
        }
    }

}
