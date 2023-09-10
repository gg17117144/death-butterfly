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

    GameObject Canvas;
    int aa;

    GameObject gun;

    float timer;

    private SpriteRenderer sprite;
    private GunType gunType;
    private UIControl _uiControl;
    void Start()
    {
        //gun = GameObject.FindGameObjectWithTag("gun");
        gun = GameManager.instance.gun;
        //bullet = GameObject.FindGameObjectWithTag("bullet");
        bullet = gun.transform.Find("bullet").gameObject;
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        sprite = gameObject.GetComponent<SpriteRenderer>();
        aa = gun.GetComponent<GunType>().aa;
        gunType = gun.GetComponent<GunType>();
        _uiControl = Canvas.GetComponent<UIControl>();
    }
    
    void Update()
    {
        CheckEnergy();
        sprite.sortingOrder = 2 - (int)gun.transform.position.y;
    }
    public void CheckEnergy()
    {
        if (Fly_Energy <= 0)
        {
            //Debug.Log("������q�Χ�");
            gunType.FlyTank[aa] = null;
            //gun.GetComponent<GunType>().butterfltdatalist.butterflydataList[aa] = gun.GetComponent<GunType>().FlyTank[aa];
            _uiControl.ReloadGunUI();
            Destroy(this.gameObject);
        }
    }


    public void skill()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        switch (ButterFlyID)
        {
            case 0:
                //Debug.Log(ButterFlyID);
                break;
            case 1: //�K������
                //Debug.Log(ButterFlyID);
                Quaternion rotation = Quaternion.LookRotation(Vector3.forward, mousePosition - bullet.transform.position);
                rotation *= Quaternion.Euler(0, 0, 90);
                Instantiate(SkillObject, gun.transform.position ,  rotation);

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
