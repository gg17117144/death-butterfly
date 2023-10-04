using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class skill : MonoBehaviour
{
    public float lifeTime = 3f;
    
    public int skillID;

    public int damage;
    Vector3 moveDirection;
    public float speed = 0.3f;
    [SerializeField]
    private PlayerHeart playerHeart;
    [SerializeField]
    private Playermovee playermovee;

    private bool isplayerHealth = false;
    // Start is called before the first frame update
    void Start()
    {
        moveDirection = new Vector3(speed * Time.deltaTime, 0, 0);
        playerHeart = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHeart>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(isplayerHealth);
        //move();
        lifeTime -= Time.deltaTime;
        
        if (lifeTime <= 0)
        {
            moveDirection = new Vector2(0,0); // 停止移動
            Destroy(gameObject);
        }
        else if (lifeTime > 0.1f)
        {
            //transform.Translate(moveDirection);
        }
        
        if (isplayerHealth)
        {
            // 增加玩家的生命值，根据恢复速率和 Time.deltaTime 来计算
            lifeTime -= Time.deltaTime;
            //Debug.Log($"lifeTime = {lifeTime} , 玩家回血{damage * Time.deltaTime}");
            playerHeart.healHp(damage * Time.deltaTime);
        }
    }

    private void useSkill(monsterAI monsterAI = null)
    {
        switch (skillID)
        {
            case 1://熾熱
                //monsterAI.isHurt(damage);
                break;
            case 2://生命
                StartCoroutine(heal(damage));
                //isplayerHealth = true;
                break;
            case 3://氧氣
                //playerHeart.healO2(damage);
                playerHeart = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHeart>();
                playermovee = GameObject.FindGameObjectWithTag("Player").GetComponent<Playermovee>();
                playermovee.startplayerSpeedUP();
                playerHeart.healO2(damage);
                Destroy(gameObject);
                break;
            case 4://閃電
                break;
            case 5://光明
                break;
            case 6://傳送
                break;
            case 7://補血治癒圈 生命+
                break;
        }
        //Destroy(gameObject);
    }
    void move()
    {
        lifeTime -= Time.deltaTime;
        
        if (lifeTime <= 0)
        {
            moveDirection = new Vector2(0,0); // 停止移動
            Destroy(gameObject);
        }
        else if (lifeTime > 0.1f)
        {
            transform.Translate(moveDirection);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "enemy")
        {
            useSkill(other.GetComponent<monsterAI>());
            
            moveDirection = new Vector2(0, 0); // 停止移動
        }
        
        if (other.tag == "PlayerCollider")
        {
            if (skillID == 3)   //氧氣
            {
                useSkill();
            }
        }
        
        if (other.tag == "Object")
        {
            moveDirection = new Vector2(0, 0); // 停止移動
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "PlayerCollider")
        {
            //isplayerHealth = true;
            if (skillID == 2)   //生命(雙)
            {
                // 增加玩家的生命值，根据恢复速率和 Time.deltaTime 来计算
                lifeTime -= Time.deltaTime;
                //Debug.Log($"lifeTime = {lifeTime} , 玩家回血{damage * Time.deltaTime}");
                playerHeart.healHp(damage * Time.deltaTime);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "PlayerCollider")
        {
            if (skillID == 2)   //生命
            {
                useSkill();
                isplayerHealth = false;
            }
        }
    }
    
    
    IEnumerator heal(int healHP)
    {
        UIControl.instance.DebugText("-回覆血量效果動畫");
        for (int i = 0; i < healHP; i++)
        {
            //Debug.Log($"回復了1d血 還有{5-i}次");
            playerHeart.healHp(1);
            yield return new WaitForSeconds(0.5f);
        }
        Destroy(gameObject);
    }
    
    

    
}
