using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyData : MonoBehaviour
{
    public string ButterFlyName;    //蝴蝶名稱
    public Sprite ButterFlyImage;   //蝴蝶圖片
    public int ButterFlyID;         //蝴蝶編號
    public int Fly_Energy;          //蝴蝶能量
    public GameObject SkillObject;  //技能物件
    [TextArea]
    public string ButterFlyInfo;    //物品簡介

    GameObject bullet;

    GameObject Canvas;
    int aa;

    GameObject gun;

    float timer;

    void Start()
    {
        gun = GameObject.FindGameObjectWithTag("gun");
        bullet = GameObject.FindGameObjectWithTag("bullet");
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
    }
    private void Awake()
    {

    }
    void Update()
    {
        aa = gun.GetComponent<GunType>().aa;
        CheckEnergy();
    }
    public void CheckEnergy()
    {
        if (Fly_Energy <= 0)
        {
            //Debug.Log("蝴蝶能量用完");
            gun.GetComponent<GunType>().FlyTank[aa] = null;
            //gun.GetComponent<GunType>().butterfltdatalist.butterflydataList[aa] = gun.GetComponent<GunType>().FlyTank[aa];
            Canvas.GetComponent<UIControl>().reloadUI();
            Destroy(this.gameObject);
        }
    }


    public void skill()
    {
        switch (ButterFlyID)
        {
            case 0:
                //Debug.Log(ButterFlyID);
                break;
            case 1: //熾熱蝴蝶
                //Debug.Log(ButterFlyID);
                Instantiate(SkillObject, bullet.transform.position , bullet.transform.rotation);

                Fly_Energy = Fly_Energy - 10;

                break;
            case 2:
                Debug.Log(ButterFlyID);
                Fly_Energy = Fly_Energy - 10;
                break;
            case 3:
                Debug.Log(ButterFlyID);
                Fly_Energy = Fly_Energy - 10;
                break;
            case 4:
                Debug.Log(ButterFlyID);
                Fly_Energy = Fly_Energy - 10;
                break;
            case 5:
                Debug.Log(ButterFlyID);
                break;
            case 6:
                Debug.Log(ButterFlyID);
                break;
            case 7:
                Debug.Log(ButterFlyID);
                break;
            default:
                Debug.Log("null");
                break;
        }
    }

}
