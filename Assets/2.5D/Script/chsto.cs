using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class chsto : MonoBehaviour
{
    public int mapnormal; //地圖物件數量

    public int mapant; //生怪洞數量

    public int mapcamp; //補給站數量

    public GameObject[] mapprefab; //地圖物件列表
    public GameObject[] antprefab; //地圖物件列表
    public GameObject[] campprefab; //地圖物件列表

    public int width;   //寬
    public int height;  //長

    public GameObject map_parent;

    public GameObject cameraCM;


    void Start()
    {
        cameraCM.SetActive(false);
        createMap();
        cameraCM.SetActive(true);

    }
    void Update()
    {

    }

    void createMap()
    {

        List<Vector3> list = new List<Vector3>();
        for (var y = transform.position.y; y < height; y++)
        {
            for (var x = transform.position.x; x < width; x++)
            {
                list.Add(new Vector3(x, y, 0));
            }
        }

        //補給站物件生成
        for (int i = 0; i < mapcamp; i++)
        {
            var index = Random.Range(0, list.Count);
            var pos = list[index];

            Collider2D overlap = Physics2D.OverlapBox(new Vector2(pos.x, pos.y), new Vector2(10, 15), 0);
            if (overlap == null)
            {
                var preindex = Random.Range(0, campprefab.Length);
                var RdScale = Random.Range(3, 6);
                campprefab[preindex].transform.localScale = new Vector2(RdScale, RdScale);
                GameObject.Instantiate(campprefab[preindex], map_parent.transform).transform.position = new Vector3(pos.x, pos.y, 10);
                list.RemoveAt(index);
            }
        }

        if (mapant != 0)
        {
            //生怪洞物件生成
            for (int i = 0; i < mapant; i++)
            {
                var index = Random.Range(0, list.Count);
                var pos = list[index];

                Collider2D overlap = Physics2D.OverlapBox(new Vector2(pos.x, pos.y), new Vector2(10, 15), 0);
                if (overlap == null)
                {
                    var preindex = Random.Range(0, antprefab.Length);
                    var RdScale = Random.Range(3, 6);
                    antprefab[preindex].transform.localScale = new Vector2(RdScale, RdScale);
                    GameObject.Instantiate(antprefab[preindex], map_parent.transform).transform.position = new Vector3(pos.x, pos.y, 10);
                    list.RemoveAt(index);
                }
            }
        }


        //一般物件生成
        for (int i = 0; i < mapnormal; i++)
        {
            var index = Random.Range(0, list.Count);
            var pos = list[index];

            Collider2D overlap = Physics2D.OverlapBox(new Vector2(pos.x, pos.y), new Vector2(10, 15), 0);
            if (overlap == null)
            {
                var preindex = Random.Range(0, mapprefab.Length);
                var RdScale = Random.Range(3, 6);
                mapprefab[preindex].transform.localScale = new Vector2(RdScale, RdScale);
                GameObject.Instantiate(mapprefab[preindex], map_parent.transform).transform.position = new Vector3(pos.x, pos.y, 10);
                list.RemoveAt(index);
            }
        }

    }

}
