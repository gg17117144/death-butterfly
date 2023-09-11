using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class chsto : MonoBehaviour
{
    public int mapnormal; //�a�Ϫ���ƶq

    public int mapant; //�ͩǬ}�ƶq

    public int mapcamp; //�ɵ����ƶq

    public GameObject[] mapprefab; //�a�Ϫ���C��
    public GameObject[] antprefab; //�a�Ϫ���C��
    public GameObject[] campprefab; //�a�Ϫ���C��

    public int width;   //�e
    public int height;  //��

    public GameObject map_parent;

    public GameObject background;

    private int maplever;
    void Start()
    {
        maplever = GameManager.instance.sceneIndex;
        Debug.Log(maplever);
        background.SetActive(false);
        switch (maplever)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                createMap(3,6);
                break;
            case 3:
                createMap(3,6);
                break;
            case 4:
                createMap(1,3);
                break;
            case 5:
                break;
            
            
        }

        
        background.SetActive(true);

    }
    void Update()
    {

    }

    void createMap(int RdScale01,int RdScale02)
    {
        List<Vector3> list = new List<Vector3>();
        for (var y = transform.position.y; y < height; y++)
        {
            for (var x = transform.position.x; x < width; x++)
            {
                list.Add(new Vector3(x, y, 0));
            }
        }

        //�ɵ�������ͦ�
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

        //Debug.Log(mapant);
        if (mapant != 0)
        {
            //�ͩǬ}����ͦ�
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


        //�@�몫��ͦ�
        for (int i = 0; i < mapnormal; i++)
        {
            var index = Random.Range(0, list.Count);
            var pos = list[index];

            Collider2D overlap = Physics2D.OverlapBox(new Vector2(pos.x, pos.y), new Vector2(20, 40), 0);
            if (overlap == null)
            {
                var preindex = Random.Range(0, mapprefab.Length);
                var RdScale = Random.Range(RdScale01, RdScale02);
                mapprefab[preindex].transform.localScale = new Vector2(RdScale, RdScale);
                GameObject.Instantiate(mapprefab[preindex], map_parent.transform).transform.position = new Vector3(pos.x, pos.y, 10);
                list.RemoveAt(index);
            }
        }

    }

}
