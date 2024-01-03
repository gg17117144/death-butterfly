using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;
using UnityHFSM;

public class BossAI : MonoBehaviour
{
    [SerializeField]
    private int hp;
    private int maxhp = 1000;
    
    public GameObject ammoPrefab;
    public int numberOfAttacks = 12; // 技能的數量
    public float radius = 5f; // 圓形範圍的半徑

    private GameObject player;


    private float time;
    private bool isLever2;

    public StateMachine fsm;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        InvokeRepeating("skill02",5,5);
        fsm = new StateMachine();
        fsm.AddState("lever1", onLogic: state => lever1());
        fsm.AddState("lever2", onLogic: state => lever2());
        fsm.SetStartState("lever1");
        hp = maxhp;
    }
    
    // Update is called once per frame
    void Update()
    {
        time = Time.deltaTime;
    }

    void lever1()
    {
        checkLever2();
        if (time > 5)
        {
            time = 0;
            skill02();
        }
    }
    
    void lever2()
    {
        if (time > 5)
        {
            time = 0;
            skill01();
        }
    }
    
    public void damage(int damage,Vector3 skillposition)  //�I��ĤH��damage
    {
        hp -= damage;
        // UIControl.instance.ReloadBossHpUI(hp);
        UIControl.instance.ShowDamageText(damage,skillposition);
        // UIControl.instance.PlayerHit();
        checkBossIsDie();
    }

    void checkLever2()
    {
        if (hp <= 500)
        {
            isLever2 = true;
            fsm.RequestStateChange("lever2");
        }
    }

    void checkBossIsDie()
    {
        if (hp <= 0)
        {
            UIControl.instance.windowsBroken();
            Destroy(gameObject);
        }
    }

    [Button]
    void skill01()
    {
        var numberOfAttacks = 20;
        for (int i = 0; i < numberOfAttacks; i++)
        {
            float angle = i * (360f / numberOfAttacks); // 計算每個技能的角度

            // 計算技能的位置
            float x = transform.position.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = transform.position.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);

            Vector3 attackPosition = new Vector3(x, y, 0f);
            // 計算技能的方向
            Vector3 direction = (attackPosition - transform.position).normalized;

            // 生成技能，並使其指向外部
            GameObject attack = Instantiate(ammoPrefab, attackPosition, Quaternion.identity);
            attack.transform.right = direction; // 將技能的右方向設為計算出的方向
            attack.transform.parent = transform;
        }
    }
    
    [Button]
    void skill02()
    {
        // 获取玩家对象的位置
        Vector2 playerPosition = player.transform.position;/* 玩家物件的位置 */;

        // 计算脚本对象到玩家的方向向量
        Vector2 directionToPlayer = playerPosition - new Vector2(transform.position.x, transform.position.y);

        // 计算脚本对象到玩家的角度
        float angleToPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

        float startAngle = angleToPlayer - 45f; // 起始角度
        float endAngle = angleToPlayer + 45f; // 結束角度
        var numberOfAttacks = 5;
        for (int i = 0; i < numberOfAttacks; i++)
        {
            float normalizedProgress = i / (numberOfAttacks - 1f); // 進度在 0 到 1 之間
            float angle = Mathf.Lerp(startAngle, endAngle, normalizedProgress); // 插值計算角度

            // 計算技能的位置
            float x = transform.position.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = transform.position.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);

            Vector3 attackPosition = new Vector3(x, y, 0f);

            // 計算技能的方向
            Vector3 direction = (attackPosition - transform.position).normalized;

            // 生成技能，並使其指向外部
            GameObject attack = Instantiate(ammoPrefab, attackPosition, Quaternion.identity);
            attack.transform.right = direction; // 將技能的右方向設為計算出的方向
            attack.transform.parent = transform;
        }
    }

}
