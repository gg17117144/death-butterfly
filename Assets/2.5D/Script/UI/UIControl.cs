using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    public static UIControl instance;
    GameObject gun;
    [SerializeField]
    public Image[] FlyTank_Image;           //蝴蝶槽的[]

    private Image[] FlyTank_energy;
    
    public Sprite nullimage;

    [SerializeField]
    List<GameObject> flytank;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        gun = GameObject.FindWithTag("gun");

        if (gun != null && gun.activeSelf)
        {
            flytank = gun.GetComponent<GunType>().FlyTank;
        }
        
        ReloadGunUI();
    }


    public void ReloadGunUI() //重製UI
    {
        for (int i = 0; i < 3; i++)
        {
            if (flytank[i])
            {
                FlyTank_Image[i].sprite = flytank[i].GetComponent<FlyData>().ButterFlyImage;
                //FlyTank_energy[i].color = new Color(225, 225, 225, 150);
            }
            else
            {
                FlyTank_Image[i].sprite = nullimage;
            }
        }
    }
}
