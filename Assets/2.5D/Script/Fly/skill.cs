using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skill : MonoBehaviour
{
    public float lifeTime = 3f;
    
    public int skillID;

    public int damage;
    Vector3 moveDirection;
    public float speed = 0.3f;
    PlayerHeart playerHeart;

    // Start is called before the first frame update
    void Start()
    {
        moveDirection = new Vector3(speed * Time.deltaTime, 0, 0);
        playerHeart = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHeart>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    private void Hitdamage(monsterAI monsterAI)
    {
        switch (skillID)
        {
            case 1://熾熱
                //monsterAI.isHurt(damage);
                break;
            case 2://生命
                //Debug.Log($"發動{skillID}的技能");
                monsterAI.isHurt(damage);
                GetComponent<Collider2D>().enabled = false;
                GetComponent<SpriteRenderer>().sprite = null;
                UIControl.instance.DebugText("-回覆效果動畫");
                StartCoroutine(heal(damage));
                break;
            case 3://氧氣
                playerHeart.healO2(damage);
                break;
            case 4://閃電
                break;
        }
        Destroy(gameObject);
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
            Hitdamage(other.GetComponent<monsterAI>());
            
            moveDirection = new Vector2(0, 0); // 停止移動
        }

        if (other.tag == "Object")
        {
            moveDirection = new Vector2(0, 0); // 停止移動
            Destroy(gameObject);
        }
    }

    IEnumerator heal(int healHP)
    {
        for (int i = 0; i < healHP; i++)
        {
            playerHeart.healHp(1);
            yield return new WaitForSeconds(0.5f);
        }
    }
    
}
