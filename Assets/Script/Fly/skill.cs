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
    public float speed = 0;
    [SerializeField]
    private PlayerHeart playerHeart;
    [SerializeField]
    private Playermovee playermovee;

    private SpriteRenderer spriteRenderer;
    private GameObject Player;
    
    private bool isplayerHealth = false;

    private Animator animator;
    private void Awake()
    {
        playerHeart = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHeart>();
        playermovee = GameObject.FindGameObjectWithTag("Player").GetComponent<Playermovee>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameManager.instance.player;
        animator = GetComponent<Animator>();
        moveDirection = new Vector3(speed * Time.deltaTime, 0, 0);

        if (skillID == 2)
        {
            
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(isplayerHealth);
        //move();
        spriteRenderer.sortingOrder = 1 - (int)Player.transform.position.y;
        
        if (isplayerHealth)
        {
            // 增加玩家的生命值，根据恢复速率和 Time.deltaTime 来计算
            lifeTime -= Time.deltaTime;
            //Debug.Log($"lifeTime = {lifeTime} , 玩家回血{damage * Time.deltaTime}");
            playerHeart.healHp(damage * Time.deltaTime);
        }

        move();

        // if (skillID == 3)
        // {
        //     transform.position = Player.transform.position;
        //     //Debug.Log("氧氣特效應該要在正確的位子");
        // }
        
    }

    private void useSkill(monsterAI monsterAI = null)
    {
        switch (skillID)
        {
            case 1://熾熱
                //monsterAI.isHurt(damage);
                break;
            case 2://生命
                lifeTime += 10;
                StartCoroutine(heal(damage));
                StartCoroutine(hurtMonster(monsterAI));
                animator.Play("animaBom");
                break;
            case 3://氧氣
                playerHeart.healO2(damage);
                playermovee.startplayerSpeedUP(5f);
                lifeTime = 5f;
                break;
            case 4://閃電
                break;
            case 5://光明
                break;
            case 6://傳送
                break;
            case 7://補血治癒圈 生命+(還有少東西)
                playerHeart.healO2(damage);
                isplayerHealth = true;
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
        if (other.tag == "Player")
        {
            //Debug.Log("有碰到玩家");
            if (skillID == 3)   //氧氣
            {
                //Debug.Log("有要使用");
                useSkill();
            }
        }
        
        if (other.tag == "enemy")
        {
            useSkill(other.GetComponent<monsterAI>());
            
            moveDirection = new Vector2(0, 0); // 停止移動
        }
        
        if (other.tag == "Object" && other.isTrigger == false)
        {
            if (skillID != 3)   //不是氧氣蝴蝶
            {
                moveDirection = new Vector2(0, 0); // 停止移動
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            /*
            Debug.Log("有碰到玩家");
            if (skillID == 3)   //氧氣
            {
                Player.GetComponent<Playermovee>().PlayerSpeed();
            }
            */
            
            //isplayerHealth = true;
            if (skillID == 7)   //生命(雙)
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
        if (other.tag == "Player")
        {
            if (skillID == 7)   //生命(雙)
            {
                useSkill();
                isplayerHealth = false;
            }
        }
    }
    
    
    IEnumerator heal(int healHP)
    {
        //UIControl.instance.DebugText("-回覆血量效果動畫");
        for (int i = 0; i < healHP; i++)
        {
            //Debug.Log($"回復了1d血 還有{5-i}次");
            playerHeart.healHp(1);
            yield return new WaitForSeconds(0.5f);
        }
        Destroy(gameObject);
    }
    
    IEnumerator hurtMonster(monsterAI monsterAI)
    {
        //UIControl.instance.DebugText("-回覆血量效果動畫");
        for (int i = 0; i < damage; i++)
        {
            monsterAI.isHurt(3);
            yield return new WaitForSeconds(0.5f);
        }
        Destroy(gameObject);
    }

    public void closeImage()
    {
        GetComponent<SpriteRenderer>().sprite = null;
        GetComponent<Collider2D>().enabled = false;
    }
    
}
