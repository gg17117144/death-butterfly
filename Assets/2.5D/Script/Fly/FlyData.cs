using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyData : MonoBehaviour
{
    public string ButterFlyName;    //�����W��
    public Sprite  ButterFlyImage;   //�����Ϥ�
    public int ButterFlyID;         //�����s��
    public int Fly_Energy;          //������q
    public GameObject SkillObject;  //�ޯફ��
    
    [TextArea]
    public string ButterFlyInfo;    //���~²��

    GameObject bullet;

    GameObject gun;
    GameObject player;
    float timer;

    private SpriteRenderer sprite;
    private GunType gunType;

    void Start()
    {
        gun = GameManager.instance.gun;
        gunType = gun.GetComponent<GunType>();
        player = GameManager.instance.player;
        bullet = gun.transform.Find("bullet").gameObject;
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        sprite.sortingOrder = 2 - (int)gun.transform.position.y;
    }
    public void CheckEnergy()
    {
        if (Fly_Energy <= 0)
        {
            gunType.flyNoEnergy();
            Destroy(gameObject);
        }
    }


    public void skill()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, mousePosition - bullet.transform.position);
        rotation *= Quaternion.Euler(0, 0, 90);
        
        switch (ButterFlyID)
        {
            case 0:
                break;
            case 1: //炙熱蝴蝶
                Fly_Energy -= 10;
                Instantiate(SkillObject, gun.transform.position ,  rotation);
                break;
            case 2://生命蝴蝶
                Fly_Energy -= 25;
                Instantiate(SkillObject, gun.transform.position ,  rotation);
                //Debug.Log("應該要升成了");
                break;
            case 3://氧氣蝴蝶
                Fly_Energy -= 20;
                Debug.Log("生成生成囉");
                Instantiate(SkillObject, player.transform);
                Debug.Log("真的有生成囉");
                break;
            case 4://閃電蝴蝶
                //Fly_Energy -= 10;
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;

        }
        
        CheckEnergy();
    }

    void test()
    {
        Debug.Log("嗨");
    }

    public void stop()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<FlyMove>().enabled = false;
        //GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().simulated = false;
        transform.rotation = Quaternion.Euler(0f,0f,0f);
        transform.localScale = new Vector3(0.5f, 0.5f, 1f);
    }

}
