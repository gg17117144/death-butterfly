using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    public static UIControl instance;
    [SerializeField]
    GameObject gun;
    [SerializeField]
    public Image[] FlyTank_Image;           //蝴蝶槽的[]
    [SerializeField]
    public Slider[] FlyTank_energy;
    
    public Sprite nullimage;

    [SerializeField] 
    List<GameObject> flytank;

    private FlyData[] flyData;
    
    [SerializeField]
    public Image blood_R;
    [SerializeField]
    public Image blood_Y;

    [FormerlySerializedAs("H2OImage")] [SerializeField]
    public Image O2Image;
    [SerializeField]
    public GameObject deadtext;

    public Image PlayerHitImage;

    private float currentPrg, targetPrg;
    public float AccelerHpSpeed = 1.0f;
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
        if (deadtext != null)
        {
            deadtext.SetActive(false);
        }

        currentPrg = blood_R.fillAmount;
    }

    private void Update()
    {

    }

    public void ReloadGunUI() //重製UI
    {
        
        for (int i = 0; i < 3; i++)
        {
            if (flytank[i])
            {
                FlyTank_Image[i].sprite = flytank[i].GetComponent<FlyData>().ButterFlyImage;
                FlyTank_energy[i].value = flytank[i].GetComponent<FlyData>().Fly_Energy;
            }
            else
            {
                FlyTank_Image[i].sprite = nullimage;
                FlyTank_energy[i].value = 0;
            }
        }
    }

    public void ReloadPlayeHpUI(float HpValue)
    {
        blood_R.fillAmount = HpValue;
        targetPrg = HpValue;
        StartCoroutine(changeYValue());
    }

    IEnumerator changeYValue()
    {
        while (currentPrg != targetPrg)
        {
            // 使用 Mathf.MoveTowards 逐渐改变 currentPrg 的值
            currentPrg = Mathf.MoveTowards(currentPrg, targetPrg, AccelerHpSpeed * Time.deltaTime);
            // 更新血量条的显示
            blood_Y.fillAmount = currentPrg;

            yield return new WaitForSeconds(0.1f); // 调整这个等待时间以控制血量下降速度
        }
    }
    
    public void ReloadPlayerO2UI(float O2Value)
    {
        O2Image.fillAmount = O2Value;
    }

    public void PlayerHit()
    {
        PlayerHitImage.color = new Color32(255, 0, 0, 60);
    }


    
}
