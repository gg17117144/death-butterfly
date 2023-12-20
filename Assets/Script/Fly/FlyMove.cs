using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMove : MonoBehaviour
{
    public bool t = true;
    public float movespeed;

    float posX;
    float posY;

    public float Tt = 0;

    public float size = 1.5f;


    private SpriteRenderer spriteRenderer;
    //float lifeTime = 30;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        fly_move();
    }

    void fly_move()
    {
        if (Tt <= 0)
        {
            posX = Random.Range(-movespeed, movespeed);
            posY = Random.Range(-movespeed, movespeed);

            if (posX >= 0)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }

            float Xabs = Mathf.Abs(posX);
            float Yabs = Mathf.Abs(posY);
            Tt = Xabs + Yabs;
        }
        else
        {
            //Debug.Log("�������ʤ�");
            this.gameObject.transform.Translate(new Vector3(posX, posY, 0) * Time.deltaTime);
            Tt -= Time.deltaTime;
        }
    }


}
