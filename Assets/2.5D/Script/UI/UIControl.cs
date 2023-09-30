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
    public Animator skillUnderAnime;
    [SerializeField]
    public Image[] FlyTank_Image;           //蝴蝶槽的[]
    [SerializeField]
    public Slider[] FlyTank_energy;
    
    public Sprite nullimage;
    [SerializeField]
    public GameObject lightSet;

    [SerializeField] 
    List<GameObject> flytank;

    private FlyData[] flyData;
    
    [SerializeField]
    public Image blood_R;
    [SerializeField]
    public Image blood_Y;

    [SerializeField]
    public Image O2Image;
    [SerializeField]
    public Animator playerHit;

    private float currentPrg, targetPrg;
    public float AccelerHpSpeed = 0.5f;

    public GameObject DebugGrid;
    public GameObject debugText;

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
        /*
        GameObject SkillUnder = GameObject.Find("SkillUnder");
        for (int i = 0; i < 3; i++)
        {
            FlyTank_Image[i] = SkillUnder.transform.GetChild(i).GetComponent<Image>();
            FlyTank_energy[i] = FlyTank_Image[i].transform.GetChild(i).GetComponent<Slider>();
        }

        blood_R = GameObject.Find("RedBlood").transform.GetComponent<Image>();
        blood_Y = GameObject.Find("YellowBlood").transform.GetComponent<Image>();
        O2Image = GameObject.Find("O2Image").transform.GetComponent<Image>();
        */
        if (gun != null && !gun.activeSelf)
        {
            ReloadGunUI();
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
        //playerHit.Play("nothing");
        playerHit.Play("hit");
        //playerHit.Play("Warning");
    }

    public void ChangeGunTank(int input)
    {
        if (!ReferenceEquals(skillUnderAnime , null))
        {
            skillUnderAnime.SetTrigger($"{input}");
        }
    }
    
    public void volumeSliderSet(float volumeValue)
    {
        AudioListener.volume = volumeValue;
        Debug.Log(volumeValue);
    }

    public void lightSliderSet(float lightValue)
    {
        if (GameManager.mapLevel >= 2)
        {
            float mappedValue = (1 - lightValue) * 150f;
            lightSet.GetComponent<Image>().color = new Color32(0, 0, 0, (byte)mappedValue);
            Debug.Log(lightValue);
            Debug.Log(mappedValue);
        }
    }

    public void DebugText(string debug)
    {
        
        //debugText.text = debug;
    }
    
}
