using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeart : MonoBehaviour
{
    private static PlayerHeart instance;
    public int max_hp;              //最大血量值
    public int hp;                  //血量值
    public float nowhp;
    [SerializeField]
    public Image blood;
    [SerializeField]
    public Image H2OImage;
    [SerializeField]
    public GameObject deadtext;

    public int max_H2O;                    //最大氧氣值
    public int H2O;                        //氧氣值

    public float H2Otimer;                 //時間

    //bool canDamage = true;               //受傷休息時間


    // Start is called before the first frame update
    void Start()
    {
        max_hp = 200;               //最大血量值
        hp = max_hp;                //血量設為最大血量值

        max_H2O = 100;              //最大氧氣值
        H2O = max_H2O;              //氧氣設為最大氧氣值

        H2Otimer = 0;                  //時間設定0

        if (deadtext != null)
        {
            deadtext.SetActive(false);
        }
    }

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

    // Update is called once per frame
    void FixedUpdate()
    {
        playerdead();
        if (GameManager.isFight)
        {
            nowhp = (float)hp / max_hp; //顯示現在血量為血量除最大血量 控制為1內
            H2Otimer += Time.deltaTime;    //計算時間

            if (H2Otimer >= 3)
            {
                H2Otimer = 0;
                H2O -= 1;
            }

            if (H2O <= 0)
            {
                playerdead();
            }

            blood.fillAmount = nowhp;
            H2OImage.fillAmount = (float)H2O / max_H2O;
        }
    }


    public void damage(int damage)  //碰到敵人扣damage
    {
        hp = hp - damage;
    }

    public void healHp(int healHP)
    {
        hp = hp + healHP;
    }

    public void healO2(int healO2)
    {
        H2O = H2O + healO2;
    }

    /*
    void waitDamage()
    {
        canDamage = true;
    }
    */


    void playerdead()   //玩家死亡並重生
    {
        if (hp <= 0)
        {
            deadtext.SetActive(true);
            GameManager.isStoping = true;
            
            //Invoke("restart", 2);
        }
    }


    void restart()
    {
        GameManager.isStoping = false;
    }

    public void relife()
    {
        Debug.Log("重生");
        hp = max_hp;
        GameManager.isStoping = false;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }


}
