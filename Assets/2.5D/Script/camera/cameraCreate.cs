using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class cameraCreate : MonoBehaviour
{
    GameObject player;

    //public int monsternum; //�Ǫ��ƶq
    //public int flyrnum; //�����ƶq

    public GameObject[] monsterprefab;  //�Ǫ�����C��
    public GameObject[] flyprefab;      //��������C��

    public Transform monster_parent;
    public Transform fly_parent;

    public float waitTimeToCreate_camera;

    public int width = 30;   //�e
    public int height = 50;  //��

    public float time;

    private int cmmapLevel;
    void Start()
    {
        time = waitTimeToCreate_camera;
        player = GameManager.instance.player;
    }

    public void RefreshReferences()
    {
        monster_parent = GameObject.FindGameObjectWithTag("monsterSpawn").transform;
        fly_parent = GameObject.FindGameObjectWithTag("flySpawn").transform;
        cmmapLevel = GameManager.mapLevel;
    }

    void FixedUpdate()
    {
        time -= Time.deltaTime;
        if (GameManager.creatMonster)
        {
            if (time <= 0)
            {
                //Debug.Log(sceneIndex);
                switch (cmmapLevel)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        Debug.Log("生成東西了");
                        createmonster(5);
                        createfly(3);
                        break;
                    case 3:
                        Debug.Log("生成東西了");
                        createmonster(3);
                        createfly(3);
                        break;
                    case 4:
                        Debug.Log("生成東西了");
                        //createmonster(3);
                        //createfly(3);
                        break;
                    
                }
                time = waitTimeToCreate_camera;
            }
        }
    }

    void createmonster(int monsternum)
    {
        List<Vector3Int> list = new List<Vector3Int>();
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                Vector3 localPosition = new Vector3(x, y, 0);
                Vector3 worldPosition = transform.TransformPoint(localPosition);
                Vector3Int worldPositionInt = new Vector3Int(
                    Mathf.RoundToInt(worldPosition.x),
                    Mathf.RoundToInt(worldPosition.y),
                    Mathf.RoundToInt(worldPosition.z)
                );
                list.Add(worldPositionInt);
            }
        }

        for (int i = 0; i < monsternum; i++)
        {
            var index = Random.Range(0, list.Count);
            var pos = list[index];

            if (player != null)
            {
                float distance = Vector3.Distance(pos, player.transform.position);

                if (distance > 27)
                {
                    float parentZ = monster_parent.position.z;
                    Instantiate(monsterprefab[cmmapLevel-2], monster_parent).transform.position = new Vector3(pos.x, pos.y, parentZ);
                    list.RemoveAt(index);
                }
            }
            else
            {
                monsternum--;
            }
        }
    }

    void createfly(int flyrnum)
    {
        List<Vector3Int> list = new List<Vector3Int>();
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                Vector3 localPosition = new Vector3(x, y, 0);
                Vector3 worldPosition = transform.TransformPoint(localPosition);
                Vector3Int worldPositionInt = new Vector3Int(
                    Mathf.RoundToInt(worldPosition.x),
                    Mathf.RoundToInt(worldPosition.y),
                    Mathf.RoundToInt(worldPosition.z)
                );
                list.Add(worldPositionInt);
            }
        }


        for (int i = 0; i < flyrnum; i++)
        {
            var index = Random.Range(0, list.Count);
            var pos = list[index];

            if (player != null)
            {
                // �p��Z��
                float distance = Vector3.Distance(pos, player.transform.position);

                if (distance > 27)
                {
                    var preindex = Random.Range(0, flyprefab.Length);//這裡有其他蝴蝶的時候要改
                    float parentZ = fly_parent.position.z;
                    Instantiate(flyprefab[preindex], fly_parent).transform.position = new Vector3(pos.x, pos.y, parentZ);
                    list.RemoveAt(index);
                }
            }
        }
    }
}