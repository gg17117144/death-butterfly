using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerHeart : MonoBehaviour
{
    private static PlayerHeart instance;
    public float max_hp;              //�̤j��q��
    public float oldhp;                  //��q��
    public float newhp;

    public float max_O2;                    //�̤j����
    public float O2;                        //����

    public float O2timer;                 //�ɶ�

    //bool canDamage = true;               //���˥𮧮ɶ�


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
        if (newhp > max_hp)
        {
            newhp = max_hp;
            //Debug.Log("有執行吧");
        }
        if (O2 > max_O2)
        {
            O2 = max_O2;
        }

        if (GameManager.instance.isStoping)
        {
            
            if (newhp <= 0)
            {
                newhp = 0;
            }
        }

        if (GameManager.mapLevel >= 2)
        {
            O2timer += Time.deltaTime;    //�p��ɶ�

            if (O2timer >= 3)
            {
                O2timer = 0;
                O2 -= 1;
                UIControl.instance.ReloadPlayerO2UI(O2 / 100);
            }

            if (O2 <= 0)
            {
                GetComponent<PlayerAnime>().Dead();
            }
        }
    }


    public void damage(int damage)  //�I��ĤH��damage
    {
        newhp -= damage;
        float HpValue = newhp / max_hp;
        UIControl.instance.ReloadPlayeHpUI(HpValue);
        UIControl.instance.PlayerHit();
    }
    
    public void healHp(float healHP)
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

    public void ResetPlayerValue()
    {
        max_hp = 200;               //�̤j��q��
        oldhp = max_hp;
        newhp = oldhp;                //��q�]���̤j��q��

        max_O2 = 100;              //�̤j����
        O2 = max_O2;              //���]���̤j����

        O2timer = 0;                  //�ɶ��]�w0
        float O2Value = O2 / max_O2;
        float HpValue = newhp / max_hp;
        UIControl.instance.ReloadPlayeHpUI(HpValue);
        UIControl.instance.ReloadPlayerO2UI(O2Value);
    }
    
    public void relife()
    {
        Debug.Log("重生囉~");
        GameManager.instance.isStoping = false;
        GameManager.instance.checkIsStoping();
        GameManager.instance.isDeading = false;
        GameManager.instance.sceneController.LoadScene(GameManager.instance.SceneIndex);
        ResetPlayerValue();
        GameManager.instance.gun.GetComponent<GunType>().reSetGunValue();
        // GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        // foreach (GameObject enemy in enemies)
        // {
        //     Destroy(enemy);
        // }
    }

    public void Invincible()
    {
        StartCoroutine(nameof(StartInvincible));
    }

    IEnumerator StartInvincible()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(5f);
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<CapsuleCollider2D>().enabled = true;
    }

}
