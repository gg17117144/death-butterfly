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



    //float lifeTime = 30;
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        fly_move();

        /*
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
        */
    }

    void fly_move()
    {
        if (Tt <= 0)
        {
            posX = Random.Range(-movespeed, movespeed);
            posY = Random.Range(-movespeed, movespeed);

            if (posX >= 0)
            {
                this.transform.localScale = new Vector3(1.5f, 1.5f, 1);
            }
            else
            {
                this.transform.localScale = new Vector3(-1.5f, 1.5f, 1);
            }

            float Xabs = Mathf.Abs(posX);
            float Yabs = Mathf.Abs(posY);
            Tt = Xabs + Yabs;
        }
        else
        {
            //Debug.Log("½¹½º²¾°Ê¤¤");
            this.gameObject.transform.Translate(new Vector3(posX, posY, 0) * Time.deltaTime);
            Tt -= Time.deltaTime;
        }
    }


}
