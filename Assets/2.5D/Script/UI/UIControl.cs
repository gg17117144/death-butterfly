using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    public static UIControl instance;
    [SerializeField]
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
    }

    private void Start()
    {
        gun = GameManager.instance.gun;
        flytank = gun.GetComponent<GunType>().FlyTank;
        if (gun != null && !gun.activeSelf)
        {
            ReloadGunUI();
        }
    }

    private void Update()
    {
        //Debug.Log(flytank.Count);
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
