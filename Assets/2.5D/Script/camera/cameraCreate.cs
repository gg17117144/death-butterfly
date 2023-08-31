using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraCreate : MonoBehaviour
{
    GameObject player;

    public int monsternum; //怪物數量
    public int flyrnum; //蝴蝶數量

    public GameObject[] monsterprefab;  //怪物物件列表
    public GameObject[] flyprefab;      //蝴蝶物件列表

    public Transform monster_parent;
    public Transform fly_parent;

    public float waitTimeToCreate_camera;

    public int width = 30;   //寬
    public int height = 50;  //長

    public float time = 0;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        monster_parent = GameObject.FindGameObjectWithTag("monsterSpawn").transform;
        fly_parent = GameObject.FindGameObjectWithTag("flySpawn").transform;
    }

    void FixedUpdate()
    {
        time += Time.deltaTime;
        if (GameManager.creatMonster == true)
        {
            if (time >= waitTimeToCreate_camera)
            {
                Debug.Log("生成蝴蝶拉");
                createmonster();    //範圍生成
                createfly();
                time = 0;

            }
        }
    }

    void createmonster()
    {
        List<Vector3Int> list = new List<Vector3Int>();
        //計算範圍
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
                // 計算距離
                float distance = Vector3.Distance(pos, player.transform.position);

                if (distance > 27)
                {
                    //沒有偵測有沒有 Collider2D 而在限定範圍內隨機生成
                    
                    var preindex = Random.Range(0, monsterprefab.Length);
                    float parentZ = monster_parent.position.z;
                    GameObject.Instantiate(monsterprefab[preindex], monster_parent).transform.position = new Vector3(pos.x, pos.y, parentZ);
                    list.RemoveAt(index);
                    
                    //有偵測 Collider2D 而在限定範圍內隨機生成
                    /*
                    Collider2D overlap = Physics2D.OverlapBox(new Vector2(pos.x, pos.y), new Vector2(3, 5), 0);
                    if (overlap == null)
                    {
                        var preindex = Random.Range(0, monsterprefab.Length);
                        //生怪物
                        GameObject.Instantiate(monsterprefab[preindex], monster_parent).transform.position = pos;
                        list.RemoveAt(index);
                    }
                    */
                }
            }
            else
            {
                monsternum--;
            }
        }
    }

    void createfly()
    {
        List<Vector3Int> list = new List<Vector3Int>();
        //計算範圍
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
                // 計算距離
                float distance = Vector3.Distance(pos, player.transform.position);

                if (distance > 27)
                {
                    //沒有偵測有沒有 Collider2D 而在限定範圍內隨機生成

                    var preindex = Random.Range(0, monsterprefab.Length);
                    float parentZ = fly_parent.position.z;
                    GameObject.Instantiate(flyprefab[preindex], fly_parent).transform.position = new Vector3(pos.x, pos.y, parentZ);
                    list.RemoveAt(index);

                    
                    //有偵測 Collider2D 而在限定範圍內隨機生成
                    /*
                    Collider2D overlap = Physics2D.OverlapBox(new Vector2(pos.x, pos.y), new Vector2(3, 5), 0);
                    if (overlap == null)
                    {
                        var preindex = Random.Range(0, monsterprefab.Length);
                        //生蝴蝶
                        GameObject.Instantiate(flyprefab[preindex], fly_parent).transform.position = pos;
                        list.RemoveAt(index);
                    }
                    */
                }
                else
                {
                    
                }
            }
        }
    }
}