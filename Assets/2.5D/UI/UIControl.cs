using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    GameObject gun;
    GameObject canvas;

    public Image[] FlyTank_Image;           //蝴蝶圖片槽[]
    Image[] bagFlyTank_Image;

    //刪除的蝴蝶能量調顯示UI
    /*
    public GameObject[] Flycom;             //蝴蝶能量框架槽[]
    public Image[] FlyTank_EnergyImage;     //蝴蝶能量圖片槽[]
    public Slider[] FlyTank_EnergySlider;   ////蝴蝶能量拉條槽[]
    */


    public Sprite nullimage;


    List<GameObject> flytank;


    void Start()
    {
        gun = GameObject.FindWithTag("gun");
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        Transform bag = transform.Find("ButterFlyGrid");

        if (gun != null && gun.activeSelf)
        {
            flytank = gun.GetComponent<GunType>().FlyTank;
        }

        for (int i = 0; i < 4; i++)
        {
            //bagFlyTank_Image[] = bag;
        }

        reloadUI();

    }

    void Update()
    {

    }

    public void checkUIing(int aa)    //讓選取的UI做反應動畫等
    {

    }
    
    public void reloadUI() //重新載入UI介面
    {
        for (int i = 0; i < 3; i++)
        {
            if (gun.GetComponent<GunType>().FlyTank[i] == null)
            {
                FlyTank_Image[i].sprite = nullimage;
                /*
                FlyTank_EnergyImage[i].sprite = nullimage;
                Flycom[i].SetActive(false);
                */
            }
            else
            {
                FlyTank_Image[i].sprite = flytank[i].GetComponent<FlyData>().ButterFlyImage;

                /*
                Flycom[i].SetActive(true);
                FlyTank_EnergyImage[i].sprite = flytank[i].GetComponent<FlyData>().ButterFlyImage;
                FlyTank_EnergySlider[i].value = flytank[i].GetComponent<FlyData>().Fly_Energy;
                */
            }
        }
    }
}
