using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerHeart : MonoBehaviour
{
    private static PlayerHeart instance;
    public float max_hp;              //最大血量值
    public float oldhp;                  //血量值
    public float newhp;

    public float max_O2;                    //最大氧氣值
    public float O2;                        //氧氣值

    public float O2timer;                 //時間

    //bool canDamage = true;               //受傷休息時間


    // Start is called before the first frame update
    void Start()
    {
        ResetPlayerValue();
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
        if (GameManager.mapLevel >= 2)
        {
            O2timer += Time.deltaTime;    //計算時間

            if (O2timer >= 3)
            {
                O2timer = 0;
                O2 -= 1;
            }

            if (O2 <= 0)
            {
                GetComponent<playerAnime>().Dead();
            }
        }
    }


    public void damage(int damage)  //碰到敵人扣damage
    {
        newhp -= damage;
        float HpValue = newhp / max_hp;
        UIControl.instance.ReloadPlayeHpUI(HpValue);
    }
    
    public void healHp(int healHP)
    {
        newhp += healHP;
        float HpValue = newhp / max_hp;
        UIControl.instance.ReloadPlayeHpUI(HpValue);
    }

    public void healO2(int healO2)
    {
        O2 += healO2;
        float O2Value = O2 / max_O2;
        UIControl.instance.ReloadPlayerO2UI(O2Value);
    }

    void ResetPlayerValue()
    {
        max_hp = 200;               //最大血量值
        oldhp = max_hp;
        newhp = oldhp;                //血量設為最大血量值

        max_O2 = 100;              //最大氧氣值
        O2 = max_O2;              //氧氣設為最大氧氣值

        O2timer = 0;                  //時間設定0
        float O2Value = O2 / max_O2;
        float HpValue = newhp / max_hp;
        UIControl.instance.ReloadPlayeHpUI(HpValue);
        UIControl.instance.ReloadPlayerO2UI(O2Value);
    }
    
    public void relife()
    {
        Debug.Log("重生");
        ResetPlayerValue();
        GameManager.isStoping = false;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

}
