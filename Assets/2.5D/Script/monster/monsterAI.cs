using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class monsterAI : MonoBehaviour
{
    GameObject player;

    public GameObject Drop; //掉落物
    public float dropProbability = 0.5f; // 掉落物的機率

    public int HP;

    public LayerMask obstacleLayerMask; // 在腳本中設定 Layer Mask

    public float speed;
    public Vector2 stoppingDistance;

    public bool isright;

    public int damage;

    Animator animator;

    bool canDamage = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // 設定 obstacleLayerMask 為 "Obstacle" Layer 的 Layer Mask
        obstacleLayerMask = LayerMask.GetMask("Obstacle");

        animator = GetComponent<Animator>();


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ChasePlayer();

        gameObject.GetComponent<SpriteRenderer>().sortingOrder = (int)stoppingDistance.y/2 - (int)this.transform.position.y;

        Vector3 newPosition = transform.position;
        newPosition.z = 0f;
        transform.position = newPosition;

    }

    void Destroy()
    {
        Destroy(this.gameObject);
    }

    void killemeny()
    {
        Task.killEmeny += 1;
    }

    void ChasePlayer()
    {
        if (player != null)
        {
            // 計算距離
            float distance = Vector2.Distance(transform.position, player.transform.position);

            // 如果距離大於停止距離，就向玩家移動
            if (distance > stoppingDistance.x || distance > stoppingDistance.y && canDamage == true)
            {
                // 計算怪物要移動的方向
                Vector2 moveDirection = (player.transform.position - transform.position).normalized;

                // 移動怪物
                transform.position = Vector2.MoveTowards(transform.position, transform.position + (Vector3)moveDirection , speed * Time.deltaTime);
            }
            // 檢查 x 和 y 值的條件，如果其中一個小於特定值，觸發攻擊動畫
            if (distance < stoppingDistance.x || distance < stoppingDistance.y)
            {
                //Debug.Log("怪物應該要使出攻擊了吧");
                animator.SetTrigger("attack");
            }
            if (isright == true)
            {
                if (gameObject.transform.position.x > player.transform.position.x)
                {
                    this.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    this.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            }
            else
            {
                if (gameObject.transform.position.x > player.transform.position.x)
                {
                    this.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                else
                {
                    this.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }

        }
    }


    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.tag == "enemy")
        {
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            this.GetComponent<BoxCollider2D>().enabled = true;
        }


        if (other.tag == "PlayerCollider" && canDamage == true)
        {
            player.GetComponent<PlayerHeart>().damage(damage);
            canDamage = false;
            Invoke("waitDamage" , 1.8f);
        }
    }

    void waitDamage()
    {
        canDamage = true;
    }
   
    void DropDown()
    {
        if (Drop != null)
        {
            if (Random.value <= dropProbability)
            {
                Instantiate(Drop, transform.position, Quaternion.identity);
                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "skill" && canDamage == true)
        {
            HP -= other.GetComponent<skill>().damage;

            //Debug.Log("撞到東西囉");
            //other.GetComponent<FireControl>().lifeTime = 0;


            if (HP <= 0)
            {
                animator.SetTrigger("die");

            }
            else
            {
                //Destroy(other.GetComponent<FireControl>().hardcollider);
                //other.GetComponent<Animator>().SetTrigger("bom");
                //other.GetComponent<FireControl>().lifeTime = 0;
                animator.SetTrigger("hurt");
                canDamage = false;
                Invoke("waitDamage", 1f);
            }
        }
    }


}
