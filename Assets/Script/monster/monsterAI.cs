using UnityEngine;

public class monsterAI : MonoBehaviour
{
    GameObject player;

    public GameObject Drop; //掉落物
    public float dropProbability = 0.5f; // 掉落物的機率

    public int HP;

    public LayerMask obstacleLayerMask; // 在腳本中設定 Layer Mask

    public float speed;
    public Vector2 stoppingDistance;
    public bool monsterStop = true;

    public bool isright;

    private GameObject task;

    public int damage;

    Animator animator;

    private bool canDamage = true;
    //private bool isattack;

    private SpriteRenderer spriteRenderer;

    public float waitAttackTime = 1.8f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // 設定 obstacleLayerMask 為 "Obstacle" Layer 的 Layer Mask
        obstacleLayerMask = LayerMask.GetMask("Obstacle");

        animator = GetComponent<Animator>();

        task = GameManager.instance.task;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager.instance.isStoping)
        {
            ChasePlayer();

            spriteRenderer.sortingOrder = (int)stoppingDistance.y / 2 - (int)this.transform.position.y;

            Vector3 newPosition = transform.position;
            newPosition.z = 0f;
            transform.position = newPosition;
        }
    }

    void Destroy()
    {
        Destroy(this.gameObject);
    }

    void killemeny()
    {
        Task.instance.killEmeny += 1;
        task.GetComponent<Task>().mapTask();
    }

    void ChasePlayer()
    {
        if (!ReferenceEquals(player, null))
        {
            // 計算距離
            float distance = Vector2.Distance(transform.position, player.transform.position);

            // 如果距離大於停止距離，就向玩家移動
            if (monsterStop == false && canDamage && HP > 0)
            {
                // 計算怪物要移動的方向
                Vector2 moveDirection = (player.transform.position - transform.position).normalized;

                // 移動怪物
                transform.position = Vector2.MoveTowards(transform.position,
                    transform.position + (Vector3)moveDirection, speed * Time.deltaTime * Random.Range(1.0f, 2.0f));
            }

            // 檢查 x 和 y 值的條件，如果其中一個小於特定值，觸發攻擊動畫
            if (monsterStop && !canDamage)
            {
                //Debug.Log("怪物應該要使出攻擊了吧");
                //Debug.Log(canDamage);
                animator.Play("attack");
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
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }


        if (other.tag == "Player" && canDamage)
        {
            monsterStop = true;
            canDamage = false;
            player.GetComponent<PlayerHeart>().damage(damage);
            Invoke("waitDamage", waitAttackTime);
        }
        else
        {
            monsterStop = false;
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

    public void isHurt(int damage)
    {
        if (canDamage)
        {
            HP -= damage;
            UIControl.instance.ShowDamageText(damage,this.transform.position);
            
            if (HP <= 0)
            {
                animator.SetTrigger("die");
                canDamage = false;
            }
            else
            {
                canDamage = false;
                //isattack = true;
                animator.Play("hurt");
                Invoke("waitDamage", 0.5f);
            }
        }
    }
}